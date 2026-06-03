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
    /// Venice uncensored model - unrestricted content generation (Dolphin-Mistral-24B-Venice-Edition).
    /// DEPRECATED: This model will be removed on 2026-04-15.
    /// </summary>
    [Obsolete("This model is being deprecated on 2026-04-15. Use VeniceUncensoredRolePlay or another uncensored model instead.")]
    [Description("venice-uncensored")]
    VeniceUncensored,

    /// <summary>
    /// Qwen3 4B - Small, efficient model (Venice Small) with reasoning support.
    /// DEPRECATED: This model is no longer available.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Llama32_3B or another small model instead.")]
    [Description("qwen3-4b")]
    VeniceSmall,

    /// <summary>
    /// Mistral 31 24B - Medium-sized model with vision capabilities (Venice Medium).
    /// DEPRECATED: This model is no longer available.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use MistralSmall3_2_24B or GoogleGemma3_27B instead.")]
    [Description("mistral-31-24b")]
    VeniceMedium,

    /// <summary>
    /// Qwen3 235B - Large, powerful model (Venice Large 1.1) with reasoning support.
    /// DEPRECATED: This model is no longer available. Use <see cref="Qwen3_235B_Instruct"/> or <see cref="Qwen3_235B_Thinking"/> instead.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Qwen3_235B_Instruct (qwen3-235b-a22b-instruct-2507) or Qwen3_235B_Thinking (qwen3-235b-a22b-thinking-2507) instead.")]
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
    /// Grok 4.1 Fast - xAI's fast reasoning model with vision support.
    /// DEPRECATED: This model is no longer available. Use <see cref="Grok4_3"/> instead.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Grok4_3 instead.")]
    [Description("grok-41-fast")]
    Grok41Fast,

    /// <summary>
    /// Grok 4.20 Beta - xAI's latest multimodal reasoning model with strong tool use and 2M-token context window.
    /// DEPRECATED: This model has been renamed. Use <see cref="Grok4_20"/> instead.
    /// Model ID: grok-4-20-beta
    /// </summary>
    [Obsolete("This model has been renamed. Use Grok4_20 (grok-4-20) instead.")]
    [Description("grok-4-20-beta")]
    Grok4_20Beta,

    /// <summary>
    /// Grok 4.20 Multi-Agent Beta - A variant of Grok 4.20 designed for collaborative, agent-based workflows.
    /// DEPRECATED: This model has been renamed. Use <see cref="Grok4_20MultiAgent"/> instead.
    /// Model ID: grok-4-20-multi-agent-beta
    /// </summary>
    [Obsolete("This model has been renamed. Use Grok4_20MultiAgent (grok-4-20-multi-agent) instead.")]
    [Description("grok-4-20-multi-agent-beta")]
    Grok4_20MultiAgentBeta,

    /// <summary>
    /// Gemini 3 Pro Preview - Google DeepMind's premium model with reasoning.
    /// DEPRECATED: This model is no longer available.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Gemini31ProPreview instead.")]
    [Description("gemini-3-pro-preview")]
    Gemini3ProPreview,

    /// <summary>
    /// Gemini 3.1 Pro Preview - Google's latest flagship frontier model with 1M context window,
    /// advancing high-precision multimodal reasoning across text, image, and code.
    /// Model ID: gemini-3-1-pro-preview
    /// </summary>
    [Description("gemini-3-1-pro-preview")]
    Gemini31ProPreview,

    /// <summary>
    /// Claude Opus 4.5 - Anthropic's frontier reasoning model (legacy ID: claude-opus-45).
    /// DEPRECATED: Use ClaudeOpus4_5 (claude-opus-4-5) instead.
    /// </summary>
    [Obsolete("The model ID 'claude-opus-45' is no longer valid. Use ClaudeOpus4_5 with ID 'claude-opus-4-5' instead.")]
    [Description("claude-opus-45")]
    ClaudeOpus45,

    /// <summary>
    /// Claude Opus 4.5 - Anthropic's frontier reasoning model optimized for complex software engineering and agentic workflows.
    /// Model ID: claude-opus-4-5
    /// </summary>
    [Description("claude-opus-4-5")]
    ClaudeOpus4_5,

    /// <summary>
    /// Claude Opus 4.6 - Anthropic's most capable reasoning model with 1M token context window
    /// Model ID: claude-opus-4-6
    /// </summary>
    [Description("claude-opus-4-6")]
    ClaudeOpus46,

    /// <summary>
    /// Claude Opus 4.6 Fast - Speed-optimized variant of Claude Opus 4.6 with lower latency via optimized routing.
    /// Model ID: claude-opus-4-6-fast
    /// </summary>
    [Description("claude-opus-4-6-fast")]
    ClaudeOpus46Fast,

    /// <summary>
    /// OpenAI GPT OSS 120B - OpenAI's open-source model
    /// </summary>
    [Description("openai-gpt-oss-120b")]
    OpenAIGptOss120B,

    /// <summary>
    /// Kimi K2 Thinking - Moonshot AI's reasoning model optimized for code.
    /// DEPRECATED: This model will be removed on 2026-05-06.
    /// </summary>
    [Obsolete("This model is being deprecated on 2026-05-06. Use KimiK25 instead.")]
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
    /// GLM 4.7 Flash Heretic - Uncensored experimental variant of GLM 4.7-Flash, optimized for creative freedom
    /// and unfiltered dialogue with fast inference speed.
    /// Model ID: olafangensan-glm-4.7-flash-heretic
    /// </summary>
    [Description("olafangensan-glm-4.7-flash-heretic")]
    Glm47FlashHeretic,

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
    /// Claude Sonnet 4.5 - Anthropic's balanced model (legacy ID: claude-sonnet-45).
    /// DEPRECATED: Use ClaudeSonnet4_5 (claude-sonnet-4-5) instead.
    /// </summary>
    [Obsolete("The model ID 'claude-sonnet-45' is no longer valid. Use ClaudeSonnet4_5 with ID 'claude-sonnet-4-5' instead.")]
    [Description("claude-sonnet-45")]
    ClaudeSonnet45,

    /// <summary>
    /// Claude Sonnet 4.5 - Anthropic's balanced model offering strong performance on coding, reasoning, and general tasks.
    /// Model ID: claude-sonnet-4-5
    /// </summary>
    [Description("claude-sonnet-4-5")]
    ClaudeSonnet4_5,

    /// <summary>
    /// Claude Sonnet 4.6 - Anthropic's best combination of speed and intelligence with strong performance
    /// on coding, reasoning, and general tasks. Features a 1M token context window and 64K max output tokens.
    /// Model ID: claude-sonnet-4-6
    /// </summary>
    [Description("claude-sonnet-4-6")]
    ClaudeSonnet46,

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
    /// GPT-5.3 Codex - OpenAI specialized coding model built on GPT-5.3, optimized for advanced software development.
    /// Model ID: openai-gpt-53-codex
    /// </summary>
    [Description("openai-gpt-53-codex")]
    OpenAIGpt53Codex,

    /// <summary>
    /// GPT-5.4 - OpenAI's latest frontier model with 1M+ context window and adaptive reasoning.
    /// Model ID: openai-gpt-54
    /// </summary>
    [Description("openai-gpt-54")]
    OpenAIGpt54,

    /// <summary>
    /// GPT-5.4 Mini - A faster, more efficient variant of GPT-5.4 optimized for high-throughput workloads.
    /// Model ID: openai-gpt-54-mini
    /// </summary>
    [Description("openai-gpt-54-mini")]
    OpenAIGpt54Mini,

    /// <summary>
    /// GPT-5.4 Pro - OpenAI's most advanced model with enhanced reasoning for complex, high-stakes tasks.
    /// Model ID: openai-gpt-54-pro
    /// </summary>
    [Description("openai-gpt-54-pro")]
    OpenAIGpt54Pro,

    /// <summary>
    /// GPT-4o (2024-11-20) - OpenAI's multimodal flagship model with vision capabilities and strong reasoning.
    /// Model ID: openai-gpt-4o-2024-11-20
    /// </summary>
    [Description("openai-gpt-4o-2024-11-20")]
    OpenAIGpt4o_Nov2024,

    /// <summary>
    /// GPT-4o Mini (2024-07-18) - OpenAI's cost-efficient small model delivering GPT-4 level intelligence.
    /// Model ID: openai-gpt-4o-mini-2024-07-18
    /// </summary>
    [Description("openai-gpt-4o-mini-2024-07-18")]
    OpenAIGpt4oMini_Jul2024,

    /// <summary>
    /// MiniMax M2.1 - Lightweight model optimized for coding and agentic workflows.
    /// DEPRECATED: This model is no longer available. Use <see cref="MinimaxM3"/> instead.
    /// Model ID: minimax-m21
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use MinimaxM3 instead.")]
    [Description("minimax-m21")]
    MinimaxM21,

    /// <summary>
    /// MiniMax M2.5 - State-of-the-art model optimized for coding with enhanced reasoning capabilities
    /// Model ID: minimax-m25
    /// </summary>
    [Description("minimax-m25")]
    MinimaxM25,

    /// <summary>
    /// MiniMax M2.7 - Next-generation model with advanced agentic capabilities through multi-agent collaboration.
    /// Model ID: minimax-m27
    /// </summary>
    [Description("minimax-m27")]
    MinimaxM27,

    /// <summary>
    /// Grok Code Fast 1 - xAI's speedy and economical reasoning model that excels at agentic coding.
    /// DEPRECATED: This model is no longer available.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Grok41Fast instead.")]
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

    /// <summary>
    /// Google Gemma 4 26B A4B Instruct - Mixture-of-Experts model with 26B total parameters and 4B active per token.
    /// Handles text, image, and video input. Supports 256K context, function calling, and reasoning.
    /// Model ID: google.gemma-4-26b-a4b-it
    /// </summary>
    [Description("google-gemma-4-26b-a4b-it")]
    GoogleGemma4_26B_A4B,

    /// <summary>
    /// Google Gemma 4 31B Instruct - Dense model from Google DeepMind with frontier-level reasoning performance.
    /// Handles text, image, and video input. Supports 256K context, function calling, and thinking modes.
    /// Model ID: google.gemma-4-31b-it
    /// </summary>
    [Description("google-gemma-4-31b-it")]
    GoogleGemma4_31B,

    /// <summary>
    /// Arcee Trinity Large Thinking - Reasoning-optimized 398B-parameter sparse MoE model with ~13B active parameters.
    /// Supports tool calling, multilingual input, and 256K context windows.
    /// Model ID: arcee-trinity-large-thinking
    /// </summary>
    [Description("arcee-trinity-large-thinking")]
    ArceeTrinityLargeThinking,

    /// <summary>
    /// Mercury 2 - Diffusion-based reasoning LLM from Inception delivering over 1,000 tokens per second.
    /// Model ID: mercury-2
    /// </summary>
    [Description("mercury-2")]
    Mercury2,

    /// <summary>
    /// Mistral Small 3.2 24B Instruct - 24B parameter model optimized for efficiency and performance.
    /// Model ID: mistral-small-3-2-24b-instruct
    /// </summary>
    [Description("mistral-small-3-2-24b-instruct")]
    MistralSmall3_2_24B,

    /// <summary>
    /// Mistral Small 4 - Unifies instruction following, reasoning, coding, and vision in a single 119B MoE model.
    /// Supports 256K context and configurable reasoning effort.
    /// Model ID: mistral-small-2603
    /// </summary>
    [Description("mistral-small-2603")]
    MistralSmall2603,

    /// <summary>
    /// NVIDIA Nemotron 3 Nano 30B - Compact and efficient model from NVIDIA with fast inference.
    /// Model ID: nvidia-nemotron-3-nano-30b-a3b
    /// </summary>
    [Description("nvidia-nemotron-3-nano-30b-a3b")]
    NvidiaNemotron3Nano30B,

    /// <summary>
    /// Nemotron Cascade 2 30B A3B - Reasoning-optimized model from NVIDIA with strong reasoning capabilities.
    /// Model ID: nvidia-nemotron-cascade-2-30b-a3b
    /// </summary>
    [Description("nvidia-nemotron-cascade-2-30b-a3b")]
    NvidiaNemotronCascade2_30B,

    /// <summary>
    /// Qwen 3.5 9B - Dense model with 262K native context window and Gated DeltaNet hybrid attention architecture.
    /// Supports 201 languages, thinking/reasoning mode, and function calling.
    /// Model ID: qwen3-5-9b
    /// </summary>
    [Description("qwen3-5-9b")]
    Qwen35_9B,

    /// <summary>
    /// Qwen 3.5 35B A3B - Highly efficient MoE model with 35B total parameters and only 3B active parameters.
    /// Model ID: qwen3-5-35b-a3b
    /// </summary>
    [Description("qwen3-5-35b-a3b")]
    Qwen35_35B_A3B,

    /// <summary>
    /// Qwen 3.5 397B - Alibaba flagship 397B MoE model with 17B active parameters. Excels at complex reasoning, coding, and general knowledge.
    /// Model ID: qwen3-5-397b-a17b
    /// </summary>
    [Description("qwen3-5-397b-a17b")]
    Qwen35_397B_A17B,

    /// <summary>
    /// Qwen 3.6 Plus Uncensored - Alibaba's latest flagship reasoning model with exceptional performance across coding, reasoning, and general knowledge.
    /// Supports mixed reasoning, function calling, and multimodal input.
    /// Model ID: qwen-3-6-plus
    /// </summary>
    [Description("qwen-3-6-plus")]
    Qwen3_6Plus,

    /// <summary>
    /// Qwen 3 Coder 480B Turbo - Turbo variant of Qwen3 Coder 480B, optimized for faster inference on code tasks.
    /// Model ID: qwen3-coder-480b-a35b-instruct-turbo
    /// </summary>
    [Description("qwen3-coder-480b-a35b-instruct-turbo")]
    Qwen3Coder480BTurbo,

    /// <summary>
    /// Venice Role Play Uncensored - Optimized for creative roleplay with maximum freedom.
    /// Designed for immersive storytelling, character interactions, and open-ended creative writing.
    /// Model ID: venice-uncensored-role-play
    /// </summary>
    [Description("venice-uncensored-role-play")]
    VeniceUncensoredRolePlay,

    /// <summary>
    /// Aion 2.0 - DeepSeek V3.2-based model fine-tuned for immersive roleplaying and long-form storytelling.
    /// Model ID: aion-labs.aion-2-0
    /// </summary>
    [Description("aion-labs-aion-2-0")]
    AionLabs2_0,

    /// <summary>
    /// GLM 5.1 - Next-generation large language model from Zhiyuan AI with significantly enhanced reasoning capabilities.
    /// Supports large context windows and fast inference speed.
    /// Model ID: zai-org-glm-5-1
    /// </summary>
    [Description("zai-org-glm-5-1")]
    Glm51,

    /// <summary>
    /// GLM 5 Turbo - Fast inference model from Z.ai tuned for agent-driven environments and production coding workflows.
    /// Model ID: z-ai-glm-5-turbo
    /// </summary>
    [Description("z-ai-glm-5-turbo")]
    ZAIGlm5Turbo,

    /// <summary>
    /// GLM 5V Turbo - Z.ai's first native multimodal agent foundation model, built for vision-based coding and agent-driven tasks.
    /// Supports image, video, and text inputs.
    /// Model ID: z-ai-glm-5v-turbo
    /// </summary>
    [Description("z-ai-glm-5v-turbo")]
    ZAIGlm5VTurbo,

    /// <summary>
    /// Venice Uncensored 1.1 (E2EE TEE) - Venice Uncensored running in a Trusted Execution Environment.
    /// Model ID: e2ee-venice-uncensored-24b-p
    /// </summary>
    [Description("e2ee-venice-uncensored-24b-p")]
    E2EEVeniceUncensored24B,

    /// <summary>
    /// Gemma 3 27B (E2EE TEE) - Google's multimodal model running in a Trusted Execution Environment.
    /// Model ID: e2ee-gemma-3-27b-p
    /// </summary>
    [Description("e2ee-gemma-3-27b-p")]
    E2EEGemma3_27B,

    /// <summary>
    /// GLM 4.7 (E2EE TEE) - Z.AI's flagship model running in a Trusted Execution Environment.
    /// Model ID: e2ee-glm-4-7-p
    /// </summary>
    [Description("e2ee-glm-4-7-p")]
    E2EEGlm47,

    /// <summary>
    /// GLM 4.7 Flash (E2EE TEE) - A 30B-class model optimized for agentic coding running in a Trusted Execution Environment.
    /// Model ID: e2ee-glm-4-7-flash-p
    /// </summary>
    [Description("e2ee-glm-4-7-flash-p")]
    E2EEGlm47Flash,

    /// <summary>
    /// GLM 5 (E2EE TEE) - GLM 5 running in a Trusted Execution Environment.
    /// DEPRECATED: This model is no longer available. Use <see cref="E2EEGlm51"/> instead.
    /// Model ID: e2ee-glm-5
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use E2EEGlm51 instead.")]
    [Description("e2ee-glm-5")]
    E2EEGlm5,

    /// <summary>
    /// GPT OSS 20B (E2EE TEE) - OpenAI's compact open-weight 21B MoE model running in a Trusted Execution Environment.
    /// Model ID: e2ee-gpt-oss-20b-p
    /// </summary>
    [Description("e2ee-gpt-oss-20b-p")]
    E2EEGptOss20B,

    /// <summary>
    /// GPT OSS 120B (E2EE TEE) - OpenAI's open-weight 117B-parameter MoE model running in a Trusted Execution Environment.
    /// Model ID: e2ee-gpt-oss-120b-p
    /// </summary>
    [Description("e2ee-gpt-oss-120b-p")]
    E2EEGptOss120B,

    /// <summary>
    /// Qwen 2.5 7B (E2EE TEE) - Compact model with strong coding, math, and multilingual capabilities running in a Trusted Execution Environment.
    /// Model ID: e2ee-qwen-2-5-7b-p
    /// </summary>
    [Description("e2ee-qwen-2-5-7b-p")]
    E2EEQwen25_7B,

    /// <summary>
    /// Qwen3 30B A3B (E2EE TEE) - MoE model with 30.5B total parameters and 3.3B activated per inference running in a Trusted Execution Environment.
    /// Model ID: e2ee-qwen3-30b-a3b-p
    /// </summary>
    [Description("e2ee-qwen3-30b-a3b-p")]
    E2EEQwen3_30B_A3B,

    /// <summary>
    /// Qwen3 VL 30B A3B (E2EE TEE) - Multimodal model unifying text generation with visual understanding running in a Trusted Execution Environment.
    /// Model ID: e2ee-qwen3-vl-30b-a3b-p
    /// </summary>
    [Description("e2ee-qwen3-vl-30b-a3b-p")]
    E2EEQwen3VL_30B_A3B,

    /// <summary>
    /// Qwen3.5 122B A10B (E2EE TEE) - Qwen3.5 122B A10B running in a Trusted Execution Environment.
    /// DEPRECATED: This model is no longer available.
    /// Model ID: e2ee-qwen3-5-122b-a10b
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API.")]
    [Description("e2ee-qwen3-5-122b-a10b")]
    E2EEQwen35_122B_A10B,

    /// <summary>
    /// Venice Uncensored 1.2 - Updated Venice uncensored model for unrestricted content generation.
    /// Model ID: venice-uncensored-1-2
    /// </summary>
    [Description("venice-uncensored-1-2")]
    VeniceUncensored12,

    /// <summary>
    /// Qwen 3.7 Max - Alibaba's premium flagship model optimized for maximum quality reasoning.
    /// Model ID: qwen-3-7-max
    /// </summary>
    [Description("qwen-3-7-max")]
    Qwen3_7Max,

    /// <summary>
    /// Qwen 3.7 Plus - Alibaba's advanced model with strong reasoning and coding capabilities.
    /// Model ID: qwen-3-7-plus
    /// </summary>
    [Description("qwen-3-7-plus")]
    Qwen3_7Plus,

    /// <summary>
    /// Qwen 3.6 27B - Dense model from Alibaba with 27B parameters.
    /// Model ID: qwen3-6-27b
    /// </summary>
    [Description("qwen3-6-27b")]
    Qwen36_27B,

    /// <summary>
    /// Gemma 4 Uncensored - Uncensored variant of Google's Gemma 4 model.
    /// Model ID: gemma-4-uncensored
    /// </summary>
    [Description("gemma-4-uncensored")]
    Gemma4Uncensored,

    /// <summary>
    /// Grok 4.3 - xAI's fast reasoning model.
    /// Model ID: grok-4-3
    /// </summary>
    [Description("grok-4-3")]
    Grok4_3,

    /// <summary>
    /// Grok 4.20 - xAI's multimodal reasoning model with strong tool use and 2M-token context window.
    /// Model ID: grok-4-20
    /// </summary>
    [Description("grok-4-20")]
    Grok4_20,

    /// <summary>
    /// Grok 4.20 Multi-Agent - xAI's variant of Grok 4.20 designed for collaborative, agent-based workflows.
    /// Model ID: grok-4-20-multi-agent
    /// </summary>
    [Description("grok-4-20-multi-agent")]
    Grok4_20MultiAgent,

    /// <summary>
    /// Grok Build 0.1 - xAI's experimental build model.
    /// Model ID: grok-build-0-1
    /// </summary>
    [Description("grok-build-0-1")]
    GrokBuild0_1,

    /// <summary>
    /// Gemini 3.5 Flash - Google's high-speed model with strong performance.
    /// Model ID: gemini-3-5-flash
    /// </summary>
    [Description("gemini-3-5-flash")]
    Gemini35Flash,

    /// <summary>
    /// Claude Opus 4.7 - Anthropic's advanced reasoning model.
    /// Model ID: claude-opus-4-7
    /// </summary>
    [Description("claude-opus-4-7")]
    ClaudeOpus4_7,

    /// <summary>
    /// Claude Opus 4.7 Fast - Speed-optimized variant of Claude Opus 4.7 with lower latency.
    /// Model ID: claude-opus-4-7-fast
    /// </summary>
    [Description("claude-opus-4-7-fast")]
    ClaudeOpus4_7Fast,

    /// <summary>
    /// Claude Opus 4.8 - Anthropic's latest frontier reasoning model.
    /// Model ID: claude-opus-4-8
    /// </summary>
    [Description("claude-opus-4-8")]
    ClaudeOpus4_8,

    /// <summary>
    /// Claude Opus 4.8 Fast - Speed-optimized variant of Claude Opus 4.8 with lower latency.
    /// Model ID: claude-opus-4-8-fast
    /// </summary>
    [Description("claude-opus-4-8-fast")]
    ClaudeOpus4_8Fast,

    /// <summary>
    /// Kimi K2.6 - Moonshot AI's latest advanced open reasoning model.
    /// Model ID: kimi-k2-6
    /// </summary>
    [Description("kimi-k2-6")]
    KimiK26,

    /// <summary>
    /// DeepSeek V4 Pro - DeepSeek's premium flagship model.
    /// Model ID: deepseek-v4-pro
    /// </summary>
    [Description("deepseek-v4-pro")]
    DeepSeekV4Pro,

    /// <summary>
    /// DeepSeek V4 Flash - DeepSeek's fast inference model.
    /// Model ID: deepseek-v4-flash
    /// </summary>
    [Description("deepseek-v4-flash")]
    DeepSeekV4Flash,

    /// <summary>
    /// GPT-5.5 - OpenAI's next-generation frontier model.
    /// Model ID: openai-gpt-55
    /// </summary>
    [Description("openai-gpt-55")]
    OpenAIGpt55,

    /// <summary>
    /// GPT-5.5 Pro - OpenAI's premium next-generation frontier model.
    /// Model ID: openai-gpt-55-pro
    /// </summary>
    [Description("openai-gpt-55-pro")]
    OpenAIGpt55Pro,

    /// <summary>
    /// MiniMax M3 - MiniMax's latest model.
    /// Model ID: minimax-m3
    /// </summary>
    [Description("minimax-m3")]
    MinimaxM3,

    /// <summary>
    /// Gemma 4 26B A4B Uncensored (E2EE TEE) - Uncensored Gemma 4 26B model running in a Trusted Execution Environment.
    /// Model ID: e2ee-gemma-4-26b-a4b-uncensored-p
    /// </summary>
    [Description("e2ee-gemma-4-26b-a4b-uncensored-p")]
    E2EEGemma4_26B_A4B_Uncensored,

    /// <summary>
    /// Qwen3.6 35B A3B Uncensored (E2EE TEE) - Uncensored Qwen3.6 model running in a Trusted Execution Environment.
    /// Model ID: e2ee-qwen3-6-35b-a3b-uncensored-p
    /// </summary>
    [Description("e2ee-qwen3-6-35b-a3b-uncensored-p")]
    E2EEQwen36_35B_A3B_Uncensored,

    /// <summary>
    /// GLM 5.1 (E2EE TEE) - GLM 5.1 running in a Trusted Execution Environment.
    /// Model ID: e2ee-glm-5-1
    /// </summary>
    [Description("e2ee-glm-5-1")]
    E2EEGlm51,

    /// <summary>
    /// Qwen3.6 35B A3B (E2EE TEE) - Qwen3.6 35B A3B model running in a Trusted Execution Environment.
    /// Model ID: e2ee-qwen3-6-35b-a3b
    /// </summary>
    [Description("e2ee-qwen3-6-35b-a3b")]
    E2EEQwen36_35B_A3B,

    /// <summary>
    /// Gemma 4 31B (E2EE TEE) - Google's Gemma 4 31B model running in a Trusted Execution Environment.
    /// Model ID: e2ee-gemma-4-31b
    /// </summary>
    [Description("e2ee-gemma-4-31b")]
    E2EEGemma4_31B,

    // Obsolete models - kept for backward compatibility
    [Obsolete("This model is no longer available in the Venice AI API. Use Glm47 (zai-org-glm-4.7) instead.")]
    [Description("zai-org-glm-4.6")]
    Glm46,
    [Obsolete("This model is no longer available in the Venice AI API. Use Qwen35_9B or another small model instead.")]
    [Description("qwen-2.5-qwq-32b")]
    QwenReasonning,

    [Obsolete("This model is no longer available in the Venice AI API. Use MistralSmall3_2_24B or GoogleGemma3_27B instead.")]
    [Description("mistral-32-24b")]
    VeniceMedium32,

    [Obsolete("This model is no longer available in the Venice AI API. Use Hermes3Llama405B instead.")]
    [Description("llama-3.1-405b")]
    Llama31_405B,

    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceLarge (qwen3-235b) instead.")]
    [Description("dolphin-2.9.2-qwen2-72b")]
    Dolphin72B,

    [Obsolete("This model is no longer available in the Venice AI API. Use MistralSmall3_2_24B or GoogleGemma3_27B for vision capabilities.")]
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
    /// HiDream - High-quality image generation model.
    /// DEPRECATED: This model is no longer available in the Venice AI API.
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use VeniceSD35 or another image model instead.")]
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
    /// Background Remover - Tool for removing backgrounds from images (legacy ID).
    /// DEPRECATED: Use BriaBgRemover instead.
    /// </summary>
    [Obsolete("This model ID is no longer valid. Use BriaBgRemover with ID 'bria-bg-remover' instead.")]
    [Description("bg-remover")]
    BgRemover,

    /// <summary>
    /// Bria Background Remover - Tool for removing backgrounds from images.
    /// Model ID: bria-bg-remover
    /// </summary>
    [Description("bria-bg-remover")]
    BriaBgRemover,

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

    /// <summary>
    /// Recraft V4 - Advanced image generation model with 10K prompt limit
    /// Model ID: recraft-v4
    /// </summary>
    [Description("recraft-v4")]
    RecraftV4,

    /// <summary>
    /// Recraft V4 Pro - Premium quality image generation model with 10K prompt limit
    /// Model ID: recraft-v4-pro
    /// </summary>
    [Description("recraft-v4-pro")]
    RecraftV4Pro,

    /// <summary>
    /// Hunyuan Image V3 - Advanced image generation model from Tencent.
    /// Model ID: hunyuan-image-v3
    /// </summary>
    [Description("hunyuan-image-v3")]
    HunyuanImageV3,

    /// <summary>
    /// Nano Banana 2 - Image generation model.
    /// Model ID: nano-banana-2
    /// </summary>
    [Description("nano-banana-2")]
    NanoBanana2,

    /// <summary>
    /// Lustify V8 - Updated Lustify model for adult content.
    /// Model ID: lustify-v8
    /// </summary>
    [Description("lustify-v8")]
    LustifyV8,

    /// <summary>
    /// SeedreamV5 Lite - Advanced image generation model (lite variant).
    /// Model ID: seedream-v5-lite
    /// </summary>
    [Description("seedream-v5-lite")]
    SeedreamV5Lite,

    /// <summary>
    /// Qwen Image 2 - Image generation model from Alibaba.
    /// Model ID: qwen-image-2
    /// </summary>
    [Description("qwen-image-2")]
    QwenImage2,

    /// <summary>
    /// Qwen Image 2 Pro - Premium image generation model from Alibaba.
    /// Model ID: qwen-image-2-pro
    /// </summary>
    [Description("qwen-image-2-pro")]
    QwenImage2Pro,

    /// <summary>
    /// Grok Imagine Image - xAI image generation model.
    /// Model ID: grok-imagine-image
    /// </summary>
    [Description("grok-imagine-image")]
    GrokImagineImage,

    /// <summary>
    /// Grok Imagine Image Pro - xAI premium image generation model.
    /// DEPRECATED: This model is no longer available in the Venice AI API. Use <see cref="GrokImagineImageQuality"/> instead.
    /// Model ID: grok-imagine-image-pro
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use GrokImagineImageQuality instead.")]
    [Description("grok-imagine-image-pro")]
    GrokImagineImagePro,

    /// <summary>
    /// Wan 2.7 Text To Image - Text to image generation model.
    /// Model ID: wan-2-7-text-to-image
    /// </summary>
    [Description("wan-2-7-text-to-image")]
    Wan27TextToImage,

    /// <summary>
    /// Wan 2.7 Pro Text To Image - Premium text to image generation model.
    /// Model ID: wan-2-7-pro-text-to-image
    /// </summary>
    [Description("wan-2-7-pro-text-to-image")]
    Wan27ProTextToImage,

    /// <summary>
    /// Grok Imagine Image Quality - xAI high-quality image generation model.
    /// Model ID: grok-imagine-image-quality
    /// </summary>
    [Description("grok-imagine-image-quality")]
    GrokImagineImageQuality,

    /// <summary>
    /// GPT Image 2 - OpenAI's latest image generation model.
    /// Model ID: gpt-image-2
    /// </summary>
    [Description("gpt-image-2")]
    GptImage2,

    /// <summary>
    /// Ideogram V4 - Advanced image generation model from Ideogram.
    /// Model ID: ideogram-v4
    /// </summary>
    [Description("ideogram-v4")]
    IdeogramV4,

    /// <summary>
    /// Krea V2 Large - Large image generation model from Krea.
    /// Model ID: krea-v2-large
    /// </summary>
    [Description("krea-v2-large")]
    KreaV2Large,

    /// <summary>
    /// Krea V2 Medium - Medium image generation model from Krea.
    /// Model ID: krea-v2-medium
    /// </summary>
    [Description("krea-v2-medium")]
    KreaV2Medium,

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
    ViduQ3ImageToVideo,

    /// <summary>
    /// Kling V3 Pro - Text to Video generation.
    /// Model ID: kling-v3-pro-text-to-video
    /// </summary>
    [Description("kling-v3-pro-text-to-video")]
    KlingV3ProTextToVideo,

    /// <summary>
    /// Kling V3 Pro - Image to Video generation.
    /// Model ID: kling-v3-pro-image-to-video
    /// </summary>
    [Description("kling-v3-pro-image-to-video")]
    KlingV3ProImageToVideo,

    /// <summary>
    /// Kling V3 Standard - Text to Video generation.
    /// Model ID: kling-v3-standard-text-to-video
    /// </summary>
    [Description("kling-v3-standard-text-to-video")]
    KlingV3StandardTextToVideo,

    /// <summary>
    /// Kling V3 Standard - Image to Video generation.
    /// Model ID: kling-v3-standard-image-to-video
    /// </summary>
    [Description("kling-v3-standard-image-to-video")]
    KlingV3StandardImageToVideo,

    /// <summary>
    /// Kling O3 Standard - Text to Video generation.
    /// Model ID: kling-o3-standard-text-to-video
    /// </summary>
    [Description("kling-o3-standard-text-to-video")]
    KlingO3StandardTextToVideo,

    /// <summary>
    /// Kling O3 Standard - Image to Video generation.
    /// Model ID: kling-o3-standard-image-to-video
    /// </summary>
    [Description("kling-o3-standard-image-to-video")]
    KlingO3StandardImageToVideo,

    /// <summary>
    /// Kling O3 Standard - Reference to Video generation.
    /// Model ID: kling-o3-standard-reference-to-video
    /// </summary>
    [Description("kling-o3-standard-reference-to-video")]
    KlingO3StandardReferenceToVideo,

    /// <summary>
    /// Kling O3 Pro - Reference to Video generation with cinematic quality.
    /// Model ID: kling-o3-pro-reference-to-video
    /// </summary>
    [Description("kling-o3-pro-reference-to-video")]
    KlingO3ProReferenceToVideo,

    /// <summary>
    /// LTX Video 2.0 V2.3 Fast - Image to Video generation.
    /// Model ID: ltx-2-v2-3-fast-image-to-video
    /// </summary>
    [Description("ltx-2-v2-3-fast-image-to-video")]
    Ltx2V23FastImageToVideo,

    /// <summary>
    /// LTX Video 2.0 V2.3 Fast - Text to Video generation.
    /// Model ID: ltx-2-v2-3-fast-text-to-video
    /// </summary>
    [Description("ltx-2-v2-3-fast-text-to-video")]
    Ltx2V23FastTextToVideo,

    /// <summary>
    /// LTX Video 2.0 V2.3 Full Quality - Image to Video generation.
    /// Model ID: ltx-2-v2-3-full-image-to-video
    /// </summary>
    [Description("ltx-2-v2-3-full-image-to-video")]
    Ltx2V23FullImageToVideo,

    /// <summary>
    /// LTX Video 2.0 V2.3 Full Quality - Text to Video generation.
    /// Model ID: ltx-2-v2-3-full-text-to-video
    /// </summary>
    [Description("ltx-2-v2-3-full-text-to-video")]
    Ltx2V23FullTextToVideo,

    /// <summary>
    /// Seedance 1.5 Pro - Image to Video generation.
    /// Model ID: seedance-1-5-pro-image-to-video
    /// </summary>
    [Description("seedance-1-5-pro-image-to-video")]
    Seedance15ProImageToVideo,

    /// <summary>
    /// Seedance 1.5 Pro - Text to Video generation.
    /// Model ID: seedance-1-5-pro-text-to-video
    /// </summary>
    [Description("seedance-1-5-pro-text-to-video")]
    Seedance15ProTextToVideo,

    /// <summary>
    /// Seedance 2.0 - Image to Video generation.
    /// Model ID: seedance-2-0-image-to-video
    /// </summary>
    [Description("seedance-2-0-image-to-video")]
    Seedance20ImageToVideo,

    /// <summary>
    /// Seedance 2.0 - Text to Video generation.
    /// Model ID: seedance-2-0-text-to-video
    /// </summary>
    [Description("seedance-2-0-text-to-video")]
    Seedance20TextToVideo,

    /// <summary>
    /// Seedance 2.0 - Reference to Video generation.
    /// Model ID: seedance-2-0-reference-to-video
    /// </summary>
    [Description("seedance-2-0-reference-to-video")]
    Seedance20ReferenceToVideo,

    /// <summary>
    /// Seedance 2.0 Fast - Image to Video generation.
    /// Model ID: seedance-2-0-fast-image-to-video
    /// </summary>
    [Description("seedance-2-0-fast-image-to-video")]
    Seedance20FastImageToVideo,

    /// <summary>
    /// Seedance 2.0 Fast - Text to Video generation.
    /// Model ID: seedance-2-0-fast-text-to-video
    /// </summary>
    [Description("seedance-2-0-fast-text-to-video")]
    Seedance20FastTextToVideo,

    /// <summary>
    /// Seedance 2.0 Fast - Reference to Video generation.
    /// Model ID: seedance-2-0-fast-reference-to-video
    /// </summary>
    [Description("seedance-2-0-fast-reference-to-video")]
    Seedance20FastReferenceToVideo,

    /// <summary>
    /// Grok Imagine - Image to Video generation.
    /// Model ID: grok-imagine-image-to-video
    /// </summary>
    [Description("grok-imagine-image-to-video")]
    GrokImagineImageToVideo,

    /// <summary>
    /// Grok Imagine - Text to Video generation.
    /// Model ID: grok-imagine-text-to-video
    /// </summary>
    [Description("grok-imagine-text-to-video")]
    GrokImagineTextToVideo,

    /// <summary>
    /// Grok Imagine - Reference to Video generation.
    /// Model ID: grok-imagine-reference-to-video
    /// </summary>
    [Description("grok-imagine-reference-to-video")]
    GrokImagineReferenceToVideo,

    /// <summary>
    /// Topaz Video Upscale - Video upscaling tool.
    /// Model ID: topaz-video-upscale
    /// </summary>
    [Description("topaz-video-upscale")]
    TopazVideoUpscale,

    /// <summary>
    /// Wan 2.7 - Image to Video generation.
    /// Model ID: wan-2-7-image-to-video
    /// </summary>
    [Description("wan-2-7-image-to-video")]
    Wan27ImageToVideo,

    /// <summary>
    /// Wan 2.7 - Text to Video generation.
    /// Model ID: wan-2-7-text-to-video
    /// </summary>
    [Description("wan-2-7-text-to-video")]
    Wan27TextToVideo,

    /// <summary>
    /// Wan 2.7 - Reference to Video generation.
    /// Model ID: wan-2-7-reference-to-video
    /// </summary>
    [Description("wan-2-7-reference-to-video")]
    Wan27ReferenceToVideo,

    /// <summary>
    /// Wan 2.7 - Video to Video generation.
    /// Model ID: wan-2-7-video-to-video
    /// </summary>
    [Description("wan-2-7-video-to-video")]
    Wan27VideoToVideo,

    /// <summary>
    /// Wan 2.7 Uncensored - Image to Video generation (uncensored).
    /// Model ID: wan-2-7-uncensored-image-to-video
    /// </summary>
    [Description("wan-2-7-uncensored-image-to-video")]
    Wan27UncensoredImageToVideo,

    /// <summary>
    /// HappyHorse 1.0 - Text to Video generation.
    /// Model ID: happyhorse-1-0-text-to-video
    /// </summary>
    [Description("happyhorse-1-0-text-to-video")]
    HappyHorse10TextToVideo,

    /// <summary>
    /// HappyHorse 1.0 - Image to Video generation.
    /// Model ID: happyhorse-1-0-image-to-video
    /// </summary>
    [Description("happyhorse-1-0-image-to-video")]
    HappyHorse10ImageToVideo,

    /// <summary>
    /// HappyHorse 1.0 - Reference to Video generation.
    /// Model ID: happyhorse-1-0-reference-to-video
    /// </summary>
    [Description("happyhorse-1-0-reference-to-video")]
    HappyHorse10ReferenceToVideo,

    /// <summary>
    /// HappyHorse 1.0 - Video to Video generation.
    /// Model ID: happyhorse-1-0-video-to-video
    /// </summary>
    [Description("happyhorse-1-0-video-to-video")]
    HappyHorse10VideoToVideo,

    /// <summary>
    /// Grok Imagine - Text to Video generation (private).
    /// Model ID: grok-imagine-text-to-video-private
    /// </summary>
    [Description("grok-imagine-text-to-video-private")]
    GrokImagineTextToVideoPrivate,

    /// <summary>
    /// Grok Imagine - Image to Video generation (private).
    /// Model ID: grok-imagine-image-to-video-private
    /// </summary>
    [Description("grok-imagine-image-to-video-private")]
    GrokImagineImageToVideoPrivate,

    /// <summary>
    /// Grok Imagine - Reference to Video generation (private).
    /// Model ID: grok-imagine-reference-to-video-private
    /// </summary>
    [Description("grok-imagine-reference-to-video-private")]
    GrokImagineReferenceToVideoPrivate,

    /// <summary>
    /// Grok Imagine - Video to Video generation (private).
    /// Model ID: grok-imagine-video-to-video-private
    /// </summary>
    [Description("grok-imagine-video-to-video-private")]
    GrokImagineVideoToVideoPrivate,

    /// <summary>
    /// Grok Imagine 1.5 - Image to Video generation (private).
    /// Model ID: grok-imagine-1-5-image-to-video-private
    /// </summary>
    [Description("grok-imagine-1-5-image-to-video-private")]
    GrokImagine15ImageToVideoPrivate,

    /// <summary>
    /// Kling V3 4K - Text to Video generation in 4K.
    /// Model ID: kling-v3-4k-text-to-video
    /// </summary>
    [Description("kling-v3-4k-text-to-video")]
    KlingV3_4KTextToVideo,

    /// <summary>
    /// Kling V3 4K - Reference to Video generation in 4K.
    /// Model ID: kling-v3-4k-reference-to-video
    /// </summary>
    [Description("kling-v3-4k-reference-to-video")]
    KlingV3_4KReferenceToVideo,

    /// <summary>
    /// Kling O3 4K - Text to Video generation in 4K.
    /// Model ID: kling-o3-4k-text-to-video
    /// </summary>
    [Description("kling-o3-4k-text-to-video")]
    KlingO3_4KTextToVideo,

    /// <summary>
    /// Kling O3 4K - Image to Video generation in 4K.
    /// Model ID: kling-o3-4k-image-to-video
    /// </summary>
    [Description("kling-o3-4k-image-to-video")]
    KlingO3_4KImageToVideo,

    /// <summary>
    /// Kling O3 4K - Reference to Video generation in 4K.
    /// Model ID: kling-o3-4k-reference-to-video
    /// </summary>
    [Description("kling-o3-4k-reference-to-video")]
    KlingO3_4KReferenceToVideo,

    /// <summary>
    /// Kling O3 Standard - Motion Control video generation.
    /// Model ID: kling-o3-standard-motion-control
    /// </summary>
    [Description("kling-o3-standard-motion-control")]
    KlingO3StandardMotionControl,

    /// <summary>
    /// Kling V3 Pro - Motion Control video generation.
    /// Model ID: kling-v3-pro-motion-control
    /// </summary>
    [Description("kling-v3-pro-motion-control")]
    KlingV3ProMotionControl,

    /// <summary>
    /// Kling V3 Standard - Motion Control video generation.
    /// Model ID: kling-v3-standard-motion-control
    /// </summary>
    [Description("kling-v3-standard-motion-control")]
    KlingV3StandardMotionControl,

    /// <summary>
    /// PixVerse C1 - Text to Video generation.
    /// Model ID: pixverse-c1-text-to-video
    /// </summary>
    [Description("pixverse-c1-text-to-video")]
    PixVerseC1TextToVideo,

    /// <summary>
    /// PixVerse C1 - Image to Video generation.
    /// Model ID: pixverse-c1-image-to-video
    /// </summary>
    [Description("pixverse-c1-image-to-video")]
    PixVerseC1ImageToVideo,

    /// <summary>
    /// PixVerse C1 - Reference to Video generation.
    /// Model ID: pixverse-c1-reference-to-video
    /// </summary>
    [Description("pixverse-c1-reference-to-video")]
    PixVerseC1ReferenceToVideo,

    /// <summary>
    /// PixVerse C1 - Transition effects.
    /// Model ID: pixverse-c1-transition
    /// </summary>
    [Description("pixverse-c1-transition")]
    PixVerseC1Transition,

    /// <summary>
    /// Runway Gen4.5 - Image to Video generation.
    /// Model ID: runway-gen4-5
    /// </summary>
    [Description("runway-gen4-5")]
    RunwayGen45,

    /// <summary>
    /// Runway Gen4.5 Text - Text to Video generation.
    /// Model ID: runway-gen4-5-text
    /// </summary>
    [Description("runway-gen4-5-text")]
    RunwayGen45Text,

    /// <summary>
    /// Runway Gen4 Turbo - Fast video generation.
    /// Model ID: runway-gen4-turbo
    /// </summary>
    [Description("runway-gen4-turbo")]
    RunwayGen4Turbo,

    /// <summary>
    /// Runway Gen4 Aleph - Advanced video generation.
    /// Model ID: runway-gen4-aleph
    /// </summary>
    [Description("runway-gen4-aleph")]
    RunwayGen4Aleph
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
