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
    [Description("venice-uncensored")]
    VeniceUncensored,

    [Description("qwen-2.5-qwq-32b")]
    QwenReasonning,

    [Description("qwen3-4b")]
    VeniceSmall,

    [Description("mistral-31-24b")]
    VeniceMedium,

    [Description("mistral-32-24b")]
    VeniceMedium32,

    [Description("qwen3-235b")]
    VeniceLarge,

    [Description("llama-3.2-3b")]
    Llama32_3B,

    [Description("llama-3.3-70b")]
    Llama33_70B,

    [Description("llama-3.1-405b")]
    Llama31_405B,

    [Description("dolphin-2.9.2-qwen2-72b")]
    Dolphin72B,

    [Description("qwen-2.5-vl")]
    Qwen25VL,

    [Description("qwen-2.5-coder-32b")]
    Qwen25Coder32B,

    [Description("deepseek-r1-671b")]
    DeepSeekR1_671B,

    [Description("deepseek-coder-v2-lite")]
    DeepSeekCoderV2Lite
}

/// <summary>
/// Available image generation models.
/// </summary>
public enum ImageModel
{
    [Description("venice-sd35")]
    VeniceSD35,

    [Description("hidream")]
    HiDream,

    [Description("flux-dev")]
    FluxStandard,

    [Description("flux-dev-uncensored")]
    FluxCustom,

    [Description("lustify-sdxl")]
    LustifySDXL,

    [Description("pony-realism")]
    PonyRealism,

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
