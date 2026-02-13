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
    /// Claude Opus 4.6 - Anthropic's most capable reasoning model with 1M token context window
    /// Model ID: claude-opus-4-6
    /// </summary>
    [Description("claude-opus-4-6")]
    ClaudeOpus46,

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
    /// Kimi K2.5 - Moonshot AI's most advanced open reasoning model with trillion-parameter MoE architecture
    /// Model ID: kimi-k2-5
    /// </summary>
    [Description("kimi-k2-5")]
    KimiK25,

    /// <summary>
    /// GLM 4.7 - Zhiyuan AI's large language model with strong reasoning capabilities and largest context window
    /// Model ID: zai-org-glm-4.7
    /// </summary>
    [Description("zai-org-glm-4.7")]
    Glm47,

    /// <summary>
    /// GLM 4.7 Flash - Fast inference variant of GLM 4.7, optimized for speed while maintaining strong reasoning
    /// Model ID: zai-org-glm-4.7-flash
    /// </summary>
    [Description("zai-org-glm-4.7-flash")]
    Glm47Flash,

    /// <summary>
    /// GLM 5 - Next-generation model from Zhiyuan AI with enhanced reasoning and instruction following
    /// Model ID: zai-org-glm-5
    /// </summary>
    [Description("zai-org-glm-5")]
    Glm5,

    /// <summary>
    /// Gemini 3 Flash Preview - Google's high-speed thinking model with near Pro-level reasoning
    /// Model ID: gemini-3-flash-preview
    /// </summary>
    [Description("gemini-3-flash-preview")]
    Gemini3FlashPreview,

    /// <summary>
    /// Claude Sonnet 4.5 - Anthropic's balanced model with strong coding and reasoning capabilities
    /// Model ID: claude-sonnet-45
    /// </summary>
    [Description("claude-sonnet-45")]
    ClaudeSonnet45,

    /// <summary>
    /// GPT-5.2 - OpenAI's latest frontier model with adaptive reasoning and strong agentic performance
    /// Model ID: openai-gpt-52
    /// </summary>
    [Description("openai-gpt-52")]
    OpenAIGpt52,

    /// <summary>
    /// GPT-5.2 Codex - OpenAI specialized coding model optimized for advanced software development
    /// Model ID: openai-gpt-52-codex
    /// </summary>
    [Description("openai-gpt-52-codex")]
    OpenAIGpt52Codex,

    /// <summary>
    /// MiniMax M2.1 - Lightweight model optimized for coding and agentic workflows
    /// Model ID: minimax-m21
    /// </summary>
    [Description("minimax-m21")]
    MinimaxM21,

    /// <summary>
    /// MiniMax M2.5 - State-of-the-art model optimized for coding with enhanced reasoning capabilities
    /// Model ID: minimax-m25
    /// </summary>
    [Description("minimax-m25")]
    MinimaxM25,

    /// <summary>
    /// Grok Code Fast 1 - xAI's speedy and economical reasoning model that excels at agentic coding
    /// Model ID: grok-code-fast-1
    /// </summary>
    [Description("grok-code-fast-1")]
    GrokCodeFast1,

    /// <summary>
    /// Qwen3 VL 235B - Vision-language model with MoE architecture, superior visual perception and OCR
    /// Model ID: qwen3-vl-235b-a22b
    /// </summary>
    [Description("qwen3-vl-235b-a22b")]
    Qwen3VL235B,

    /// <summary>
    /// DeepSeek V3.2 - DeepSeek's latest model
    /// </summary>
    [Description("deepseek-v3.2")]
    DeepSeekV32,

    // Obsolete models - kept for backward compatibility
    [Obsolete("This model is no longer available in the Venice AI API. Use Glm47 (zai-org-glm-4.7) instead.")]
    [Description("zai-org-glm-4.6")]
    Glm46,
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

    /// <summary>
    /// Flux 2 Pro - High-quality image generation model
    /// Model ID: flux-2-pro
    /// </summary>
    [Description("flux-2-pro")]
    Flux2Pro,

    /// <summary>
    /// Flux 2 Max - Premium quality image generation model
    /// Model ID: flux-2-max
    /// </summary>
    [Description("flux-2-max")]
    Flux2Max,

    /// <summary>
    /// GPT Image 1.5 - OpenAI's image generation model with 32K prompt limit
    /// Model ID: gpt-image-1-5
    /// </summary>
    [Description("gpt-image-1-5")]
    GptImage15,

    /// <summary>
    /// SeedreamV4.5 - Advanced image generation model
    /// Model ID: seedream-v4
    /// </summary>
    [Description("seedream-v4")]
    SeedreamV4,

    /// <summary>
    /// Background Remover - Tool for removing backgrounds from images
    /// Model ID: bg-remover
    /// </summary>
    [Description("bg-remover")]
    BgRemover,

    /// <summary>
    /// ImagineArt 1.5 Pro - Advanced image generation model with 10K prompt limit
    /// Model ID: imagineart-1.5-pro
    /// </summary>
    [Description("imagineart-1.5-pro")]
    ImagineArt15Pro,

    /// <summary>
    /// Chroma - Fast image generation model
    /// Model ID: chroma
    /// </summary>
    [Description("chroma")]
    Chroma,

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
    /// LTX Video 2.0 19B - Text to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-full-text-to-video
    /// </summary>
    [Description("ltx-2-19b-full-text-to-video")]
    Ltx2_19BFullTextToVideo,

    /// <summary>
    /// LTX Video 2.0 19B - Image to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-full-image-to-video
    /// </summary>
    [Description("ltx-2-19b-full-image-to-video")]
    Ltx2_19BFullImageToVideo,

    /// <summary>
    /// LTX Video 2.0 19B Distilled - Text to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-distilled-text-to-video
    /// </summary>
    [Description("ltx-2-19b-distilled-text-to-video")]
    Ltx2_19BDistilledTextToVideo,

    /// <summary>
    /// LTX Video 2.0 19B Distilled - Image to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-distilled-image-to-video
    /// </summary>
    [Description("ltx-2-19b-distilled-image-to-video")]
    Ltx2_19BDistilledImageToVideo,

    /// <summary>
    /// Wan 2.6 - Image to Video generation with audio support
    /// Model ID: wan-2.6-image-to-video
    /// </summary>
    [Description("wan-2.6-image-to-video")]
    Wan26ImageToVideo,

    /// <summary>
    /// Wan 2.6 Flash - Fast Image to Video generation with audio support
    /// Model ID: wan-2.6-flash-image-to-video
    /// </summary>
    [Description("wan-2.6-flash-image-to-video")]
    Wan26FlashImageToVideo,

    /// <summary>
    /// Wan 2.6 - Text to Video generation with audio support
    /// Model ID: wan-2.6-text-to-video
    /// </summary>
    [Description("wan-2.6-text-to-video")]
    Wan26TextToVideo,

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
    /// Kling O3 Pro - Text to Video generation with cinematic quality
    /// Model ID: kling-o3-pro-text-to-video
    /// </summary>
    [Description("kling-o3-pro-text-to-video")]
    KlingO3ProTextToVideo,

    /// <summary>
    /// Kling O3 Pro - Image to Video generation with cinematic quality
    /// Model ID: kling-o3-pro-image-to-video
    /// </summary>
    [Description("kling-o3-pro-image-to-video")]
    KlingO3ProImageToVideo,

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
    Sora2ProTextToVideo,

    /// <summary>
    /// PixVerse v5.6 - Text to Video generation with cinematic quality
    /// Model ID: pixverse-v5.6-text-to-video
    /// </summary>
    [Description("pixverse-v5.6-text-to-video")]
    PixVerseV56TextToVideo,

    /// <summary>
    /// PixVerse v5.6 - Image to Video generation with cinematic quality
    /// Model ID: pixverse-v5.6-image-to-video
    /// </summary>
    [Description("pixverse-v5.6-image-to-video")]
    PixVerseV56ImageToVideo,

    /// <summary>
    /// PixVerse v5.6 Transition - Image transition effects
    /// Model ID: pixverse-v5.6-transition
    /// </summary>
    [Description("pixverse-v5.6-transition")]
    PixVerseV56Transition,

    /// <summary>
    /// Vidu Q3 - Text to Video generation with cinematic quality
    /// Model ID: vidu-q3-text-to-video
    /// </summary>
    [Description("vidu-q3-text-to-video")]
    ViduQ3TextToVideo,

    /// <summary>
    /// Vidu Q3 - Image to Video generation with cinematic quality
    /// Model ID: vidu-q3-image-to-video
    /// </summary>
    [Description("vidu-q3-image-to-video")]
    ViduQ3ImageToVideo
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
