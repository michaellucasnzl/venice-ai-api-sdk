using System.ComponentModel;

namespace VeniceAI.SDK.Models.Common;

/// <summary>
/// Model type filter options for Venice AI API endpoints.
/// </summary>
public enum ModelType
{
    [Description("all")]
    All,

    [Description("text")]
    Text,

    [Description("code")]
    Code,

    [Description("image")]
    Image,

    [Description("tts")]
    Tts,

    [Description("audio")]
    Audio,

    [Description("asr")]
    Asr,

    [Description("video")]
    Video,

    [Description("embedding")]
    Embedding,

    [Description("upscale")]
    Upscale,

    [Description("inpaint")]
    Inpaint
}

/// <summary>
/// Available text models for chat and text generation.
/// </summary>
public enum TextModel
{
    /// <summary>
    /// Venice uncensored model - unrestricted content generation (Dolphin-Mistral-24B-Venice-Edition)
    /// </summary>
    [Description("venice-uncensored")]
    VeniceUncensored,

    /// <summary>
    /// Qwen3 4B - Small, efficient model (Venice Small) with reasoning support
    /// </summary>
    [Description("qwen3-4b")]
    VeniceSmall,

    /// <summary>
    /// Mistral 31 24B - Medium-sized model with vision capabilities (Venice Medium)
    /// </summary>
    [Description("mistral-31-24b")]
    VeniceMedium,

    /// <summary>
    /// Qwen3 235B - Large, powerful model (Venice Large 1.1) with reasoning support
    /// </summary>
    [Description("qwen3-235b")]
    VeniceLarge,

    /// <summary>
    /// Qwen3 235B A22B Thinking 2507 - Large reasoning model with extended thinking capabilities
    /// </summary>
    [Description("qwen3-235b-a22b-thinking-2507")]
    Qwen3_235B_Thinking,

    /// <summary>
    /// Qwen3 235B A22B Instruct 2507 - Large instruction-following model
    /// </summary>
    [Description("qwen3-235b-a22b-instruct-2507")]
    Qwen3_235B_Instruct,

    /// <summary>
    /// Qwen3 Next 80B - Medium-large model with 262K context
    /// </summary>
    [Description("qwen3-next-80b")]
    Qwen3Next80B,

    /// <summary>
    /// Qwen3 Coder 480B - Large coding-optimized model (default_code trait)
    /// </summary>
    [Description("qwen3-coder-480b-a35b-instruct")]
    Qwen3Coder480B,

    /// <summary>
    /// Llama 3.2 3B - Compact Meta model (fastest trait)
    /// </summary>
    [Description("llama-3.2-3b")]
    Llama32_3B,

    /// <summary>
    /// Llama 3.3 70B - High-performance Meta model (default trait)
    /// </summary>
    [Description("llama-3.3-70b")]
    Llama33_70B,

    /// <summary>
    /// Hermes 3 Llama 3.1 405B - Large NousResearch model
    /// </summary>
    [Description("hermes-3-llama-3.1-405b")]
    Hermes3Llama405B,

    /// <summary>
    /// Google Gemma 3 27B Instruct - Google's vision-capable model
    /// </summary>
    [Description("google-gemma-3-27b-it")]
    GoogleGemma3_27B,

    /// <summary>
    /// Grok 4.1 Fast - xAI's fast reasoning model with vision support
    /// </summary>
    [Description("grok-41-fast")]
    Grok41Fast,

    /// <summary>
    /// Gemini 3 Pro Preview - Google DeepMind's premium model with reasoning
    /// </summary>
    [Description("gemini-3-pro-preview")]
    Gemini3ProPreview,

    /// <summary>
    /// Claude Opus 4.5 - Anthropic's premium coding and reasoning model
    /// </summary>
    [Description("claude-opus-45")]
    ClaudeOpus45,

    /// <summary>
    /// OpenAI GPT OSS 120B - OpenAI's open-source model
    /// </summary>
    [Description("openai-gpt-oss-120b")]
    OpenAIGptOss120B,

    /// <summary>
    /// Kimi K2 Thinking - Moonshot AI's reasoning model optimized for code
    /// </summary>
    [Description("kimi-k2-thinking")]
    KimiK2Thinking,

    /// <summary>
    /// GLM 4.6 - ZhipuAI's large language model
    /// </summary>
    [Description("zai-org-glm-4.6")]
    Glm46,

    /// <summary>
    /// DeepSeek V3.2 - DeepSeek's latest model
    /// </summary>
    [Description("deepseek-v3.2")]
    DeepSeekV32,

