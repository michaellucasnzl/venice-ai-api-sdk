using System.Text.Json.Serialization;

namespace VeniceAI.SDK.Models.Chat;

/// <summary>
/// Content object for chat messages.
/// </summary>
public class MessageContent
{
    /// <summary>
    /// The type of content.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The text content (for text type).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// The image URL (for image_url type).
    /// </summary>
    [JsonPropertyName("image_url")]
    public ImageUrl? ImageUrl { get; set; }
}

/// <summary>
/// Image URL information.
/// </summary>
public class ImageUrl
{
    /// <summary>
    /// The URL of the image.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

/// <summary>
/// Base class for chat messages.
/// </summary>
public abstract class ChatMessage
{
    /// <summary>
    /// The role of the message sender.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The content of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public object? Content { get; set; }

    /// <summary>
    /// Optional name for the message sender.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

/// <summary>
/// User message in a chat conversation.
/// </summary>
public class UserMessage : ChatMessage
{
    /// <summary>
    /// Initializes a new instance of the UserMessage class.
    /// </summary>
    public UserMessage()
    {
        Role = "user";
    }

    /// <summary>
    /// Initializes a new instance of the UserMessage class with text content.
    /// </summary>
    /// <param name="content">The text content of the message.</param>
    public UserMessage(string content)
    {
        Role = "user";
        Content = content;
    }

    /// <summary>
    /// Initializes a new instance of the UserMessage class with structured content.
    /// </summary>
    /// <param name="content">The structured content of the message.</param>
    public UserMessage(List<MessageContent> content)
    {
        Role = "user";
        Content = content;
    }
}

/// <summary>
/// System message in a chat conversation.
/// </summary>
public class SystemMessage : ChatMessage
{
    /// <summary>
    /// Initializes a new instance of the SystemMessage class.
    /// </summary>
    public SystemMessage()
    {
        Role = "system";
    }

    /// <summary>
    /// Initializes a new instance of the SystemMessage class with text content.
    /// </summary>
    /// <param name="content">The text content of the message.</param>
    public SystemMessage(string content)
    {
        Role = "system";
        Content = content;
    }
}

/// <summary>
/// Assistant message in a chat conversation.
/// </summary>
public class AssistantMessage : ChatMessage
{
    /// <summary>
    /// Initializes a new instance of the AssistantMessage class.
    /// </summary>
    public AssistantMessage()
    {
        Role = "assistant";
    }

    /// <summary>
    /// Reasoning content for thinking models.
    /// </summary>
    [JsonPropertyName("reasoning_content")]
    public string? ReasoningContent { get; set; }

    /// <summary>
    /// Tool calls made by the assistant.
    /// </summary>
    [JsonPropertyName("tool_calls")]
    public List<ToolCall>? ToolCalls { get; set; }
}

/// <summary>
/// Tool message in a chat conversation.
/// </summary>
public class ToolMessage : ChatMessage
{
    /// <summary>
    /// Initializes a new instance of the ToolMessage class.
    /// </summary>
    public ToolMessage()
    {
        Role = "tool";
    }

    /// <summary>
    /// The ID of the tool call this message responds to.
    /// </summary>
    [JsonPropertyName("tool_call_id")]
    public string ToolCallId { get; set; } = string.Empty;

    /// <summary>
    /// Reasoning content for thinking models.
    /// </summary>
    [JsonPropertyName("reasoning_content")]
    public string? ReasoningContent { get; set; }

    /// <summary>
    /// Tool calls made by the tool.
    /// </summary>
    [JsonPropertyName("tool_calls")]
    public List<ToolCall>? ToolCalls { get; set; }
}

/// <summary>
/// Tool call information.
/// </summary>
public class ToolCall
{
    /// <summary>
    /// The ID of the tool call.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of tool call.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Function call details.
    /// </summary>
    [JsonPropertyName("function")]
    public FunctionCall? Function { get; set; }
}

/// <summary>
/// Function call details.
/// </summary>
public class FunctionCall
{
    /// <summary>
    /// The name of the function.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The arguments for the function call.
    /// </summary>
    [JsonPropertyName("arguments")]
    public string Arguments { get; set; } = string.Empty;
}
