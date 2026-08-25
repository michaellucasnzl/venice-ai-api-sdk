using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Responses;

/// <summary>
/// Request for the Responses API (Alpha).
/// </summary>
public class ResponsesRequest
{
    /// <summary>
    /// The ID of the model to use. E2EE-capable models are not supported on /api/v1/responses.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(TextModelJsonConverter))]
    public TextModel Model { get; set; }

    /// <summary>
    /// The input to the model: a string or a list of input items (messages, images, etc.).
    /// </summary>
    [JsonPropertyName("input")]
    public object? Input { get; set; }

    /// <summary>
    /// Maximum number of tokens to generate.
    /// </summary>
    [JsonPropertyName("max_output_tokens")]
    public int? MaxOutputTokens { get; set; }

    /// <summary>
    /// Sampling temperature, between 0 and 2.
    /// </summary>
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }

    /// <summary>
    /// Nucleus sampling probability mass.
    /// </summary>
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }

    /// <summary>
    /// Whether to stream back partial progress.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }

    /// <summary>
    /// Configuration for reasoning behavior on supported models.
    /// </summary>
    [JsonPropertyName("reasoning")]
    public ResponsesReasoningConfig? Reasoning { get; set; }

    /// <summary>
    /// Enable web search for this request.
    /// </summary>
    [JsonPropertyName("web_search")]
    public bool? WebSearch { get; set; }

    /// <summary>
    /// A list of tools the model may call.
    /// </summary>
    [JsonPropertyName("tools")]
    public List<object>? Tools { get; set; }

    /// <summary>
    /// Controls which tool is called by the model.
    /// </summary>
    [JsonPropertyName("tool_choice")]
    public object? ToolChoice { get; set; }

    /// <summary>
    /// Anthropic beta parameter for Claude Fable 5 server-side refusal fallback.
    /// </summary>
    [JsonPropertyName("fallbacks")]
    public List<FallbackModel>? Fallbacks { get; set; }

    /// <summary>
    /// OpenAI-compatible parameter specifying additional data to include in the response.
    /// </summary>
    [JsonPropertyName("include")]
    public List<string>? Include { get; set; }

    /// <summary>
    /// Unique parameters to Venice's API implementation.
    /// </summary>
    [JsonPropertyName("venice_parameters")]
    public ResponsesVeniceParameters? VeniceParameters { get; set; }
}

/// <summary>
/// Configuration for reasoning behavior on the Responses API.
/// </summary>
public class ResponsesReasoningConfig
{
    /// <summary>
    /// Controls reasoning effort level for supported models (none, minimal, low, medium, high, xhigh, max).
    /// </summary>
    [JsonPropertyName("effort")]
    public string? Effort { get; set; }

    /// <summary>
    /// Controls reasoning summary format (auto, concise, detailed).
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}

/// <summary>
/// Venice-specific parameters for the Responses API.
/// </summary>
public class ResponsesVeniceParameters
{
    /// <summary>
    /// The character slug of a public Venice character.
    /// </summary>
    [JsonPropertyName("character_slug")]
    public string? CharacterSlug { get; set; }

    /// <summary>
    /// Enable end-to-end encryption for E2EE-capable models.
    /// </summary>
    [JsonPropertyName("enable_e2ee")]
    public bool? EnableE2ee { get; set; }

    /// <summary>
    /// Enable web search for this request (off, on, auto).
    /// </summary>
    [JsonPropertyName("enable_web_search")]
    public string? EnableWebSearch { get; set; }

    /// <summary>
    /// Enable Venice web scraping of URLs in the latest user message.
    /// </summary>
    [JsonPropertyName("enable_web_scraping")]
    public bool? EnableWebScraping { get; set; }

    /// <summary>
    /// Request that the LLM cite its sources.
    /// </summary>
    [JsonPropertyName("enable_web_citations")]
    public bool? EnableWebCitations { get; set; }

    /// <summary>
    /// Whether to include the Venice supplied system prompts.
    /// </summary>
    [JsonPropertyName("include_venice_system_prompt")]
    public bool? IncludeVeniceSystemPrompt { get; set; }

    /// <summary>
    /// Include search results in the stream as the first emitted chunk.
    /// </summary>
    [JsonPropertyName("include_search_results_in_stream")]
    public bool? IncludeSearchResultsInStream { get; set; }
}

