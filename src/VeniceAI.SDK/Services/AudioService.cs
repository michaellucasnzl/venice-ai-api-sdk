using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Models.Common;
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

        try
        {
            // Create request matching Venice AI API format
            var apiRequest = new
            {
                input = request.Input,
                model = request.Model?.ToModelString() ?? TextToSpeechModel.TtsKokoro.ToModelString(),
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

    /// <summary>
    /// Queues a new audio generation request.
    /// </summary>
    /// <param name="request">The audio generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    public async Task<QueueAudioResponse> QueueAudioAsync(
        QueueAudioRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        try
        {
            var response = await PostAsync<QueueAudioRequest, QueueAudioResponse>(
                "audio/queue",
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during audio queue: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves the status and result of an audio generation request.
    /// </summary>
    /// <param name="request">The retrieve request with queue ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The audio generation status and result.</returns>
    public async Task<RetrieveAudioResponse> RetrieveAudioAsync(
        RetrieveAudioRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.QueueId))
            throw new ArgumentException("QueueId is required", nameof(request));

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        try
        {
            var response = await PostAsync<RetrieveAudioRequest, RetrieveAudioResponse>(
                "audio/retrieve",
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during audio retrieve: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Marks an audio generation as complete and deletes media from storage.
    /// </summary>
    /// <param name="request">The complete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    public async Task<CompleteAudioResponse> CompleteAudioAsync(
        CompleteAudioRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.QueueId))
            throw new ArgumentException("QueueId is required", nameof(request));

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        try
        {
            var response = await PostAsync<CompleteAudioRequest, CompleteAudioResponse>(
                "audio/complete",
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during audio complete: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a price quote for an audio generation request.
    /// </summary>
    /// <param name="request">The quote request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The price quote.</returns>
    public async Task<QuoteAudioResponse> QuoteAudioAsync(
        QuoteAudioRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var response = await PostAsync<QuoteAudioRequest, QuoteAudioResponse>(
                "audio/quote",
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during audio quote: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Transcribes audio to text.
    /// </summary>
    /// <param name="request">The transcription request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The transcription response.</returns>
    public async Task<CreateTranscriptionResponse> TranscribeAudioAsync(
        CreateTranscriptionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("File is required", nameof(request));

        try
        {
            var fields = new Dictionary<string, string>
            {
                ["model"] = request.Model.ToModelString()
            };

            if (!string.IsNullOrEmpty(request.ResponseFormat))
                fields["response_format"] = request.ResponseFormat;

            if (request.Timestamps.HasValue)
                fields["timestamps"] = request.Timestamps.Value ? "true" : "false";

            if (!string.IsNullOrEmpty(request.Language))
                fields["language"] = request.Language;

            var response = await PostMultipartAsync<CreateTranscriptionResponse>(
                "audio/transcriptions",
                request.File,
                request.Filename,
                "file",
                fields,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during audio transcription: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates a cloned voice from an audio sample.
    /// </summary>
    /// <param name="request">The cloned voice request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cloned voice response.</returns>
    public async Task<CreateClonedVoiceResponse> CreateClonedVoiceAsync(
        CreateClonedVoiceRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("File is required", nameof(request));

        try
        {
            var fields = new Dictionary<string, string>
            {
                ["model"] = request.Model.ToModelString()
            };

            var response = await PostMultipartAsync<CreateClonedVoiceResponse>(
                "audio/voices",
                request.File,
                request.Filename,
                "file",
                fields,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during voice cloning: {ex.Message}", ex);
        }
    }
}
