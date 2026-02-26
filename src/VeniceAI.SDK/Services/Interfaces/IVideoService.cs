using VeniceAI.SDK.Models.Video;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for video generation services.
/// </summary>
public interface IVideoService
{
    /// <summary>
    /// Queues a new video generation request.
    /// </summary>
    /// <param name="request">The video generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    Task<QueueVideoResponse> QueueVideoAsync(QueueVideoRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the status and result of a video generation request.
    /// </summary>
    /// <param name="request">The retrieve request with queue ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The video generation status and result.</returns>
    Task<RetrieveVideoResponse> RetrieveVideoAsync(RetrieveVideoRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a video by queue ID and model.
    /// </summary>
    /// <param name="model">The model used for generation.</param>
    /// <param name="queueId">The queue ID from the queue response.</param>
    /// <param name="deleteMediaOnCompletion">Whether to delete media after retrieval.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The video generation status and result.</returns>
    Task<RetrieveVideoResponse> RetrieveVideoAsync(string model, string queueId, bool deleteMediaOnCompletion = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a video generation as complete and deletes media from storage.
    /// </summary>
    /// <param name="request">The complete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    Task<CompleteVideoResponse> CompleteVideoAsync(CompleteVideoRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a video generation as complete by queue ID and model.
    /// </summary>
    /// <param name="model">The model used for generation.</param>
    /// <param name="queueId">The queue ID from the queue response.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    Task<CompleteVideoResponse> CompleteVideoAsync(string model, string queueId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a price quote for a video generation request.
    /// </summary>
    /// <param name="request">The video generation request to quote.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The price quote.</returns>
    Task<QuoteVideoResponse> QuoteVideoAsync(QueueVideoRequest request, CancellationToken cancellationToken = default);
}
