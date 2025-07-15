using System.Runtime.CompilerServices;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Services.Interfaces;
using ChatCompletionRequest = VeniceAI.SDK.Models.Chat.ChatCompletionRequest;
using ChatCompletionResponse = VeniceAI.SDK.Models.Chat.ChatCompletionResponse;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for chat completions using the Venice AI API.
/// </summary>
public class ChatService : IChatService
{
    private readonly IVeniceAIGeneratedClient _generatedClient;

    /// <summary>
    /// Initializes a new instance of the ChatService class.
    /// </summary>
    /// <param name="generatedClient">The generated Venice AI client.</param>
    public ChatService(IVeniceAIGeneratedClient generatedClient)
    {
        _generatedClient = generatedClient ?? throw new ArgumentNullException(nameof(generatedClient));
    }

    /// <summary>
    /// Creates a chat completion.
    /// </summary>
    /// <param name="request">The chat completion request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The chat completion response.</returns>
    public async Task<ChatCompletionResponse> CreateChatCompletionAsync(
        ChatCompletionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (request.Messages == null || !request.Messages.Any())
            throw new ArgumentException("Messages are required", nameof(request));

        try
        {
            // Convert SDK request to generated client format
            var generatedRequest = request.ToGeneratedRequest();

            // Call the generated client
            var response = await _generatedClient.CreateChatCompletionAsync(
                "gzip, br", // Accept compression
                generatedRequest,
                cancellationToken);

            // Convert back to SDK format
            return response.ToSdkResponse();
        }
        catch (ApiException ex)
        {
            throw VeniceAIException.FromApiException(ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during chat completion: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates a streaming chat completion.
    /// </summary>
    /// <param name="request">The chat completion request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of chat completion chunks.</returns>
    public IAsyncEnumerable<ChatCompletionResponse> CreateChatCompletionStreamAsync(
        ChatCompletionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (request.Messages == null || !request.Messages.Any())
            throw new ArgumentException("Messages are required", nameof(request));

        return CreateChatCompletionStreamInternalAsync(request, cancellationToken);
    }

    private async IAsyncEnumerable<ChatCompletionResponse> CreateChatCompletionStreamInternalAsync(
        ChatCompletionRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // Set streaming to true
        request.Stream = true;

        // Convert SDK request to generated client format
        var generatedRequest = request.ToGeneratedRequest();

        // For streaming, we'll need to implement Server-Sent Events parsing
        // This is a placeholder that calls the non-streaming version for now
        // TODO: Implement proper streaming support with SSE parsing
        ChatCompletionResponse response;
        try
        {
            var generatedResponse = await _generatedClient.CreateChatCompletionAsync(
                "gzip, br",
                generatedRequest,
                cancellationToken);

            response = generatedResponse.ToSdkResponse();
        }
        catch (ApiException ex)
        {
            throw VeniceAIException.FromApiException(ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during streaming chat completion: {ex.Message}", ex);
        }

        yield return response;
    }
}
