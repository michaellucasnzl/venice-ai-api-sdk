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
    [JsonConverter(typeof(ImageModelJsonConverter))]
    public ImageModel Model { get; set; }

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
    /// Number of images to generate (1-4). Only supported when return_binary is false.
    /// </summary>
    [JsonPropertyName("variants")]
    public int? Variants { get; set; }

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

    /// <summary>
    /// Aspect ratio (utilized by certain image models including Nano Banana). Examples: "1:1", "16:9".
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public string? AspectRatio { get; set; }

    /// <summary>
    /// Resolution (utilized by certain image models including Nano Banana). Examples: "1K", "2K", "4K".
    /// </summary>
    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    /// <summary>
    /// Enable web search for the image generation task. Only supported by certain models.
    /// Additional credits are charged if web search is used.
    /// </summary>
    [JsonPropertyName("enable_web_search")]
    public bool? EnableWebSearch { get; set; }

    /// <summary>
    /// Output quality for supported models (e.g. GPT Image 2). Values: low, medium, high.
    /// Higher values can increase the final request charge.
    /// </summary>
    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    /// <summary>
    /// Skip the model's prompt-optimization thinking step for faster generation.
    /// Only supported by models with supportsOptimizePromptThinking; ignored by others.
    /// </summary>
    [JsonPropertyName("disable_prompt_optimization_thinking")]
    public bool? DisablePromptOptimizationThinking { get; set; }

    /// <summary>
    /// Rewrite the prompt before generation to add clarifying visual detail.
    /// Additional credits are charged when a rewrite is generated.
    /// When applied, the final prompt is returned URL-encoded in the x-venice-enhanced-prompt response header.
    /// </summary>
    [JsonPropertyName("enhance_prompt")]
    public bool? EnhancePrompt { get; set; }

    /// <summary>
    /// Style reference images that guide the output. Each reference includes an image
    /// (base64 string, data URI, or http/https URL) and an optional strength between 0.1 and 1.
    /// </summary>
    [JsonPropertyName("style_references")]
    public List<StyleReference>? StyleReferences { get; set; }
}

/// <summary>
/// A style reference image used to guide image generation output.
/// </summary>
public class StyleReference
{
    /// <summary>
    /// The style reference image as a base64-encoded string (raw or data URI) or a URL.
    /// Must be less than 8MB.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// How strongly the reference guides the output (0.1-1). Defaults to 0.5.
    /// </summary>
    [JsonPropertyName("strength")]
    public double? Strength { get; set; }
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
    [JsonConverter(typeof(ImageModelJsonConverter))]
    public ImageModel? Model { get; set; }

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
/// Request for editing multiple images together (multi-edit).
/// </summary>
public class MultiEditImageRequest
{
    /// <summary>
    /// The text directions to edit or modify the images. Short, descriptive prompts work best.
    /// Character limit is model specific.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// Images used for multi-editing (minimum 1). The first image is treated as the base image,
    /// and the remaining images are used as edit layers/masks. Each image can be a base64-encoded
    /// string or a URL starting with http:// or https://.
    /// </summary>
    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = new();

    /// <summary>
    /// The model ID to use for multi-edit.
    /// </summary>
    [JsonPropertyName("modelId")]
    public string? ModelId { get; set; }

    /// <summary>
    /// The aspect ratio for the output image. Use 'auto' or omit to infer from the input image.
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public string? AspectRatio { get; set; }

    /// <summary>
    /// The resolution of the output image (e.g. "1K", "2K", "4K").
    /// </summary>
    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    /// <summary>
    /// The output format of the generated image (e.g. "png", "jpeg", "webp").
    /// </summary>
    [JsonPropertyName("output_format")]
    public string? OutputFormat { get; set; }

    /// <summary>
    /// Whether to apply safe mode filtering to the output image.
    /// </summary>
    [JsonPropertyName("safe_mode")]
    public bool? SafeMode { get; set; }

