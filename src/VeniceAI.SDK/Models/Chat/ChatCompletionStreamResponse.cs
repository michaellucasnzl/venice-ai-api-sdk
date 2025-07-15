using System.Text.Json.Serialization;

namespace VeniceAI.SDK.Models.Chat;

/// <summary>
/// Response for chat completion streaming chunks.
/// These match the actual Venice AI API streaming response format.
/// </summary>
public class ChatCompletionStreamResponse
{
    /// <summary>
    /// Unique identifier for the chat completion.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The object type, which is always "chat.completion.chunk".
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// The Unix timestamp (in seconds) of when the chat completion was created.
    /// </summary>
    [JsonPropertyName("created")]
    public long Created { get; set; }

    /// <summary>
    /// The model used for the chat completion.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// A list of streaming chat completion choices.
    /// </summary>
    [JsonPropertyName("choices")]
    public List<ChatCompletionStreamChoice>? Choices { get; set; }
}

/// <summary>
/// A streaming chat completion choice.
/// </summary>
public class ChatCompletionStreamChoice
{
    /// <summary>
    /// The index of the choice in the list of choices.
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// The delta containing the incremental content.
    /// </summary>
    [JsonPropertyName("delta")]
    public ChatCompletionDelta? Delta { get; set; }

    /// <summary>
    /// The reason the model stopped generating tokens.
    /// </summary>
    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; set; }
}

/// <summary>
/// Delta containing incremental content for streaming responses.
/// </summary>
public class ChatCompletionDelta
{
    /// <summary>
    /// The role of the message (only sent in first chunk).
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    /// <summary>
    /// The content of the message (incremental).
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
