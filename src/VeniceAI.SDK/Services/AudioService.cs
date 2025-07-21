using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Extensions;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for audio operations using the Venice AI API.
/// </summary>
public class AudioService : BaseHttpService, IAudioService
{
    /// <summary>
    /// Initializes a new instance of the AudioService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public AudioService(HttpClient httpClient, string apiKey, ILogger<AudioService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Creates speech from text.
    /// </summary>
    /// <param name="request">The speech creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The speech creation response.</returns>
    public async Task<CreateSpeechResponse> CreateSpeechAsync(
        CreateSpeechRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Input))
            throw new ArgumentException("Input text is required", nameof(request));

        // Validate the model if provided
        if (!string.IsNullOrEmpty(request.Model))
        {
            ModelEnumExtensions.ValidateTextToSpeechModel(request.Model);
        }

        try
        {
            // Create request matching Venice AI API format
            var apiRequest = new
            {
                input = request.Input,
                model = request.Model ?? "tts-1",
                voice = request.Voice ?? "alloy",
                response_format = request.ResponseFormat ?? "mp3",
                speed = request.Speed ?? 1.0,
                streaming = request.Streaming ?? false
            };

            // Get binary response
            var (audioData, contentType) = await PostForBinaryAsync(
                "audio/speech",
                apiRequest,
                cancellationToken);

            return new CreateSpeechResponse
            {
                AudioContent = audioData,
                ContentType = contentType,
                StatusCode = 200,
                IsSuccess = true
            };
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during speech creation: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates streaming speech from text.
    /// </summary>
    /// <param name="request">The speech creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of audio chunks.</returns>
    public IAsyncEnumerable<byte[]> CreateSpeechStreamAsync(
        CreateSpeechRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Input))
            throw new ArgumentException("Input text is required", nameof(request));

        return CreateSpeechStreamInternalAsync(request, cancellationToken);
    }

    private async IAsyncEnumerable<byte[]> CreateSpeechStreamInternalAsync(
        CreateSpeechRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // Set streaming to true
        request.Streaming = true;

        // Use the streaming binary method for proper streaming support
        await foreach (var chunk in PostStreamBinaryAsync("audio/speech", request, cancellationToken))
        {
            yield return chunk;
        }
    }
}
