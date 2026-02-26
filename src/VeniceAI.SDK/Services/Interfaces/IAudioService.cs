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
}
