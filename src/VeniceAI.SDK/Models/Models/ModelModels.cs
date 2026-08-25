using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Models;

/// <summary>
/// Response from models API.
/// </summary>
public class ModelsResponse : BaseResponse
{
    /// <summary>
    /// The object type, which is always "list".
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "list";

    /// <summary>
    /// List of available models.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Model> Data { get; set; } = new();
}

/// <summary>
/// Model information.
/// </summary>
public class Model
{
    /// <summary>
    /// Model ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Object type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "model";

    /// <summary>
    /// Release date on Venice API.
    /// </summary>
    [JsonPropertyName("created")]
    public long Created { get; set; }

    /// <summary>
    /// Who runs the model.
    /// </summary>
    [JsonPropertyName("owned_by")]
    public string OwnedBy { get; set; } = "venice.ai";

    /// <summary>
    /// Model type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Model specification details.
    /// </summary>
    [JsonPropertyName("model_spec")]
    public ModelSpec ModelSpec { get; set; } = new();
}

/// <summary>
/// Model specification details.
/// </summary>
public class ModelSpec
{
    /// <summary>
    /// The name of the model.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The source of the model.
    /// </summary>
    [JsonPropertyName("modelSource")]
    public string? ModelSource { get; set; }

    /// <summary>
    /// Is this model presently offline?
    /// </summary>
    [JsonPropertyName("offline")]
    public bool Offline { get; set; }

    /// <summary>
    /// Is this model in beta?
    /// </summary>
    [JsonPropertyName("beta")]
    public bool Beta { get; set; }

    /// <summary>
    /// The context length supported by the model.
    /// </summary>
    [JsonPropertyName("availableContextTokens")]
    public int? AvailableContextTokens { get; set; }

    /// <summary>
    /// The maximum number of completion tokens the model can generate.
    /// </summary>
    [JsonPropertyName("maxCompletionTokens")]
    public int? MaxCompletionTokens { get; set; }

    /// <summary>
    /// The maximum number of input tokens supported by the model.
    /// </summary>
    [JsonPropertyName("maxInputTokens")]
    public int? MaxInputTokens { get; set; }

    /// <summary>
    /// The embedding dimensions for embedding models.
    /// </summary>
    [JsonPropertyName("embeddingDimensions")]
    public int? EmbeddingDimensions { get; set; }

    /// <summary>
    /// Whether the model supports custom embedding dimensions.
    /// </summary>
    [JsonPropertyName("supportsCustomDimensions")]
    public bool? SupportsCustomDimensions { get; set; }

    /// <summary>
    /// Whether the model supports style references.
    /// </summary>
    [JsonPropertyName("supportsStyleReferences")]
    public bool? SupportsStyleReferences { get; set; }

    /// <summary>
    /// The privacy mode of the model (private, anonymized).
    /// </summary>
    [JsonPropertyName("privacy")]
    public string? Privacy { get; set; }

    /// <summary>
    /// The description of the model.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Whether this model is uncensored.
    /// </summary>
    [JsonPropertyName("uncensored")]
    public bool? Uncensored { get; set; }

    /// <summary>
    /// Deprecation details for models being retired.
    /// </summary>
    [JsonPropertyName("deprecation")]
    public ModelDeprecation? Deprecation { get; set; }

    /// <summary>
    /// Country codes where this model is intended to be available.
    /// </summary>
    [JsonPropertyName("regionRestrictions")]
    public List<string>? RegionRestrictions { get; set; }

    /// <summary>
    /// The default voice for TTS models.
    /// </summary>
    [JsonPropertyName("default_voice")]
    public string? DefaultVoice { get; set; }

    /// <summary>
    /// The default format for the model.
    /// </summary>
    [JsonPropertyName("default_format")]
    public string? DefaultFormat { get; set; }

    /// <summary>
    /// The default duration for audio/video models.
    /// </summary>
    [JsonPropertyName("default_duration")]
    public double? DefaultDuration { get; set; }

    /// <summary>
    /// The default speed for audio models.
    /// </summary>
    [JsonPropertyName("default_speed")]
    public double? DefaultSpeed { get; set; }

    /// <summary>
    /// The minimum prompt length required by the model.
    /// </summary>
    [JsonPropertyName("min_prompt_length")]
    public int? MinPromptLength { get; set; }

    /// <summary>
    /// The prompt character limit for the model.
    /// </summary>
    [JsonPropertyName("prompt_character_limit")]
    public int? PromptCharacterLimit { get; set; }

    /// <summary>
    /// The lyrics character limit for lyric-capable music models.
    /// </summary>
    [JsonPropertyName("lyrics_character_limit")]
    public int? LyricsCharacterLimit { get; set; }

