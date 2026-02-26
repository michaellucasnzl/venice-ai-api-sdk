using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Chat;

/// <summary>
/// Request for chat completion.
/// </summary>
public class ChatCompletionRequest
{
    /// <summary>
    /// A list of messages comprising the conversation so far.
    /// </summary>
    [JsonPropertyName("messages")]
    public List<ChatMessage> Messages { get; set; } = new();

    /// <summary>
    /// The model to use for the chat completion.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(TextModelJsonConverter))]
    public TextModel Model { get; set; }

    /// <summary>
    /// The maximum number of tokens that can be generated in the chat completion.
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; set; }

    /// <summary>
    /// An upper bound for the number of tokens that can be generated for a completion.
    /// </summary>
    [JsonPropertyName("max_completion_tokens")]
    public int? MaxCompletionTokens { get; set; }

    /// <summary>
    /// What sampling temperature to use, between 0 and 2.
    /// </summary>
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }

    /// <summary>
    /// An alternative to sampling with temperature, called nucleus sampling.
    /// </summary>
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }

    /// <summary>
    /// The number of highest probability vocabulary tokens to keep for top-k-filtering.
    /// </summary>
    [JsonPropertyName("top_k")]
    public int? TopK { get; set; }

    /// <summary>
    /// Sets a minimum probability threshold for token selection.
    /// </summary>
    [JsonPropertyName("min_p")]
    public double? MinP { get; set; }

    /// <summary>
    /// How many chat completion choices to generate for each input message.
    /// </summary>
    [JsonPropertyName("n")]
    public int? N { get; set; }

    /// <summary>
    /// Whether to stream back partial progress.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }

    /// <summary>
    /// Stream options for streaming responses.
    /// </summary>
    [JsonPropertyName("stream_options")]
    public StreamOptions? StreamOptions { get; set; }

    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens.
    /// </summary>
    [JsonPropertyName("stop")]
    public object? Stop { get; set; }

    /// <summary>
    /// Array of token IDs where the API will stop generating further tokens.
    /// </summary>
    [JsonPropertyName("stop_token_ids")]
    public List<int>? StopTokenIds { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency.
    /// </summary>
    [JsonPropertyName("frequency_penalty")]
    public double? FrequencyPenalty { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far.
    /// </summary>
    [JsonPropertyName("presence_penalty")]
    public double? PresencePenalty { get; set; }

    /// <summary>
    /// The parameter for repetition penalty. 1.0 means no penalty.
    /// </summary>
    [JsonPropertyName("repetition_penalty")]
    public double? RepetitionPenalty { get; set; }

    /// <summary>
    /// The random seed used to generate the response.
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }

    /// <summary>
    /// Whether to include log probabilities in the response.
    /// </summary>
    [JsonPropertyName("logprobs")]
    public bool? Logprobs { get; set; }

    /// <summary>
    /// The number of highest probability tokens to return for each token position.
    /// </summary>
    [JsonPropertyName("top_logprobs")]
    public int? TopLogprobs { get; set; }

    /// <summary>
    /// Minimum temperature value for dynamic temperature scaling.
    /// </summary>
    [JsonPropertyName("min_temp")]
    public double? MinTemp { get; set; }

    /// <summary>
    /// Maximum temperature value for dynamic temperature scaling.
    /// </summary>
    [JsonPropertyName("max_temp")]
    public double? MaxTemp { get; set; }

    /// <summary>
    /// Configuration for reasoning behavior on supported models.
    /// </summary>
    [JsonPropertyName("reasoning")]
    public ReasoningConfig? Reasoning { get; set; }

    /// <summary>
    /// OpenAI-compatible parameter to control reasoning effort level for supported models (low, medium, high).
    /// Takes precedence over reasoning.effort if both are provided.
    /// </summary>
    [JsonPropertyName("reasoning_effort")]
    public string? ReasoningEffort { get; set; }

    /// <summary>
    /// When supplied, this field may be used to optimize conversation routing to improve cache performance.
    /// </summary>
    [JsonPropertyName("prompt_cache_key")]
    public string? PromptCacheKey { get; set; }

    /// <summary>
    /// Format in which the response should be returned.
    /// </summary>
    [JsonPropertyName("response_format")]
    public ResponseFormat? ResponseFormat { get; set; }

    /// <summary>
    /// A list of tools the model may call.
    /// </summary>
    [JsonPropertyName("tools")]
    public List<Tool>? Tools { get; set; }

    /// <summary>
    /// Controls which tool is called by the model.
    /// </summary>
    [JsonPropertyName("tool_choice")]
    public object? ToolChoice { get; set; }

    /// <summary>
    /// Whether to enable parallel function calling during tool use.
    /// </summary>
    [JsonPropertyName("parallel_tool_calls")]
    public bool? ParallelToolCalls { get; set; }

    /// <summary>
    /// Unique parameters to Venice's API implementation.
    /// </summary>
    [JsonPropertyName("venice_parameters")]
    public VeniceParameters? VeniceParameters { get; set; }

    /// <summary>
    /// This field is discarded on the request but is supported for compatibility.
    /// </summary>
    [JsonPropertyName("user")]
    public string? User { get; set; }
}

