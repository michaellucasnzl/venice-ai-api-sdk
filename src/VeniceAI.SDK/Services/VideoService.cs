using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Video;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for video generation operations using the Venice AI API.
/// </summary>
public class VideoService : BaseHttpService, IVideoService
{
    /// <summary>
    /// Initializes a new instance of the VideoService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public VideoService(HttpClient httpClient, string apiKey, ILogger<VideoService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Queues a new video generation request.
    /// </summary>
    /// <param name="request">The video generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    public async Task<QueueVideoResponse> QueueVideoAsync(
        QueueVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        if (string.IsNullOrEmpty(request.Duration))
            throw new ArgumentException("Duration is required", nameof(request));

        try
        {
            var response = await PostAsync<QueueVideoRequest, QueueVideoResponse>(
                "video/queue",
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
            throw new VeniceAIException($"Unexpected error during video queue: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves the status and result of a video generation request.
    /// </summary>
    /// <param name="request">The retrieve request with queue ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The video generation status and result.</returns>
    public async Task<RetrieveVideoResponse> RetrieveVideoAsync(
        RetrieveVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.QueueId))
            throw new ArgumentException("QueueId is required", nameof(request));

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        try
        {
            var response = await PostAsync<RetrieveVideoRequest, RetrieveVideoResponse>(
                "video/retrieve",
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
            throw new VeniceAIException($"Unexpected error during video retrieve: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves a video by queue ID and model.
    /// </summary>
    /// <param name="model">The model used for generation.</param>
    /// <param name="queueId">The queue ID from the queue response.</param>
    /// <param name="deleteMediaOnCompletion">Whether to delete media after retrieval.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The video generation status and result.</returns>
    public Task<RetrieveVideoResponse> RetrieveVideoAsync(
        string model,
        string queueId,
        bool deleteMediaOnCompletion = false,
        CancellationToken cancellationToken = default)
    {
        return RetrieveVideoAsync(new RetrieveVideoRequest
        {
            Model = model,
            QueueId = queueId,
            DeleteMediaOnCompletion = deleteMediaOnCompletion
        }, cancellationToken);
    }

    /// <summary>
    /// Marks a video generation as complete and deletes media from storage.
    /// </summary>
    /// <param name="request">The complete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    public async Task<CompleteVideoResponse> CompleteVideoAsync(
        CompleteVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.QueueId))
            throw new ArgumentException("QueueId is required", nameof(request));

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        try
        {
            var response = await PostAsync<CompleteVideoRequest, CompleteVideoResponse>(
                "video/complete",
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
            throw new VeniceAIException($"Unexpected error during video complete: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Marks a video generation as complete by queue ID and model.
    /// </summary>
    /// <param name="model">The model used for generation.</param>
    /// <param name="queueId">The queue ID from the queue response.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    public Task<CompleteVideoResponse> CompleteVideoAsync(
        string model,
        string queueId,
        CancellationToken cancellationToken = default)
    {
        return CompleteVideoAsync(new CompleteVideoRequest
        {
            Model = model,
            QueueId = queueId
        }, cancellationToken);
    }

    /// <summary>
    /// Gets a price quote for a video generation request.
    /// </summary>
    /// <param name="request">The video generation request to quote.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The price quote.</returns>
    public async Task<QuoteVideoResponse> QuoteVideoAsync(
        QueueVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var response = await PostAsync<QueueVideoRequest, QuoteVideoResponse>(
                "video/quote",
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
            throw new VeniceAIException($"Unexpected error during video quote: {ex.Message}", ex);
        }
    }
}