    /// <summary>
    /// Whether lyrics are required for this music model.
    /// </summary>
    [JsonPropertyName("lyrics_required")]
    public bool? LyricsRequired { get; set; }

    /// <summary>
    /// Whether the model supports lyrics.
    /// </summary>
    [JsonPropertyName("supports_lyrics")]
    public bool? SupportsLyrics { get; set; }

    /// <summary>
    /// Whether the model supports force instrumental mode.
    /// </summary>
    [JsonPropertyName("supports_force_instrumental")]
    public bool? SupportsForceInstrumental { get; set; }

    /// <summary>
    /// Whether the model supports a language code.
    /// </summary>
    [JsonPropertyName("supports_language_code")]
    public bool? SupportsLanguageCode { get; set; }

    /// <summary>
    /// Whether the model supports looping.
    /// </summary>
    [JsonPropertyName("supports_loop")]
    public bool? SupportsLoop { get; set; }

    /// <summary>
    /// Whether the model supports speed adjustment.
    /// </summary>
    [JsonPropertyName("supports_speed")]
    public bool? SupportsSpeed { get; set; }

    /// <summary>
    /// The minimum duration supported by the model.
    /// </summary>
    [JsonPropertyName("min_duration")]
    public double? MinDuration { get; set; }

    /// <summary>
    /// The maximum duration supported by the model.
    /// </summary>
    [JsonPropertyName("max_duration")]
    public double? MaxDuration { get; set; }

    /// <summary>
    /// The minimum speed supported by the model.
    /// </summary>
    [JsonPropertyName("min_speed")]
    public double? MinSpeed { get; set; }

    /// <summary>
    /// The maximum speed supported by the model.
    /// </summary>
    [JsonPropertyName("max_speed")]
    public double? MaxSpeed { get; set; }

    /// <summary>
    /// The available duration options for the model.
    /// </summary>
    [JsonPropertyName("duration_options")]
    public List<double>? DurationOptions { get; set; }

    /// <summary>
    /// The supported formats for the model.
    /// </summary>
    [JsonPropertyName("supported_formats")]
    public List<string>? SupportedFormats { get; set; }

    /// <summary>
    /// Whether the model supports custom voice ID.
    /// </summary>
    [JsonPropertyName("supports_custom_voice_id")]
    public bool? SupportsCustomVoiceId { get; set; }

    /// <summary>
    /// Whether the model supports voice cloning.
    /// </summary>
    [JsonPropertyName("voice_cloning")]
    public VoiceCloning? VoiceCloning { get; set; }

    /// <summary>
    /// The model sets this model belongs to (e.g., "venice_recommendations").
    /// </summary>
    [JsonPropertyName("model_sets")]
    public List<string>? ModelSets { get; set; }

    /// <summary>
    /// Whether the model supports web search.
    /// </summary>
    [JsonPropertyName("supportsWebSearch")]
    public bool? SupportsWebSearch { get; set; }

    /// <summary>
    /// Whether the model supports prompt optimization thinking.
    /// </summary>
    [JsonPropertyName("supportsOptimizePromptThinking")]
    public bool? SupportsOptimizePromptThinking { get; set; }

    /// <summary>
    /// Whether the model supports a lyrics optimizer.
    /// </summary>
    [JsonPropertyName("supports_lyrics_optimizer")]
    public bool? SupportsLyricsOptimizer { get; set; }

    /// <summary>
    /// Text model specific capabilities.
    /// </summary>
    [JsonPropertyName("capabilities")]
    public ModelCapabilities? Capabilities { get; set; }

    /// <summary>
    /// Constraints that apply to this model.
    /// </summary>
    [JsonPropertyName("constraints")]
    public ModelConstraints? Constraints { get; set; }

    /// <summary>
    /// Pricing details for the model.
    /// </summary>
    [JsonPropertyName("pricing")]
    public ModelPricing? Pricing { get; set; }

    /// <summary>
    /// Traits that apply to this model.
    /// </summary>
    [JsonPropertyName("traits")]
    public List<string> Traits { get; set; } = new();

    /// <summary>
    /// The voices available for this TTS model.
    /// </summary>
    [JsonPropertyName("voices")]
    public List<string>? Voices { get; set; }
}

/// <summary>
/// Model capabilities.
/// </summary>
public class ModelCapabilities
{
    /// <summary>
    /// Is the LLM optimized for coding?
    /// </summary>
    [JsonPropertyName("optimizedForCode")]
    public bool OptimizedForCode { get; set; }

    /// <summary>
    /// The quantization type of the running model.
    /// </summary>
    [JsonPropertyName("quantization")]
    public string Quantization { get; set; } = string.Empty;