/// <summary>
/// Stream options for streaming responses.
/// </summary>
public class StreamOptions
{
    /// <summary>
    /// Whether to include usage information in the stream.
    /// </summary>
    [JsonPropertyName("include_usage")]
    public bool? IncludeUsage { get; set; }
}

/// <summary>
/// Response format specification.
/// </summary>
public class ResponseFormat
{
    /// <summary>
    /// The type of response format.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// JSON schema for structured responses.
    /// </summary>
    [JsonPropertyName("json_schema")]
    public Dictionary<string, object>? JsonSchema { get; set; }
}

/// <summary>
/// Tool definition.
/// </summary>
public class Tool
{
    /// <summary>
    /// The type of tool.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "function";

    /// <summary>
    /// Function definition.
    /// </summary>
    [JsonPropertyName("function")]
    public FunctionDefinition Function { get; set; } = new();

    /// <summary>
    /// Optional tool ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Function definition for tools.
/// </summary>
public class FunctionDefinition
{
    /// <summary>
    /// The name of the function.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the function.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The parameters schema for the function.
    /// </summary>
    [JsonPropertyName("parameters")]
    public Dictionary<string, object>? Parameters { get; set; }
}

/// <summary>
/// Venice-specific parameters.
/// </summary>
public class VeniceParameters
{
    /// <summary>
    /// The character slug of a public Venice character.
    /// </summary>
    [JsonPropertyName("character_slug")]
    public string? CharacterSlug { get; set; }

    /// <summary>
    /// Strip thinking blocks from the response (for models using legacy think tag format).
    /// </summary>
    [JsonPropertyName("strip_thinking_response")]
    public bool? StripThinkingResponse { get; set; }

    /// <summary>
    /// On supported reasoning models, will disable thinking and strip the think blocks.
    /// </summary>
    [JsonPropertyName("disable_thinking")]
    public bool? DisableThinking { get; set; }

    /// <summary>
    /// Enable web search for this request (off, on, auto). Auto enables based on model's discretion.
    /// Additional usage-based pricing applies.
    /// </summary>
    [JsonPropertyName("enable_web_search")]
    public string? EnableWebSearch { get; set; }

    /// <summary>
    /// Enable web scraping of URLs detected in the user message. Scraped content augments responses.
    /// Additional usage-based pricing applies.
    /// </summary>
    [JsonPropertyName("enable_web_scraping")]
    public bool? EnableWebScraping { get; set; }

    /// <summary>
    /// When web search is enabled, request that the LLM cite its sources using [REF]0[/REF] format.
    /// </summary>
    [JsonPropertyName("enable_web_citations")]
    public bool? EnableWebCitations { get; set; }

    /// <summary>
    /// Experimental: Include search results in the stream as the first emitted chunk.
    /// </summary>
    [JsonPropertyName("include_search_results_in_stream")]
    public bool? IncludeSearchResultsInStream { get; set; }

    /// <summary>
    /// Surface search results in an OpenAI-compatible tool call named venice_web_search_documents for LangChain integration.
    /// </summary>
    [JsonPropertyName("return_search_results_as_documents")]
    public bool? ReturnSearchResultsAsDocuments { get; set; }

    /// <summary>
    /// Whether to include Venice's default system prompts alongside specified system prompts.
    /// </summary>
    [JsonPropertyName("include_venice_system_prompt")]
    public bool? IncludeVeniceSystemPrompt { get; set; }
}

/// <summary>
/// Configuration for reasoning behavior on supported models.
/// </summary>
public class ReasoningConfig
{
    /// <summary>
    /// The effort level for reasoning (low, medium, high).
    /// </summary>
    [JsonPropertyName("effort")]
    public string? Effort { get; set; }
}