    /// <summary>
    /// Output quality for supported models (low, medium, high).
    /// </summary>
    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    /// <summary>
    /// Skip the model's prompt-optimization thinking step for faster generation.
    /// </summary>
    [JsonPropertyName("disable_prompt_optimization_thinking")]
    public bool? DisablePromptOptimizationThinking { get; set; }

    /// <summary>
    /// Rewrite the edit prompt using the input images before editing to add clarifying detail.
    /// </summary>
    [JsonPropertyName("enhance_prompt")]
    public bool? EnhancePrompt { get; set; }
}

/// <summary>
/// Request to remove the background from an image.
/// Provide either an image file/base64 or an image URL.
/// </summary>
public class BackgroundRemoveImageRequest
{
    /// <summary>
    /// The image to remove the background from. Can be a base64-encoded string (file upload).
    /// File size must be less than 25MB.
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    /// <summary>
    /// URL of the image to remove the background from.
    /// </summary>
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
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
    /// The image to edit. Must be a URL or a data URL.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The model to use for image editing.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(ImageModelJsonConverter))]
    public ImageModel? Model { get; set; }

    /// <summary>
    /// The aspect ratio of the output image (e.g. "16:9", "1:1", "9:16").
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public string? AspectRatio { get; set; }

    /// <summary>
    /// The resolution of the output image (e.g. "1024x1024").
    /// </summary>
    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    /// <summary>
    /// The output format of the generated image (e.g. "png", "jpeg", "webp").
    /// </summary>
    [JsonPropertyName("output_format")]
    public string? OutputFormat { get; set; }

    /// <summary>
    /// Whether to apply safe mode filtering to the output image.
    /// </summary>
    [JsonPropertyName("safe_mode")]
    public bool? SafeMode { get; set; }
}

/// <summary>
/// Response from image generation API.
/// </summary>
public class ImageGenerationResponse : BaseResponse
{
    private List<string> _images = new();

    /// <summary>
    /// List of generated images (base64 encoded).
    /// </summary>
    [JsonPropertyName("images")]
    public List<string> Images
    {
        get => _images;
        set
        {
            _images = value;
            // Populate Data property for backward compatibility
            Data = _images.Select(img => new ImageData { B64Json = img }).ToList();
        }
    }

    /// <summary>
    /// List of generated images (for compatibility).
    /// </summary>
    [JsonPropertyName("data")]
    public List<ImageData> Data { get; set; } = new();

    /// <summary>
    /// The timestamp when the image was created.
    /// </summary>
    [JsonPropertyName("created")]
    public long? Created { get; set; }

    /// <summary>
    /// Request information.
    /// </summary>
    [JsonPropertyName("request")]
    public RequestInfo? Request { get; set; }

    /// <summary>
    /// Timing information.
    /// </summary>
    [JsonPropertyName("timing")]
    public TimingInfo? Timing { get; set; }
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

/// <summary>
/// Request information from the API response.
/// </summary>
public class RequestInfo
{
    /// <summary>
    /// Indicates if the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Request data details.
    /// </summary>
    [JsonPropertyName("data")]
    public object? Data { get; set; }
}

/// <summary>
/// Timing information from the API response.
/// </summary>
public class TimingInfo
{
    /// <summary>
    /// Duration of inference in milliseconds.
    /// </summary>
    [JsonPropertyName("inferenceDuration")]
    public long? InferenceDuration { get; set; }

    /// <summary>
    /// Preprocessing time in milliseconds.
    /// </summary>
    [JsonPropertyName("inferencePreprocessingTime")]
    public long? InferencePreprocessingTime { get; set; }

    /// <summary>
    /// Queue time in milliseconds.
    /// </summary>
    [JsonPropertyName("inferenceQueueTime")]
    public long? InferenceQueueTime { get; set; }

    /// <summary>
    /// Total processing time in milliseconds.
    /// </summary>
    [JsonPropertyName("total")]
    public long? Total { get; set; }
}
