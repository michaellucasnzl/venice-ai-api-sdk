using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for audio operations.
/// </summary>
public class AudioService : BaseService, IAudioService
{
    /// <summary>
    /// Initializes a new instance of the AudioService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    public AudioService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger<AudioService> logger)
        : base(httpClient, options, logger)
    {
    }

    /// <inheritdoc />
    public async Task<CreateSpeechResponse> CreateSpeechAsync(CreateSpeechRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Streaming == true)
        {
            throw new InvalidOperationException("Use CreateSpeechStreamAsync for streaming requests.");
        }

        request.Streaming = false;
        
        try
        {
            var response = await HttpClient.PostAsync("/audio/speech", 
                new StringContent(System.Text.Json.JsonSerializer.Serialize(request), 
                System.Text.Encoding.UTF8, "application/json"), cancellationToken);

            var result = new CreateSpeechResponse
            {
                StatusCode = (int)response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                result.AudioContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);
                result.ContentType = response.Content.Headers.ContentType?.MediaType ?? "audio/mpeg";
            }

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating speech");
            throw;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<byte[]> CreateSpeechStreamAsync(
        CreateSpeechRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        request.Streaming = true;

        HttpResponseMessage? response = null;
        Stream? stream = null;

        try
        {
            response = await HttpClient.PostAsync("/audio/speech",
                new StringContent(System.Text.Json.JsonSerializer.Serialize(request),
                System.Text.Encoding.UTF8, "application/json"), cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                Logger.LogError("Speech streaming request failed with status {StatusCode}: {Content}", 
                    response.StatusCode, errorContent);
                yield break;
            }

            stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var buffer = new byte[8192];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                var chunk = new byte[bytesRead];
                Array.Copy(buffer, chunk, bytesRead);
                yield return chunk;
            }
        }
        finally
        {
            stream?.Dispose();
            response?.Dispose();
        }
    }
}
