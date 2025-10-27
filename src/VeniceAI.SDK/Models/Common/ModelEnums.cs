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
    Audio
}

/// <summary>
/// Available text models for chat and text generation.
/// </summary>
public enum TextModel
{
    /// <summary>
    /// Venice uncensored model - unrestricted content generation
    /// </summary>
    [Description("venice-uncensored")]
    VeniceUncensored,

    /// <summary>
    /// Qwen3 4B - Small, efficient model (Venice Small)
    /// </summary>
    [Description("qwen3-4b")]
    VeniceSmall,

    /// <summary>
    /// Mistral 31 24B - Medium-sized model with vision capabilities (Venice Medium)
    /// </summary>
    [Description("mistral-31-24b")]
    VeniceMedium,

    /// <summary>
    /// Qwen3 235B - Large, powerful model (Venice Large)
    /// </summary>
    [Description("qwen3-235b")]
    VeniceLarge,

    /// <summary>
    /// Llama 3.2 3B - Compact Meta model
    /// </summary>
    [Description("llama-3.2-3b")]
    Llama32_3B,

    /// <summary>
    /// Llama 3.3 70B - High-performance Meta model
    /// </summary>
    [Description("llama-3.3-70b")]
    Llama33_70B,

    /// <summary>
    /// DeepSeek R1 671B - Large reasoning model
    /// </summary>
    [Description("deepseek-r1-671b")]
    DeepSeekR1_671B,

    // Obsolete models - kept for backward compatibility
    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSmall (qwen3-4b) instead.")]
    [Description("qwen-2.5-qwq-32b")]
    QwenReasonning,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceMedium (mistral-31-24b) instead.")]
    [Description("mistral-32-24b")]
    VeniceMedium32,

    [Obsolete("This model is no longer available in the Venice AI API. Use Llama33_70B instead.")]
    [Description("llama-3.1-405b")]
    Llama31_405B,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceLarge (qwen3-235b) instead.")]
    [Description("dolphin-2.9.2-qwen2-72b")]
    Dolphin72B,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceMedium (mistral-31-24b) for vision capabilities.")]
    [Description("qwen-2.5-vl")]
    Qwen25VL,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSmall (qwen3-4b) or VeniceLarge (qwen3-235b) instead.")]
    [Description("qwen-2.5-coder-32b")]
    Qwen25Coder32B,

    [Obsolete("This model is no longer available in the Venice AI API. Use DeepSeekR1_671B instead.")]
    [Description("deepseek-coder-v2-lite")]
    DeepSeekCoderV2Lite
}

/// <summary>
/// Available image generation models.
/// </summary>
public enum ImageModel
{
    /// <summary>
    /// Venice SD 3.5 - Venice's optimized Stable Diffusion 3.5 model
    /// </summary>
    [Description("venice-sd35")]
    VeniceSD35,

    /// <summary>
    /// HiDream - High-quality image generation model
    /// </summary>
    [Description("hidream")]
    HiDream,

    /// <summary>
    /// Lustify SDXL - Uncensored SDXL model for adult content
    /// </summary>
    [Description("lustify-sdxl")]
    LustifySDXL,

    /// <summary>
    /// Qwen Image - Fast image generation model
    /// </summary>
    [Description("qwen-image")]
    QwenImage,

    /// <summary>
    /// WAI Illustrious - Anime-style image generation model
    /// </summary>
    [Description("wai-Illustrious")]
    WaiIllustrious,

    // Obsolete models - kept for backward compatibility
    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 or HiDream instead.")]
    [Description("flux-dev")]
    FluxStandard,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 or HiDream instead.")]
    [Description("flux-dev-uncensored")]
    FluxCustom,

    [Obsolete("This model is no longer available in the Venice AI API. Use LustifySDXL or WaiIllustrious instead.")]
    [Description("pony-realism")]
    PonyRealism,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 instead.")]
    [Description("stable-diffusion-3.5")]
    StableDiffusion35
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
