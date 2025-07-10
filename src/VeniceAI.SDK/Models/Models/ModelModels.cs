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