    /// <summary>
    /// Does the LLM model support function calling?
    /// </summary>
    [JsonPropertyName("supportsFunctionCalling")]
    public bool SupportsFunctionCalling { get; set; }

    /// <summary>
    /// Does the model support reasoning with thinking blocks?
    /// </summary>
    [JsonPropertyName("supportsReasoning")]
    public bool SupportsReasoning { get; set; }

    /// <summary>
    /// Does the LLM model support response schema?
    /// </summary>
    [JsonPropertyName("supportsResponseSchema")]
    public bool SupportsResponseSchema { get; set; }

    /// <summary>
    /// Does the LLM support vision?
    /// </summary>
    [JsonPropertyName("supportsVision")]
    public bool SupportsVision { get; set; }

    /// <summary>
    /// Does the LLM model support web search?
    /// </summary>
    [JsonPropertyName("supportsWebSearch")]
    public bool SupportsWebSearch { get; set; }

    /// <summary>
    /// Does the LLM model support logprobs parameter?
    /// </summary>
    [JsonPropertyName("supportsLogProbs")]
    public bool SupportsLogProbs { get; set; }

    /// <summary>
    /// Does the LLM model support audio input?
    /// </summary>
    [JsonPropertyName("supportsAudioInput")]
    public bool SupportsAudioInput { get; set; }

    /// <summary>
    /// Does the model support multiple images per request?
    /// </summary>
    [JsonPropertyName("supportsMultipleImages")]
    public bool SupportsMultipleImages { get; set; }

    /// <summary>
    /// Does the model support video input?
    /// </summary>
    [JsonPropertyName("supportsVideoInput")]
    public bool SupportsVideoInput { get; set; }

    /// <summary>
    /// Does the model support end-to-end encryption?
    /// </summary>
    [JsonPropertyName("supportsE2EE")]
    public bool SupportsE2EE { get; set; }

    /// <summary>
    /// Does the model support TEE attestation?
    /// </summary>
    [JsonPropertyName("supportsTeeAttestation")]
    public bool SupportsTeeAttestation { get; set; }

    /// <summary>
    /// Does the model support configurable reasoning effort?
    /// </summary>
    [JsonPropertyName("supportsReasoningEffort")]
    public bool SupportsReasoningEffort { get; set; }

    /// <summary>
    /// The default reasoning effort for models that support it.
    /// </summary>
    [JsonPropertyName("defaultReasoningEffort")]
    public string? DefaultReasoningEffort { get; set; }

    /// <summary>
    /// The available reasoning effort options for the model.
    /// </summary>
    [JsonPropertyName("reasoningEffortOptions")]
    public List<string>? ReasoningEffortOptions { get; set; }

    /// <summary>
    /// Does the model support xAI native search?
    /// </summary>
    [JsonPropertyName("supportsXSearch")]
    public bool SupportsXSearch { get; set; }

    /// <summary>
    /// The maximum number of images supported per request.
    /// </summary>
    [JsonPropertyName("maxImages")]
    public int? MaxImages { get; set; }

    /// <summary>
    /// The maximum number of videos supported per request.
    /// </summary>
    [JsonPropertyName("maxVideos")]
    public int? MaxVideos { get; set; }
}

/// <summary>
/// Model deprecation details.
/// </summary>
public class ModelDeprecation
{
    /// <summary>
    /// Legacy ISO 8601 instant aligned with the deprecation sunset used in response headers.
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>
    /// ISO 8601 instant when this model ID is omitted from public GET /models listings.
    /// </summary>
    [JsonPropertyName("removesAt")]
    public string? RemovesAt { get; set; }

    /// <summary>
    /// Suggested public API model ID to migrate to, when one exists.
    /// </summary>
    [JsonPropertyName("replacementModelId")]
    public string? ReplacementModelId { get; set; }

    /// <summary>
    /// When true, Venice may automatically remap API requests for this model ID to the replacement model.
    /// </summary>
    [JsonPropertyName("autoRemap")]
    public bool AutoRemap { get; set; }
}

/// <summary>
/// Voice cloning configuration for TTS models.
/// </summary>
public class VoiceCloning
{
    /// <summary>
    /// The voice cloning mode (e.g., "zero_shot").
    /// </summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    /// <summary>
    /// The accepted audio formats for voice samples.
    /// </summary>
    [JsonPropertyName("accepted_formats")]
    public List<string>? AcceptedFormats { get; set; }

    /// <summary>
    /// The minimum voice sample length in seconds.
    /// </summary>
    [JsonPropertyName("min_sample_seconds")]
    public int? MinSampleSeconds { get; set; }