/// <summary>
/// Response from the Responses API endpoint.
/// </summary>
public class ResponsesResponse : BaseResponse
{
    /// <summary>
    /// Unique identifier for the response.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The object type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "response";

    /// <summary>
    /// Unix timestamp of when the response was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }

    /// <summary>
    /// The model used for the response.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The status of the response (completed, failed, in_progress, cancelled).
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The output items generated by the model.
    /// </summary>
    [JsonPropertyName("output")]
    public List<ResponseOutputItem> Output { get; set; } = new();

    /// <summary>
    /// Token usage statistics.
    /// </summary>
    [JsonPropertyName("usage")]
    public ResponsesUsage? Usage { get; set; }

    /// <summary>
    /// Error information if the response failed.
    /// </summary>
    [JsonPropertyName("error")]
    public ResponsesError? ResponseError { get; set; }
}

/// <summary>
/// Output item generated by the model in a Responses API response.
/// </summary>
public class ResponseOutputItem
{
    /// <summary>
    /// The type of output item (reasoning, message, function_call, web_search_call).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The identifier of the output item.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The status of the output item.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The role of the output item (assistant).
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    /// <summary>
    /// The content items of a message output.
    /// </summary>
    [JsonPropertyName("content")]
    public List<ResponseContentItem>? Content { get; set; }

    /// <summary>
    /// The reasoning summary.
    /// </summary>
    [JsonPropertyName("summary")]
    public List<string>? Summary { get; set; }

    /// <summary>
    /// The function call identifier.
    /// </summary>
    [JsonPropertyName("call_id")]
    public string? CallId { get; set; }

    /// <summary>
    /// The name of the function called.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The JSON arguments of the function call.
    /// </summary>
    [JsonPropertyName("arguments")]
    public string? Arguments { get; set; }
}

/// <summary>
/// A content item within a message output.
/// </summary>
public class ResponseContentItem
{
    /// <summary>
    /// The type of content item (output_text).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The text content.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Annotations associated with the content item (e.g., URL citations).
    /// </summary>
    [JsonPropertyName("annotations")]
    public List<ResponseAnnotation>? Annotations { get; set; }
}

/// <summary>
/// Annotation associated with a content item (e.g., URL citation).
/// </summary>
public class ResponseAnnotation
{
    /// <summary>
    /// The type of annotation (url_citation).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The cited URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// The title of the cited page.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Start index of the citation in the text.
    /// </summary>
    [JsonPropertyName("start_index")]
    public int? StartIndex { get; set; }

    /// <summary>
    /// End index of the citation in the text.
    /// </summary>
    [JsonPropertyName("end_index")]
    public int? EndIndex { get; set; }
}

/// <summary>
/// Token usage statistics for a Responses API response.
/// </summary>
public class ResponsesUsage
{
    /// <summary>
    /// Number of input tokens.
    /// </summary>
    [JsonPropertyName("input_tokens")]
    public int InputTokens { get; set; }

    /// <summary>
    /// Details of input token usage.
    /// </summary>
    [JsonPropertyName("input_tokens_details")]
    public InputTokensDetails? InputTokensDetails { get; set; }

    /// <summary>
    /// Number of output tokens.
    /// </summary>
    [JsonPropertyName("output_tokens")]
    public int OutputTokens { get; set; }

    /// <summary>
    /// Details of output token usage.
    /// </summary>
    [JsonPropertyName("output_tokens_details")]
    public OutputTokensDetails? OutputTokensDetails { get; set; }

    /// <summary>
    /// Total number of tokens used.
    /// </summary>
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}

/// <summary>
/// Details of input token usage.
/// </summary>
public class InputTokensDetails
{
    /// <summary>
    /// Number of cached tokens.
    /// </summary>
    [JsonPropertyName("cached_tokens")]
    public int? CachedTokens { get; set; }
}

/// <summary>
/// Details of output token usage.
/// </summary>
public class OutputTokensDetails
{
    /// <summary>
    /// Number of reasoning tokens.
    /// </summary>
    [JsonPropertyName("reasoning_tokens")]
    public int? ReasoningTokens { get; set; }
}

/// <summary>
/// Error information if a Responses API request failed.
/// </summary>
public class ResponsesError
{
    /// <summary>
    /// The error code.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
