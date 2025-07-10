using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Images;

/// <summary>
/// Request for generating an image.
/// </summary>
public class GenerateImageRequest
{
    /// <summary>
    /// The model to use for image generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The description for the image.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// A description of what should not be in the image.
    /// </summary>
    [JsonPropertyName("negative_prompt")]
    public string? NegativePrompt { get; set; }

    /// <summary>
    /// Width of the generated image.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }

    /// <summary>
    /// Height of the generated image.
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; }

    /// <summary>
    /// Number of inference steps.
    /// </summary>
    [JsonPropertyName("steps")]
    public int? Steps { get; set; }

    /// <summary>
    /// CFG scale parameter.
    /// </summary>
    [JsonPropertyName("cfg_scale")]
    public double? CfgScale { get; set; }

    /// <summary>
    /// Random seed for generation.
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }

    /// <summary>
    /// The image format to return.
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// Whether to return binary image data instead of base64.
    /// </summary>
    [JsonPropertyName("return_binary")]
    public bool? ReturnBinary { get; set; }

    /// <summary>
    /// Whether to use safe mode.
    /// </summary>
    [JsonPropertyName("safe_mode")]
    public bool? SafeMode { get; set; }

    /// <summary>
    /// Whether to hide the Venice watermark.
    /// </summary>
    [JsonPropertyName("hide_watermark")]
    public bool? HideWatermark { get; set; }

    /// <summary>
    /// Embed prompt generation information into the image's EXIF metadata.
    /// </summary>
    [JsonPropertyName("embed_exif_metadata")]
    public bool? EmbedExifMetadata { get; set; }

    /// <summary>
    /// Lora strength for the model.
    /// </summary>
    [JsonPropertyName("lora_strength")]
    public int? LoraStrength { get; set; }

    /// <summary>
    /// An image style to apply to the image.
    /// </summary>
    [JsonPropertyName("style_preset")]
    public string? StylePreset { get; set; }
}

/// <summary>
/// Simple image generation request (OpenAI compatible).
/// </summary>
public class SimpleGenerateImageRequest
{
    /// <summary>
    /// A text description of the desired image.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The model to use for image generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Number of images to generate.
    /// </summary>
    [JsonPropertyName("n")]
    public int? N { get; set; }

    /// <summary>
    /// Size of generated images.
    /// </summary>
    [JsonPropertyName("size")]
    public string? Size { get; set; }

    /// <summary>
    /// Output format for generated images.
    /// </summary>
    [JsonPropertyName("output_format")]
    public string? OutputFormat { get; set; }

    /// <summary>
    /// Response format.
    /// </summary>
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }

    /// <summary>
    /// Quality setting.
    /// </summary>
    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    /// <summary>
    /// Style setting.
    /// </summary>
    [JsonPropertyName("style")]
    public string? Style { get; set; }

    /// <summary>
    /// Background setting.
    /// </summary>
    [JsonPropertyName("background")]
    public string? Background { get; set; }

    /// <summary>
    /// Moderation setting.
    /// </summary>
    [JsonPropertyName("moderation")]
    public string? Moderation { get; set; }

    /// <summary>
    /// Output compression setting.
    /// </summary>
    [JsonPropertyName("output_compression")]
    public int? OutputCompression { get; set; }

    /// <summary>
    /// User identifier.
    /// </summary>
    [JsonPropertyName("user")]
    public string? User { get; set; }
}

/// <summary>
/// Request for upscaling an image.
/// </summary>
public class UpscaleImageRequest
{
    /// <summary>
    /// The image to upscale.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The scale factor for upscaling the image.
    /// </summary>
    [JsonPropertyName("scale")]
    public double? Scale { get; set; }

    /// <summary>
    /// Whether to enhance the image using Venice's image engine.
    /// </summary>
    [JsonPropertyName("enhance")]
    public object? Enhance { get; set; }

    /// <summary>
    /// Higher values let the enhancement AI change the image more.
    /// </summary>
    [JsonPropertyName("enhanceCreativity")]
    public double? EnhanceCreativity { get; set; }

    /// <summary>
    /// The text to image style to apply during prompt enhancement.
    /// </summary>
    [JsonPropertyName("enhancePrompt")]
    public string? EnhancePrompt { get; set; }

    /// <summary>
    /// How strongly lines and noise in the base image are preserved.
    /// </summary>
    [JsonPropertyName("replication")]
    public double? Replication { get; set; }
}

/// <summary>
/// Request for editing an image.
/// </summary>
public class EditImageRequest
{
    /// <summary>
    /// The text directions to edit or modify the image.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The image to edit.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}

/// <summary>
/// Response from image generation API.
/// </summary>
public class ImageGenerationResponse : BaseResponse
{
    /// <summary>
    /// List of generated images.
    /// </summary>
    [JsonPropertyName("data")]
    public List<ImageData> Data { get; set; } = new();

    /// <summary>
    /// The timestamp when the image was created.
    /// </summary>
    [JsonPropertyName("created")]
    public long Created { get; set; }
}

/// <summary>
/// Image data in the response.
/// </summary>
public class ImageData
{
    /// <summary>
    /// The base64-encoded image data.
    /// </summary>
    [JsonPropertyName("b64_json")]
    public string? B64Json { get; set; }

    /// <summary>
    /// The URL of the image.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// The revised prompt used for generation.
    /// </summary>
    [JsonPropertyName("revised_prompt")]
    public string? RevisedPrompt { get; set; }
}

/// <summary>
/// Image style information.
/// </summary>
public class ImageStyle
{
    /// <summary>
    /// The name of the style.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the style.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Example image URL for the style.
    /// </summary>
    [JsonPropertyName("example_image")]
    public string? ExampleImage { get; set; }
}

/// <summary>
/// Response from image styles API.
/// </summary>
public class ImageStylesResponse : BaseResponse
{
    /// <summary>
    /// List of available image styles.
    /// </summary>
    [JsonPropertyName("styles")]
    public List<ImageStyle> Styles { get; set; } = new();
}
