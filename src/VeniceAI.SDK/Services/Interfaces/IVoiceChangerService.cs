using VeniceAI.SDK.Models.Audio;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for the voice changer (speech-to-speech) service.
/// </summary>
public interface IVoiceChangerService
{
    /// <summary>
    /// Queues a voice conversion supplying the source recording as a URL.
    /// </summary>
    /// <param name="request">The queue request. <see cref="QueueVoiceChangerRequest.AudioUrl"/> is required.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    Task<QueueVoiceChangerResponse> QueueVoiceChangerAsync(QueueVoiceChangerRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queues a voice conversion supplying the source recording as an uploaded file.
    /// </summary>
    /// <param name="request">The queue request. <see cref="QueueVoiceChangerRequest.File"/> is required.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    Task<QueueVoiceChangerResponse> QueueVoiceChangerWithFileAsync(QueueVoiceChangerRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Polls a queued voice conversion. Returns converted audio bytes when finished,
    /// or a "PROCESSING" status while still running.
    /// </summary>
    /// <param name="request">The retrieve request with the queue ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The conversion status or converted audio.</returns>
    Task<RetrieveVoiceChangerResponse> RetrieveVoiceChangerAsync(RetrieveVoiceChangerRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the provider-held media for a finished voice conversion.
    /// </summary>
    /// <param name="request">The complete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    Task<CompleteVoiceChangerResponse> CompleteVoiceChangerAsync(CompleteVoiceChangerRequest request, CancellationToken cancellationToken = default);
}