    // Obsolete models - kept for backward compatibility
    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSmall (qwen3-4b) instead.")]
    [Description("qwen-2.5-qwq-32b")]
    QwenReasonning,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceMedium (mistral-31-24b) instead.")]
    [Description("mistral-32-24b")]
    VeniceMedium32,

    [Obsolete("This model is no longer available in the Venice AI API. Use Hermes3Llama405B instead.")]
    [Description("llama-3.1-405b")]
    Llama31_405B,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceLarge (qwen3-235b) instead.")]
    [Description("dolphin-2.9.2-qwen2-72b")]
    Dolphin72B,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceMedium (mistral-31-24b) for vision capabilities.")]
    [Description("qwen-2.5-vl")]
    Qwen25VL,

    [Obsolete("This model is no longer available in the Venice AI API. Use Qwen3Coder480B instead.")]
    [Description("qwen-2.5-coder-32b")]
    Qwen25Coder32B,

    [Obsolete("This model is no longer available in the Venice AI API. Use DeepSeekV32 instead.")]
    [Description("deepseek-coder-v2-lite")]
    DeepSeekCoderV2Lite,

    [Obsolete("This model is no longer available in the Venice AI API. Use DeepSeekV32 instead.")]
    [Description("deepseek-r1-671b")]
    DeepSeekR1_671B
}

/// <summary>
/// Available image generation models.
/// </summary>
public enum ImageModel
{
    /// <summary>
    /// Venice SD 3.5 - Venice's optimized Stable Diffusion 3.5 model (default trait)
    /// </summary>
    [Description("venice-sd35")]
    VeniceSD35,

    /// <summary>
    /// HiDream - High-quality image generation model
    /// </summary>
    [Description("hidream")]
    HiDream,

    /// <summary>
    /// Nano Banana Pro - Premium image generation with web search support and 32K prompt limit
    /// </summary>
    [Description("nano-banana-pro")]
    NanoBananaPro,

    /// <summary>
    /// Lustify SDXL - Uncensored SDXL model for adult content
    /// </summary>
    [Description("lustify-sdxl")]
    LustifySDXL,

    /// <summary>
    /// Lustify v7 - Updated Lustify model for adult content
    /// </summary>
    [Description("lustify-v7")]
    LustifyV7,

    /// <summary>
    /// Qwen Image - Fast image generation model (highest_quality trait)
    /// </summary>
    [Description("qwen-image")]
    QwenImage,

    /// <summary>
    /// WAI Illustrious - Anime-style image generation model
    /// </summary>
    [Description("wai-Illustrious")]
    WaiIllustrious,

    /// <summary>
    /// Z-Image Turbo - Fast turbo image generation model with 7500 char prompt limit
    /// </summary>
    [Description("z-image-turbo")]
    ZImageTurbo,

    // Obsolete models - kept for backward compatibility
    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 or HiDream instead.")]
    [Description("flux-dev")]
    FluxStandard,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 or HiDream instead.")]
    [Description("flux-dev-uncensored")]
    FluxCustom,

    [Obsolete("This model is no longer available in the Venice AI API. Use LustifySDXL, LustifyV7, or WaiIllustrious instead.")]
    [Description("pony-realism")]
    PonyRealism,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 instead.")]
    [Description("stable-diffusion-3.5")]
    StableDiffusion35
}

/// <summary>
/// Available video generation models.
/// </summary>
public enum VideoModel
{
    /// <summary>
    /// Wan 2.5 Preview - Image to Video generation
    /// </summary>
    [Description("wan-2.5-preview-image-to-video")]
    Wan25PreviewImageToVideo,

    /// <summary>
    /// Wan 2.5 Preview - Text to Video generation
    /// </summary>
    [Description("wan-2.5-preview-text-to-video")]
    Wan25PreviewTextToVideo,

    /// <summary>
    /// Wan 2.2 A14B - Text to Video generation
    /// </summary>
    [Description("wan-2.2-a14b-text-to-video")]
    Wan22A14BTextToVideo,

    /// <summary>
    /// Wan 2.1 Pro - Image to Video generation
    /// </summary>
    [Description("wan-2.1-pro-image-to-video")]
    Wan21ProImageToVideo,

    /// <summary>
    /// LTX Video 2.0 Fast - Image to Video generation
    /// </summary>
    [Description("ltx-2-fast-image-to-video")]
    Ltx2FastImageToVideo,

    /// <summary>
    /// LTX Video 2.0 Fast - Text to Video generation
    /// </summary>
    [Description("ltx-2-fast-text-to-video")]
    Ltx2FastTextToVideo,

