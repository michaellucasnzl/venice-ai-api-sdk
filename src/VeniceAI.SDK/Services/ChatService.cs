using System.Runtime.CompilerServices;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Chat;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for chat completions using the Venice AI API.
/// </summary>
public class ChatService : BaseHttpService, IChatService
{
    /// <summary>
    /// Initializes a new instance of the ChatService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    public ChatService(HttpClient httpClient, string apiKey) : base(httpClient, apiKey)
    {
    }

    /// <summary>
    /// Helper method to extract content from different message types.
    /// </summary>
    /// <param name="message">The message to extract content from.</param>
    /// <returns>The content string.</returns>
    private string GetMessageContent(ChatMessage message)
    {
        return message.Content switch
        {
            string str => str,
            List<MessageContent> contentList =>
                contentList.FirstOrDefault(c => c.Type == "text")?.Text ?? string.Empty,
            _ => string.Empty
        };
    }

    /// <summary>
    /// Creates a chat completion request to the Venice AI API.
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
            // Create a simple request object matching the Postman examples
            var apiRequest = new
            {
                model = request.Model,
                messages = request.Messages.Select(m => new
                {
                    role = m.Role,
                    content = GetMessageContent(m)
                }).ToList(),
                stream = request.Stream ?? false
            };

            // Call the API
            var apiResponse = await PostAsync<object, ChatCompletionApiResponse>(
                "chat/completions",
                apiRequest,
                cancellationToken);

            // Convert to SDK response format
            return new ChatCompletionResponse
            {
                Id = apiResponse.Id ?? string.Empty,
                Object = apiResponse.Object ?? string.Empty,
                Created = apiResponse.Created,
                Model = apiResponse.Model ?? string.Empty,
                Choices = apiResponse.Choices?.Select(c => new ChatChoice
                {
                    Index = c.Index,
                    Message = new AssistantMessage { Content = c.Message?.Content ?? string.Empty },
                    FinishReason = c.FinishReason ?? string.Empty
                }).ToList() ?? new List<ChatChoice>()
            };
        }
        catch (VeniceAIException)
        {
            throw;
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

        var streamResponses = PostStreamAsync<ChatCompletionRequest>("chat/completions", request, cancellationToken);

        await foreach (var streamResponse in streamResponses)
        {
            if (streamResponse.Choices?.Count > 0)
            {
                var choice = streamResponse.Choices[0];
                if (choice.Delta?.Content != null)
                {
                    yield return new ChatCompletionResponse
                    {
                        Id = streamResponse.Id ?? string.Empty,
                        Object = streamResponse.Object ?? "chat.completion.chunk",
                        Created = streamResponse.Created,
                        Model = streamResponse.Model ?? string.Empty,
                        Choices = new List<ChatChoice>
                        {
                            new ChatChoice
                            {
                                Index = choice.Index,
                                Message = new AssistantMessage
                                {
                                    Content = choice.Delta.Content
                                },
                                FinishReason = choice.FinishReason
                            }
                        }
                    };
                }
            }
        }
    }
}

/// <summary>
/// Internal API response classes for chat completions.
/// These match the actual Venice AI API response format.
/// </summary>
internal class ChatCompletionApiResponse
{
    public string? Id { get; set; }
    public string? Object { get; set; }
    public long Created { get; set; }
    public string? Model { get; set; }
    public List<ChatCompletionChoice>? Choices { get; set; }
}

internal class ChatCompletionChoice
{
    public int Index { get; set; }
    public ChatCompletionMessage? Message { get; set; }
    public string? FinishReason { get; set; }
}

internal class ChatCompletionMessage
{
    public string? Role { get; set; }
    public string? Content { get; set; }
}
