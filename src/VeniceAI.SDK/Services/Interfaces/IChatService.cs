using VeniceAI.SDK.Models.Chat;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for chat completion services.
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Creates a chat completion.
    /// </summary>
    /// <param name="request">The chat completion request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The chat completion response.</returns>
    Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a streaming chat completion.
    /// </summary>
    /// <param name="request">The chat completion request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of chat completion chunks.</returns>
    IAsyncEnumerable<ChatCompletionResponse> CreateChatCompletionStreamAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default);
}
