using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Audio;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for voice changer (speech-to-speech) operations using the Venice AI API.
/// </summary>
public class VoiceChangerService : BaseHttpService, IVoiceChangerService
{
    /// <summary>
    /// Initializes a new instance of the VoiceChangerService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public VoiceChangerService(HttpClient httpClient, string apiKey, ILogger<VoiceChangerService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Queues a voice conversion supplying the source recording as a URL.
    /// </summary>
    /// <param name="request">The queue request. <see cref="QueueVoiceChangerRequest.AudioUrl"/> is required.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    public async Task<QueueVoiceChangerResponse> QueueVoiceChangerAsync(
        QueueVoiceChangerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (string.IsNullOrWhiteSpace(request.AudioUrl))
            throw new ArgumentException("AudioUrl is required for JSON queue requests. Use QueueVoiceChangerWithFileAsync to upload a file.", nameof(request));

        try
        {
            var response = await PostAsync<QueueVoiceChangerRequest, QueueVoiceChangerResponse>(
                "audio/voice-changer/queue",
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
            throw new VeniceAIException($"Unexpected error during voice changer queue: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Queues a voice conversion supplying the source recording as an uploaded file.
    /// </summary>
    /// <param name="request">The queue request. <see cref="QueueVoiceChangerRequest.File"/> is required.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    public async Task<QueueVoiceChangerResponse> QueueVoiceChangerWithFileAsync(
        QueueVoiceChangerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("File is required", nameof(request));

        try
        {
            var fields = new Dictionary<string, string>
            {
                ["model"] = request.Model
            };

            if (!string.IsNullOrWhiteSpace(request.Voice))
                fields["voice"] = request.Voice;

            if (request.RemoveBackgroundNoise.HasValue)
                fields["remove_background_noise"] = request.RemoveBackgroundNoise.Value ? "true" : "false";

            if (request.Seed.HasValue)
                fields["seed"] = request.Seed.Value.ToString(CultureInfo.InvariantCulture);

            var response = await PostMultipartAsync<QueueVoiceChangerResponse>(
                "audio/voice-changer/queue",
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
            throw new VeniceAIException($"Unexpected error during voice changer queue: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Polls a queued voice conversion. Returns converted audio bytes when finished,
    /// or a "PROCESSING" status while still running.
    /// </summary>
    /// <param name="request">The retrieve request with the queue ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The conversion status or converted audio.</returns>
    public async Task<RetrieveVoiceChangerResponse> RetrieveVoiceChangerAsync(
        RetrieveVoiceChangerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (string.IsNullOrWhiteSpace(request.QueueId))
            throw new ArgumentException("QueueId is required", nameof(request));

        try
        {
            var (binary, json, contentType, _) = await PostBinaryOrJsonAsync(
                "audio/voice-changer/retrieve",
                request,
                cancellationToken);

            if (binary != null)
            {
                return new RetrieveVoiceChangerResponse
                {
                    AudioContent = binary,
                    ContentType = contentType,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }

            var response = JsonSerializer.Deserialize<RetrieveVoiceChangerResponse>(json ?? "{}")
                ?? throw new VeniceAIException("Null response from API");
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
            throw new VeniceAIException($"Unexpected error during voice changer retrieve: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Releases the provider-held media for a finished voice conversion.
    /// </summary>
    /// <param name="request">The complete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    public async Task<CompleteVoiceChangerResponse> CompleteVoiceChangerAsync(
        CompleteVoiceChangerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (string.IsNullOrWhiteSpace(request.QueueId))
            throw new ArgumentException("QueueId is required", nameof(request));

        try
        {
            var response = await PostAsync<CompleteVoiceChangerRequest, CompleteVoiceChangerResponse>(
                "audio/voice-changer/complete",
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
            throw new VeniceAIException($"Unexpected error during voice changer complete: {ex.Message}", ex);
        }
    }
}