    /// <summary>
    /// The retention period for cloned voices in days.
    /// </summary>
    [JsonPropertyName("retention_days")]
    public int? RetentionDays { get; set; }
}

/// <summary>
/// Model constraints.
/// </summary>
public class ModelConstraints
{
    /// <summary>
    /// Temperature constraints.
    /// </summary>
    [JsonPropertyName("temperature")]
    public ParameterConstraint? Temperature { get; set; }

    /// <summary>
    /// Top-p constraints.
    /// </summary>
    [JsonPropertyName("top_p")]
    public ParameterConstraint? TopP { get; set; }

    /// <summary>
    /// The maximum supported prompt length.
    /// </summary>
    [JsonPropertyName("promptCharacterLimit")]
    public int? PromptCharacterLimit { get; set; }

    /// <summary>
    /// Steps constraints for image models.
    /// </summary>
    [JsonPropertyName("steps")]
    public StepsConstraint? Steps { get; set; }

    /// <summary>
    /// The requested width and height divisor for image models.
    /// </summary>
    [JsonPropertyName("widthHeightDivisor")]
    public int? WidthHeightDivisor { get; set; }
}

/// <summary>
/// Parameter constraint.
/// </summary>
public class ParameterConstraint
{
    /// <summary>
    /// Default value.
    /// </summary>
    [JsonPropertyName("default")]
    public double Default { get; set; }

    /// <summary>
    /// Minimum value.
    /// </summary>
    [JsonPropertyName("min")]
    public double? Min { get; set; }

    /// <summary>
    /// Maximum value.
    /// </summary>
    [JsonPropertyName("max")]
    public double? Max { get; set; }
}

/// <summary>
/// Steps constraint for image models.
/// </summary>
public class StepsConstraint
{
    /// <summary>
    /// Default number of steps.
    /// </summary>
    [JsonPropertyName("default")]
    public int Default { get; set; }

    /// <summary>
    /// Maximum number of steps.
    /// </summary>
    [JsonPropertyName("max")]
    public int Max { get; set; }
}

/// <summary>
/// Model pricing information.
/// </summary>
public class ModelPricing
{
    /// <summary>
    /// Input pricing for LLM models.
    /// </summary>
    [JsonPropertyName("input")]
    public PricingDetails? Input { get; set; }

    /// <summary>
    /// Cached input pricing for LLM models.
    /// </summary>
    [JsonPropertyName("cache_input")]
    public PricingDetails? CacheInput { get; set; }

    /// <summary>
    /// Output pricing for LLM models.
    /// </summary>
    [JsonPropertyName("output")]
    public PricingDetails? Output { get; set; }

    /// <summary>
    /// Generation pricing for image models.
    /// </summary>
    [JsonPropertyName("generation")]
    public PricingDetails? Generation { get; set; }

    /// <summary>
    /// Upscale pricing for image models.
    /// </summary>
    [JsonPropertyName("upscale")]
    public PricingDetails? Upscale { get; set; }
}

/// <summary>
/// Pricing details.
/// </summary>
public class PricingDetails
{
    /// <summary>
    /// Price in USD.
    /// </summary>
    [JsonPropertyName("usd")]
    public double? Usd { get; set; }

    /// <summary>
    /// Price in VCU.
    /// </summary>
    [JsonPropertyName("vcu")]
    public double? Vcu { get; set; }

    /// <summary>
    /// Price in DIEM.
    /// </summary>
    [JsonPropertyName("diem")]
    public double? Diem { get; set; }
}

/// <summary>
/// Model traits response.
/// </summary>
public class ModelTraitsResponse : BaseResponse
{
    /// <summary>
    /// Dictionary of traits to model IDs.
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, string> Traits { get; set; } = new();
}

/// <summary>
/// Model compatibility mapping response.
/// </summary>
public class ModelCompatibilityResponse : BaseResponse
{
    /// <summary>
    /// Dictionary of compatibility mappings.
    /// </summary>
    [JsonPropertyName("compatibility")]
    public Dictionary<string, string> Compatibility { get; set; } = new();
}

/// <summary>
/// Model traits API response.
/// </summary>
public class ModelTraitsApiResponse
{
    /// <summary>
    /// Dictionary of traits to model IDs.
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, string> Data { get; set; } = new();

    /// <summary>
    /// Object type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "list";

    /// <summary>
    /// Type of models.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "text";
}

/// <summary>
/// Model compatibility mapping API response.
/// </summary>
public class ModelCompatibilityApiResponse
{
    /// <summary>
    /// Dictionary of compatibility mappings.
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, string> Data { get; set; } = new();

    /// <summary>
    /// Object type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "list";

    /// <summary>
    /// Type of models.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "text";
}