    /// <summary>
    /// LTX Video 2.0 Full Quality - Image to Video generation
    /// </summary>
    [Description("ltx-2-full-image-to-video")]
    Ltx2FullImageToVideo,

    /// <summary>
    /// LTX Video 2.0 Full Quality - Text to Video generation
    /// </summary>
    [Description("ltx-2-full-text-to-video")]
    Ltx2FullTextToVideo,

    /// <summary>
    /// Ovi - Image to Video generation
    /// </summary>
    [Description("ovi-image-to-video")]
    OviImageToVideo,

    /// <summary>
    /// Kling 2.6 Pro - Text to Video generation
    /// </summary>
    [Description("kling-2.6-pro-text-to-video")]
    Kling26ProTextToVideo,

    /// <summary>
    /// Kling 2.6 Pro - Image to Video generation
    /// </summary>
    [Description("kling-2.6-pro-image-to-video")]
    Kling26ProImageToVideo,

    /// <summary>
    /// Kling 2.5 Turbo Pro - Text to Video generation
    /// </summary>
    [Description("kling-2.5-turbo-pro-text-to-video")]
    Kling25TurboProTextToVideo,

    /// <summary>
    /// Kling 2.5 Turbo Pro - Image to Video generation
    /// </summary>
    [Description("kling-2.5-turbo-pro-image-to-video")]
    Kling25TurboProImageToVideo,

    /// <summary>
    /// Longcat Distilled - Image to Video generation (up to 30s)
    /// </summary>
    [Description("longcat-distilled-image-to-video")]
    LongcatDistilledImageToVideo,

    /// <summary>
    /// Longcat Distilled - Text to Video generation (up to 30s)
    /// </summary>
    [Description("longcat-distilled-text-to-video")]
    LongcatDistilledTextToVideo,

    /// <summary>
    /// Longcat Full Quality - Image to Video generation (up to 30s)
    /// </summary>
    [Description("longcat-image-to-video")]
    LongcatImageToVideo,

    /// <summary>
    /// Longcat Full Quality - Text to Video generation (up to 30s)
    /// </summary>
    [Description("longcat-text-to-video")]
    LongcatTextToVideo,

    /// <summary>
    /// Veo 3 Fast - Text to Video generation with audio
    /// </summary>
    [Description("veo3-fast-text-to-video")]
    Veo3FastTextToVideo,

    /// <summary>
    /// Veo 3 Fast - Image to Video generation with audio
    /// </summary>
    [Description("veo3-fast-image-to-video")]
    Veo3FastImageToVideo,

    /// <summary>
    /// Veo 3 Full Quality - Text to Video generation with audio
    /// </summary>
    [Description("veo3-full-text-to-video")]
    Veo3FullTextToVideo,

    /// <summary>
    /// Veo 3 Full Quality - Image to Video generation with audio
    /// </summary>
    [Description("veo3-full-image-to-video")]
    Veo3FullImageToVideo,

    /// <summary>
    /// Veo 3.1 Fast - Text to Video generation with audio
    /// </summary>
    [Description("veo3.1-fast-text-to-video")]
    Veo31FastTextToVideo,

    /// <summary>
    /// Veo 3.1 Fast - Image to Video generation with audio
    /// </summary>
    [Description("veo3.1-fast-image-to-video")]
    Veo31FastImageToVideo,

    /// <summary>
    /// Veo 3.1 Full Quality - Text to Video generation with audio
    /// </summary>
    [Description("veo3.1-full-text-to-video")]
    Veo31FullTextToVideo,

    /// <summary>
    /// Veo 3.1 Full Quality - Image to Video generation with audio
    /// </summary>
    [Description("veo3.1-full-image-to-video")]
    Veo31FullImageToVideo,

    /// <summary>
    /// Sora 2 - Image to Video generation with audio
    /// </summary>
    [Description("sora-2-image-to-video")]
    Sora2ImageToVideo,

    /// <summary>
    /// Sora 2 Pro - Image to Video generation with audio (up to 1080p)
    /// </summary>
    [Description("sora-2-pro-image-to-video")]
    Sora2ProImageToVideo,

    /// <summary>
    /// Sora 2 - Text to Video generation with audio
    /// </summary>
    [Description("sora-2-text-to-video")]
    Sora2TextToVideo,

    /// <summary>
    /// Sora 2 Pro - Text to Video generation with audio (up to 1080p)
    /// </summary>
    [Description("sora-2-pro-text-to-video")]
    Sora2ProTextToVideo
}

/// <summary>
/// Available embedding models.
/// </summary>
public enum EmbeddingModel
{
    [Description("text-embedding-bge-m3")]
    TextEmbeddingBGEM3
}

