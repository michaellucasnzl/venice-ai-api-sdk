using VeniceAI.SDK.Models.Audio;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for audio services.
/// </summary>
public interface IAudioService
{
    /// <summary>
    /// Creates speech from text.
    /// </summary>
    /// <param name="request">The speech creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The speech response.</returns>
    Task<CreateSpeechResponse> CreateSpeechAsync(CreateSpeechRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates streaming speech from text.
    /// </summary>
    /// <param name="request">The speech creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of audio chunks.</returns>
    IAsyncEnumerable<byte[]> CreateSpeechStreamAsync(CreateSpeechRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queues a new audio generation request.
    /// </summary>
    /// <param name="request">The audio generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response with a queue ID for tracking.</returns>
    Task<QueueAudioResponse> QueueAudioAsync(QueueAudioRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the status and result of an audio generation request.
    /// </summary>
    /// <param name="request">The retrieve request with queue ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The audio generation status and result.</returns>
    Task<RetrieveAudioResponse> RetrieveAudioAsync(RetrieveAudioRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks an audio generation as complete and deletes media from storage.
    /// </summary>
    /// <param name="request">The complete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completion response.</returns>
    Task<CompleteAudioResponse> CompleteAudioAsync(CompleteAudioRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a price quote for an audio generation request.
    /// </summary>
    /// <param name="request">The quote request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The price quote.</returns>
    Task<QuoteAudioResponse> QuoteAudioAsync(QuoteAudioRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Transcribes audio to text.
    /// </summary>
    /// <param name="request">The transcription request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The transcription response.</returns>
    Task<CreateTranscriptionResponse> TranscribeAudioAsync(CreateTranscriptionRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a cloned voice from an audio sample.
    /// </summary>
    /// <param name="request">The cloned voice request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cloned voice response.</returns>
    Task<CreateClonedVoiceResponse> CreateClonedVoiceAsync(CreateClonedVoiceRequest request, CancellationToken cancellationToken = default);
}
