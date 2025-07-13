using System.Runtime.CompilerServices;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Services.Interfaces;
using CreateSpeechRequest = VeniceAI.SDK.Models.Audio.CreateSpeechRequest;
using CreateSpeechResponse = VeniceAI.SDK.Models.Audio.CreateSpeechResponse;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for audio operations using the Venice AI API.
/// </summary>
public class AudioService : IAudioService
{
    private readonly IVeniceAIGeneratedClient _generatedClient;

    /// <summary>
    /// Initializes a new instance of the AudioService class.
    /// </summary>
    /// <param name="generatedClient">The generated Venice AI client.</param>
    public AudioService(IVeniceAIGeneratedClient generatedClient)
    {
        _generatedClient = generatedClient ?? throw new ArgumentNullException(nameof(generatedClient));
    }

    /// <summary>
    /// Creates speech from text.
    /// </summary>
    /// <param name="request">The speech creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The speech response.</returns>
    public async Task<CreateSpeechResponse> CreateSpeechAsync(CreateSpeechRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Input))
            throw new ArgumentException("Input text is required", nameof(request));

        if (request.Streaming == true)
        {
            throw new InvalidOperationException("Use CreateSpeechStreamAsync for streaming requests.");
        }

        try
        {
            // Ensure streaming is false for non-streaming requests
            request.Streaming = false;

            // Convert SDK request to generated client format
            var generatedRequest = request.ToGeneratedRequest();
            
            // Call the generated client
            var fileResponse = await _generatedClient.CreateSpeechAsync(generatedRequest, cancellationToken);

            // Convert back to SDK format
            return await fileResponse.ToSdkSpeechResponseAsync();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Speech creation failed: {ex.Message}", ex);
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
    public IAsyncEnumerable<byte[]> CreateSpeechStreamAsync(CreateSpeechRequest request, CancellationToken cancellationToken = default)
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

        // Convert SDK request to generated client format
        var generatedRequest = request.ToGeneratedRequest();

        FileResponse? fileResponse = null;
        Stream? stream = null;
        
        try
        {
            // Call the generated client for streaming
            fileResponse = await _generatedClient.CreateSpeechAsync(generatedRequest, cancellationToken);
            stream = fileResponse.Stream;
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Streaming speech creation failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during streaming speech creation: {ex.Message}", ex);
        }

        if (stream != null)
        {
            try
            {
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
                fileResponse?.Dispose();
            }
        }
    }
}