/// <summary>
/// Available text-to-speech models.
/// </summary>
public enum TextToSpeechModel
{
    [Description("tts-kokoro")]
    TtsKokoro
}

/// <summary>
/// Available upscale models.
/// </summary>
public enum UpscaleModel
{
    [Description("upscaler")]
    Upscaler
}

/// <summary>
/// Available inpaint models.
/// </summary>
public enum InpaintModel
{
    [Description("edit-image")]
    EditImage
}

/// <summary>
/// Available image styles for image generation.
/// </summary>
public enum ImageStyle
{
    [Description("3D Model")]
    ThreeDModel,

    [Description("Analog Film")]
    AnalogFilm,

    [Description("Anime")]
    Anime,

    [Description("Cinematic")]
    Cinematic,

    [Description("Comic Book")]
    ComicBook,

    [Description("Craft Clay")]
    CraftClay,

    [Description("Digital Art")]
    DigitalArt,

    [Description("Enhance")]
    Enhance,

    [Description("Fantasy Art")]
    FantasyArt,

    [Description("Isometric Style")]
    IsometricStyle,

    [Description("Line Art")]
    LineArt,

    [Description("Lowpoly")]
    Lowpoly,

    [Description("Neon Punk")]
    NeonPunk,

    [Description("Origami")]
    Origami,

    [Description("Photographic")]
    Photographic,

    [Description("Pixel Art")]
    PixelArt,

    [Description("Texture")]
    Texture,

    [Description("Advertising")]
    Advertising,

    [Description("Food Photography")]
    FoodPhotography,

    [Description("Real Estate")]
    RealEstate,

    [Description("Abstract")]
    Abstract,

    [Description("Cubist")]
    Cubist,

    [Description("Graffiti")]
    Graffiti,

    [Description("Hyperrealism")]
    Hyperrealism,

    [Description("Impressionist")]
    Impressionist,

    [Description("Pointillism")]
    Pointillism,

    [Description("Pop Art")]
    PopArt,

    [Description("Psychedelic")]
    Psychedelic,

    [Description("Renaissance")]
    Renaissance,

    [Description("Steampunk")]
    Steampunk,

    [Description("Surrealist")]
    Surrealist,

    [Description("Typography")]
    Typography,

    [Description("Watercolor")]
    Watercolor,

    [Description("Fighting Game")]
    FightingGame,

    [Description("GTA")]
    GTA,

    [Description("Super Mario")]
    SuperMario,

    [Description("Minecraft")]
    Minecraft,

    [Description("Pokemon")]
    Pokemon,

    [Description("Retro Arcade")]
    RetroArcade,

    [Description("Retro Game")]
    RetroGame,

    [Description("RPG Fantasy Game")]
    RPGFantasyGame,

    [Description("Strategy Game")]
    StrategyGame,

    [Description("Street Fighter")]
    StreetFighter,

    [Description("Legend of Zelda")]
    LegendOfZelda,

    [Description("Architectural")]
    Architectural,

    [Description("Disco")]
    Disco,

    [Description("Dreamscape")]
    Dreamscape,

    [Description("Dystopian")]
    Dystopian,

    [Description("Fairy Tale")]
    FairyTale,

    [Description("Gothic")]
    Gothic,

    [Description("Grunge")]
    Grunge,

    [Description("Horror")]
    Horror,

    [Description("Minimalist")]
    Minimalist,

    [Description("Monochrome")]
    Monochrome,

    [Description("Nautical")]
    Nautical,

    [Description("Space")]
    Space,

    [Description("Stained Glass")]
    StainedGlass,

    [Description("Techwear Fashion")]
    TechwearFashion,

    [Description("Tribal")]
    Tribal,

    [Description("Zentangle")]
    Zentangle,

    [Description("Collage")]
    Collage,

    [Description("Flat Papercut")]
    FlatPapercut,

    [Description("Kirigami")]
    Kirigami,

    [Description("Paper Mache")]
    PaperMache,

    [Description("Paper Quilling")]
    PaperQuilling,

    [Description("Papercut Collage")]
    PapercutCollage,

    [Description("Papercut Shadow Box")]
    PapercutShadowBox,

    [Description("Stacked Papercut")]
    StackedPapercut,

    [Description("Thick Layered Papercut")]
    ThickLayeredPapercut,

    [Description("Alien")]
    Alien,

    [Description("Film Noir")]
    FilmNoir,

    [Description("HDR")]
    HDR,

    [Description("Long Exposure")]
    LongExposure,

    [Description("Neon Noir")]
    NeonNoir,

    [Description("Silhouette")]
    Silhouette,

    [Description("Tilt-Shift")]
    TiltShift
}
