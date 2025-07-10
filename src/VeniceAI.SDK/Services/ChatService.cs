using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for chat completion operations.
/// </summary>
public class ChatService : BaseService, IChatService
{
    /// <summary>
    /// Initializes a new instance of the ChatService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    public ChatService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger<ChatService> logger)
        : base(httpClient, options, logger)
    {
    }

    /// <inheritdoc />
    public async Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Stream == true)
        {
            throw new InvalidOperationException("Use CreateChatCompletionStreamAsync for streaming requests.");
        }

        request.Stream = false;
        return await SendPostRequestAsync<ChatCompletionResponse>("/chat/completions", request, cancellationToken);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<ChatCompletionResponse> CreateChatCompletionStreamAsync(
        ChatCompletionRequest request,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        request.Stream = true;
        
        await foreach (var chunk in SendStreamingPostRequestAsync<ChatCompletionResponse>("/chat/completions", request, cancellationToken))
        {
            yield return chunk;
        }
    }
}
