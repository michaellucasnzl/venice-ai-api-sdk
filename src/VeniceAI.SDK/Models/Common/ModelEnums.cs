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
    Inpaint,

    [Description("music")]
    Music
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
    [Obsolete("This model is no longer available in the Venice AI API. Use Qwen3Coder480BTurbo (qwen3-coder-480b-a35b-instruct-turbo) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use ClaudeOpus5Fast (claude-opus-5-fast) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use Qwen38Max or another large reasoning model instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use NvidiaNemotron3Ultra550B (nvidia-nemotron-3-ultra-550b-a55b) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use AionLabs3_0 (aion-labs-aion-3-0) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use a newer Venice uncensored model instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use E2EEGlm52 (e2ee-glm-5-2-p) instead.")]
    [Description("e2ee-glm-4-7-p")]
    E2EEGlm47,

    /// <summary>
    /// GLM 4.7 Flash (E2EE TEE) - A 30B-class model optimized for agentic coding running in a Trusted Execution Environment.
    /// Model ID: e2ee-glm-4-7-flash-p
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use E2EEGlm52 (e2ee-glm-5-2-p) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use E2EEQwen36_27B (e2ee-qwen3-6-27b) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use ClaudeOpus5Fast (claude-opus-5-fast) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use MinimaxM3Preview (minimax-m3-preview) instead.")]
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

    /// <summary>
    /// Claude Fable 5 - Anthropic's most capable widely released model, designed for demanding reasoning
    /// and long-horizon agentic work with a 1M token context window and always-on adaptive thinking.
    /// Model ID: claude-fable-5
    /// </summary>
    [Description("claude-fable-5")]
    ClaudeFable5,

    /// <summary>
    /// Kimi K2.7 Code - Moonshot AI's coding-focused agentic model built on Kimi K2.6 with 1T total
    /// parameters and 32B active parameters. Always operates in thinking mode and supports text and
    /// image input with 256K context.
    /// Model ID: kimi-k2-7-code
    /// </summary>
    [Description("kimi-k2-7-code")]
    KimiK27Code,

    /// <summary>
    /// MiniMax M3 Preview - MiniMax's 1.4T-parameter frontier model preview for coding, agentic workflows,
    /// and complex reasoning, served at fp8 with a 512K context window.
    /// Model ID: minimax-m3-preview
    /// </summary>
    [Description("minimax-m3-preview")]
    MinimaxM3Preview,

    /// <summary>
    /// NVIDIA Nemotron 3 Ultra 550B A55B - Built for frontier reasoning, orchestration, coding agents,
    /// deep research, and complex enterprise workflows. Up to 5x faster inference with up to 1M token
    /// context support.
    /// Model ID: nvidia-nemotron-3-ultra-550b-a55b
    /// </summary>
    [Description("nvidia-nemotron-3-ultra-550b-a55b")]
    NvidiaNemotron3Ultra550B,

    /// <summary>
    /// Hy3 Preview - Tencent Hy Team's 295B-parameter Mixture-of-Experts model with 21B active parameters.
    /// Excels at complex reasoning, instruction following, context learning, coding, and agent tasks
    /// with 256K context.
    /// Model ID: tencent-hy3-preview
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use a newer large reasoning model instead.")]
    [Description("tencent-hy3-preview")]
    TencentHy3Preview,

    /// <summary>
    /// MiMo V2.5 - Xiaomi's native omnimodal model with strong agentic capabilities, supporting text,
    /// image, video, and audio understanding. Sparse Mixture-of-Experts backbone with 310B total and
    /// 15B active parameters and up to 1M token context.
    /// Model ID: xiaomi-mimo-v2-5
    /// </summary>
    [Description("xiaomi-mimo-v2-5")]
    XiaomiMimoV25,

    /// <summary>
    /// Aion 3.0 is a multi-model roleplaying and storytelling system from AionLabs, built on the GLM family of models. Multiple specialized models collaborate on each response to produce stronger narrative structure and more compelling tension and conflict. It handles mature and darker themes with nuance and supports tool calling for richer interactive fiction.
    /// Model ID: aion-labs-aion-3-0
    /// </summary>
    [Description("aion-labs-aion-3-0")]
    AionLabs3_0,

    /// <summary>
    /// Aion 3.0 Mini is a multi-model roleplaying and storytelling system from AionLabs, built on the DeepSeek family of models. Multiple specialized models collaborate on each response to produce stronger narrative structure and more compelling tension and conflict at lower cost. It handles mature and darker themes with nuance and supports tool calling for richer interactive fiction.
    /// Model ID: aion-labs-aion-3-0-mini
    /// </summary>
    [Description("aion-labs-aion-3-0-mini")]
    AionLabs3_0Mini,

    /// <summary>
    /// Claude Opus 5 is Anthropic's most capable model in the Opus family. It delivers major gains over Opus 4.8 in agentic coding, professional knowledge work, and long-horizon reasoning, with a 1M token context window, 128K max output tokens, adaptive thinking, and strong multimodal capabilities.
    /// Model ID: claude-opus-5
    /// </summary>
    [Description("claude-opus-5")]
    ClaudeOpus5,

    /// <summary>
    /// Claude Opus 5 (Fast) is a speed-optimized variant of Anthropic's most capable Opus model, offering the same 1M token context window and strong performance across agentic coding, professional knowledge work, and long-horizon reasoning — with lower latency.
    /// Model ID: claude-opus-5-fast
    /// </summary>
    [Description("claude-opus-5-fast")]
    ClaudeOpus5Fast,

    /// <summary>
    /// Claude Sonnet 5 is Anthropic's latest Sonnet model, substantially improving on Sonnet 4.6 in coding and agentic work and reaching near-Opus quality on many tasks. It features a 1M token context window, adaptive thinking, and strong document and vision understanding.
    /// Model ID: claude-sonnet-5
    /// </summary>
    [Description("claude-sonnet-5")]
    ClaudeSonnet5,

    /// <summary>
    /// DeepSeek V4 Flash is an efficiency-optimized 284B-parameter Mixture-of-Experts model with 13B active parameters and a 1M-token context window. Tuned for fast inference and high-throughput workloads while maintaining strong reasoning and coding performance.
    /// Model ID: deepseek-v4-flash-0731
    /// </summary>
    [Description("deepseek-v4-flash-0731")]
    DeepSeekV4Flash0731,

    /// <summary>
    /// DeepSeek V4 Flash is an efficiency-optimized 284B-parameter Mixture-of-Experts model with 13B active parameters and a 1M-token context window. Tuned for fast inference and high-throughput workloads while maintaining strong reasoning and coding performance.
    /// Model ID: deepseek-v4-flash-0731-fast
    /// </summary>
    [Description("deepseek-v4-flash-0731-fast")]
    DeepSeekV4Flash0731Fast,

    /// <summary>
    /// DeepSeek V4 Pro is a 1.6T-parameter Mixture-of-Experts model with 49B active parameters and a 1M-token context window. Built for advanced reasoning, coding, and long-horizon agentic workflows with a hybrid attention system for efficient long-context processing.
    /// Model ID: deepseek-v4-pro-0813
    /// </summary>
    [Description("deepseek-v4-pro-0813")]
    DeepSeekV4Pro0813,

    /// <summary>
    /// DeepSeek V4 Flash running in a Trusted Execution Environment (TEE). Hardware attestation evidence is available for independent verification of enclave identity and configuration.
    /// Model ID: e2ee-deepseek-v4-flash
    /// </summary>
    [Description("e2ee-deepseek-v4-flash")]
    E2EEDeepSeekV4Flash,

    /// <summary>
    /// GLM 5.2 running in a Trusted Execution Environment (TEE). Z.AI's flagship model for long-horizon tasks with enhanced reasoning and project-level engineering context, with hardware attestation evidence available for independent verification.
    /// Model ID: e2ee-glm-5-2-p
    /// </summary>
    [Description("e2ee-glm-5-2-p")]
    E2EEGlm52,

    /// <summary>
    /// Qwen 3.6 27B FP8 running in a Trusted Execution Environment (TEE). Hardware attestation evidence is available for independent verification of enclave identity and configuration.
    /// Model ID: e2ee-qwen3-6-27b
    /// </summary>
    [Description("e2ee-qwen3-6-27b")]
    E2EEQwen36_27B,

    /// <summary>
    /// Gemini 3.5 Flash-Lite is the fastest, most cost-efficient Gemini 3.5 model with 1M context, ideal for everyday questions, summarization, and lightweight coding tasks.
    /// Model ID: gemini-3-5-flash-lite
    /// </summary>
    [Description("gemini-3-5-flash-lite")]
    Gemini35FlashLite,

    /// <summary>
    /// Gemini 3.6 Flash is a high speed, high value thinking model with 1M context, designed for agentic workflows, multi-turn chat, and coding assistance. It delivers near Pro level reasoning with substantially lower latency.
    /// Model ID: gemini-3-6-flash
    /// </summary>
    [Description("gemini-3-6-flash")]
    Gemini36Flash,

    /// <summary>
    /// Gemini 3.7 Flash is Google's most capable Flash model, built for complex coding, agentic workflows, and reliable multi-step execution, with 1M context and tunable thinking.
    /// Model ID: gemini-3-7-flash
    /// </summary>
    [Description("gemini-3-7-flash")]
    Gemini37Flash,

    /// <summary>
    /// Grok 4.5 is xAI's intelligent coding model for agentic software engineering and workflow tasks, with function calling, structured outputs, and a 500K-token context window.
    /// Model ID: grok-4-5
    /// </summary>
    [Description("grok-4-5")]
    Grok4_5,

    /// <summary>
    /// Grok 4.6 is xAI's multimodal chat and reasoning model with function calling, structured outputs, adjustable reasoning effort (low/medium/high/xhigh), and a 500K-token context window.
    /// Model ID: grok-4-6
    /// </summary>
    [Description("grok-4-6")]
    Grok4_6,

    /// <summary>
    /// Inkling is a general-purpose multimodal model from Thinking Machines Lab that accepts text, image, and audio inputs and generates text. It is a 66-layer sparse MoE (975B total / 41B active) with hybrid local/global attention, 512K context, and variable thinking effort — suited for chat, coding, tool use, and agentic workflows. Video input is not supported on Venice.
    /// Model ID: inkling
    /// </summary>
    [Description("inkling")]
    Inkling,

    /// <summary>
    /// Kimi K3 is an ultra-large-scale, open-weight multimodal reasoning model from Moonshot AI. It is suited for complex coding, knowledge work, and long-horizon agentic workflows, and is particularly strong at navigating large repositories, using tools, debugging, and iterating against images, logs, tests, and runtime feedback.
    /// Model ID: kimi-k3
    /// </summary>
    [Description("kimi-k3")]
    KimiK3,

    /// <summary>
    /// Kimi K3 is an ultra-large-scale, open-weight multimodal reasoning model from Moonshot AI. It is suited for complex coding, knowledge work, and long-horizon agentic workflows, and is particularly strong at navigating large repositories, using tools, debugging, and iterating against images, logs, tests, and runtime feedback.
    /// Model ID: kimi-k3-fast-api
    /// </summary>
    [Description("kimi-k3-fast-api")]
    KimiK3FastApi,

    /// <summary>
    /// GPT-5.6 Luna is a fast, cost-efficient model in OpenAI's GPT-5.6 series. It is suited for high-volume, latency-sensitive tasks such as chat, classification, and lightweight agentic workflows, providing capable reasoning for its price tier.
    /// Model ID: openai-gpt-56-luna
    /// </summary>
    [Description("openai-gpt-56-luna")]
    OpenAIGpt56Luna,

    /// <summary>
    /// GPT-5.6 Luna Pro is the same underlying model as GPT-5.6 Luna, served with reasoning.mode set to pro for higher-quality responses on complex tasks.
    /// Model ID: openai-gpt-56-luna-pro
    /// </summary>
    [Description("openai-gpt-56-luna-pro")]
    OpenAIGpt56LunaPro,

    /// <summary>
    /// GPT-5.6 Sol is the flagship model in OpenAI's GPT-5.6 series. It is suited for complex reasoning, coding, and agentic workflows, and is particularly strong at command-line and multi-step coding tasks and long-horizon problem solving.
    /// Model ID: openai-gpt-56-sol
    /// </summary>
    [Description("openai-gpt-56-sol")]
    OpenAIGpt56Sol,

    /// <summary>
    /// GPT-5.6 Sol Pro is the same underlying model as GPT-5.6 Sol, served with reasoning.mode set to pro for higher-quality responses on complex tasks.
    /// Model ID: openai-gpt-56-sol-pro
    /// </summary>
    [Description("openai-gpt-56-sol-pro")]
    OpenAIGpt56SolPro,

    /// <summary>
    /// GPT-5.6 Terra is a balanced model in OpenAI's GPT-5.6 series, positioned between the flagship Sol tier and the cost-efficient Luna tier. It is suited for everyday coding, reasoning, and agentic tasks where capability and cost need to be balanced.
    /// Model ID: openai-gpt-56-terra
    /// </summary>
    [Description("openai-gpt-56-terra")]
    OpenAIGpt56Terra,

    /// <summary>
    /// GPT-5.6 Terra Pro is the same underlying model as GPT-5.6 Terra, served with reasoning.mode set to pro for higher-quality responses on complex tasks.
    /// Model ID: openai-gpt-56-terra-pro
    /// </summary>
    [Description("openai-gpt-56-terra-pro")]
    OpenAIGpt56TerraPro,

    /// <summary>
    /// Qwen 3.8 2.4T is Alibaba's open-weight 2.4-trillion-parameter MoE model (95B active), with major gains in software engineering, research, and long-horizon agentic tasks. It is text-only, requires thinking mode, and supports a 262K-token context window.
    /// Model ID: qwen-3-8-2-4t-a95b
    /// </summary>
    [Description("qwen-3-8-2-4t-a95b")]
    Qwen38_2_4T_A95B,

    /// <summary>
    /// Qwen 3.8 27B is a native vision-language dense model with 27B parameters. It improves coding, professional work, research, and long-horizon agentic tasks, with flexible thinking control and image and video understanding. It supports a native 262K-token context window.
    /// Model ID: qwen-3-8-27b
    /// </summary>
    [Description("qwen-3-8-27b")]
    Qwen38_27B,

    /// <summary>
    /// Qwen 3.8 Max is Alibaba's flagship 2.4-trillion-parameter MoE model, with major gains over Qwen 3.7 Max in software engineering and office-productivity workflows and strong long-horizon, multi-agent performance. It accepts both text and vision-language input (images and video), operates in thinking mode only, and supports a 1M-token context window.
    /// Model ID: qwen-3-8-max
    /// </summary>
    [Description("qwen-3-8-max")]
    Qwen38Max,

    /// <summary>
    /// Qwen 3.6 35B A3B is a fast mixture-of-experts model with 35B total parameters and ~3B active per token. Strong at agentic coding, STEM reasoning, and tool use, with a native 256K context window.
    /// Model ID: qwen3-6-35b-a3b
    /// </summary>
    [Description("qwen3-6-35b-a3b")]
    Qwen36_35B_A3B,

    /// <summary>
    /// Seed 2.1 Turbo (Dola-Seed-2.1) is ByteDance’s next-generation multimodal model for the coding and agent era, with engineering-grade code delivery, long-horizon agent execution, and upgraded GUI and video understanding. Supports text, image, and video inputs with a 256K context window.
    /// Model ID: seed-2-1-turbo
    /// </summary>
    [Description("seed-2-1-turbo")]
    Seed21Turbo,

    /// <summary>
    /// Ox Alpha is a reasoning model designed for coding, sustained agentic work, and production workloads. It is suited for long-horizon software engineering, complex reasoning, and workflows that combine text with visual context.
    /// Model ID: stealth-ox-alpha
    /// </summary>
    [Description("stealth-ox-alpha")]
    StealthOxAlpha,

    /// <summary>
    /// GLM-5.3 is a large-scale reasoning model from Z.ai, built for complex software engineering and long-horizon agent tasks. It supports text input and output with a 1M-token context window, and improves on GLM-5.2 in coding and in the balance between performance and token efficiency.
    /// Model ID: z-ai-glm-5-3
    /// </summary>
    [Description("z-ai-glm-5-3")]
    ZAIGlm53,

    /// <summary>
    /// GLM-5.2 is the next-generation large language model developed by Zhiyuan AI, featuring significantly enhanced reasoning capabilities, improved instruction following, and support for multiple languages. Supports large context windows for processing extensive text and detailed analysis with fast inference speed.
    /// Model ID: zai-org-glm-5-2
    /// </summary>
    [Description("zai-org-glm-5-2")]
    Glm52,

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

    /// <summary>
    /// Grok Imagine 2.0 image generation model.
    /// Model ID: grok-imagine-image-2-0
    /// </summary>
    [Description("grok-imagine-image-2-0")]
    GrokImagineImage2_0,

    /// <summary>
    /// Krea 2 Turbo image generation model.
    /// Model ID: krea-2-turbo
    /// </summary>
    [Description("krea-2-turbo")]
    Krea2Turbo,

    /// <summary>
    /// Luma Uni-1 image generation model.
    /// Model ID: luma-uni-1
    /// </summary>
    [Description("luma-uni-1")]
    LumaUni1,

    /// <summary>
    /// Luma Uni-1 Max image generation model.
    /// Model ID: luma-uni-1-max
    /// </summary>
    [Description("luma-uni-1-max")]
    LumaUni1Max,

    /// <summary>
    /// Nano Banana 2 Lite image generation model.
    /// Model ID: nano-banana-2-lite
    /// </summary>
    [Description("nano-banana-2-lite")]
    NanoBanana2Lite,

    /// <summary>
    /// Qwen Image 3 image generation model.
    /// Model ID: qwen-image-3
    /// </summary>
    [Description("qwen-image-3")]
    QwenImage3,

    /// <summary>
    /// Qwen Image 3 Pro image generation model.
    /// Model ID: qwen-image-3-pro
    /// </summary>
    [Description("qwen-image-3-pro")]
    QwenImage3Pro,

    /// <summary>
    /// Seedream V5 Pro image generation model.
    /// Model ID: seedream-v5-pro
    /// </summary>
    [Description("seedream-v5-pro")]
    SeedreamV5Pro,

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
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5FastImageToVideo (ltx-2-5-fast-image-to-video) instead.")]
    [Description("ltx-2-fast-image-to-video")]
    Ltx2FastImageToVideo,

    /// <summary>
    /// LTX Video 2.0 Fast - Text to Video generation
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5FastTextToVideo (ltx-2-5-fast-text-to-video) instead.")]
    [Description("ltx-2-fast-text-to-video")]
    Ltx2FastTextToVideo,

    /// <summary>
    /// LTX Video 2.0 Full Quality - Image to Video generation
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5ProImageToVideo (ltx-2-5-pro-image-to-video) instead.")]
    [Description("ltx-2-full-image-to-video")]
    Ltx2FullImageToVideo,

    /// <summary>
    /// LTX Video 2.0 Full Quality - Text to Video generation
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5ProTextToVideo (ltx-2-5-pro-text-to-video) instead.")]
    [Description("ltx-2-full-text-to-video")]
    Ltx2FullTextToVideo,

    /// <summary>
    /// LTX Video 2.0 19B - Text to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-full-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5ProTextToVideo (ltx-2-5-pro-text-to-video) instead.")]
    [Description("ltx-2-19b-full-text-to-video")]
    Ltx2_19BFullTextToVideo,

    /// <summary>
    /// LTX Video 2.0 19B - Image to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-full-image-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5ProImageToVideo (ltx-2-5-pro-image-to-video) instead.")]
    [Description("ltx-2-19b-full-image-to-video")]
    Ltx2_19BFullImageToVideo,

    /// <summary>
    /// LTX Video 2.0 19B Distilled - Text to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-distilled-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5FastTextToVideo (ltx-2-5-fast-text-to-video) instead.")]
    [Description("ltx-2-19b-distilled-text-to-video")]
    Ltx2_19BDistilledTextToVideo,

    /// <summary>
    /// LTX Video 2.0 19B Distilled - Image to Video generation with multiple aspect ratios
    /// Model ID: ltx-2-19b-distilled-image-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Ltx2_5FastImageToVideo (ltx-2-5-fast-image-to-video) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance15ProImageToVideoBasic (seedance-1-5-pro-image-to-video-basic) instead.")]
    [Description("seedance-1-5-pro-image-to-video")]
    Seedance15ProImageToVideo,

    /// <summary>
    /// Seedance 1.5 Pro - Text to Video generation.
    /// Model ID: seedance-1-5-pro-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance15ProTextToVideoBasic (seedance-1-5-pro-text-to-video-basic) instead.")]
    [Description("seedance-1-5-pro-text-to-video")]
    Seedance15ProTextToVideo,

    /// <summary>
    /// Seedance 2.0 - Image to Video generation.
    /// Model ID: seedance-2-0-image-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance20ImageToVideoBasic (seedance-2-0-image-to-video-basic) instead.")]
    [Description("seedance-2-0-image-to-video")]
    Seedance20ImageToVideo,

    /// <summary>
    /// Seedance 2.0 - Text to Video generation.
    /// Model ID: seedance-2-0-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance20TextToVideoBasic (seedance-2-0-text-to-video-basic) instead.")]
    [Description("seedance-2-0-text-to-video")]
    Seedance20TextToVideo,

    /// <summary>
    /// Seedance 2.0 - Reference to Video generation.
    /// Model ID: seedance-2-0-reference-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance20ReferenceToVideoBasic (seedance-2-0-reference-to-video-basic) instead.")]
    [Description("seedance-2-0-reference-to-video")]
    Seedance20ReferenceToVideo,

    /// <summary>
    /// Seedance 2.0 Fast - Image to Video generation.
    /// Model ID: seedance-2-0-fast-image-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance20FastImageToVideoBasic (seedance-2-0-fast-image-to-video-basic) instead.")]
    [Description("seedance-2-0-fast-image-to-video")]
    Seedance20FastImageToVideo,

    /// <summary>
    /// Seedance 2.0 Fast - Text to Video generation.
    /// Model ID: seedance-2-0-fast-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance20FastTextToVideoBasic (seedance-2-0-fast-text-to-video-basic) instead.")]
    [Description("seedance-2-0-fast-text-to-video")]
    Seedance20FastTextToVideo,

    /// <summary>
    /// Seedance 2.0 Fast - Reference to Video generation.
    /// Model ID: seedance-2-0-fast-reference-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance20FastReferenceToVideoBasic (seedance-2-0-fast-reference-to-video-basic) instead.")]
    [Description("seedance-2-0-fast-reference-to-video")]
    Seedance20FastReferenceToVideo,

    /// <summary>
    /// Grok Imagine - Image to Video generation.
    /// Model ID: grok-imagine-image-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use GrokImagine15ImageToVideoPrivate (grok-imagine-1-5-image-to-video-private) instead.")]
    [Description("grok-imagine-image-to-video")]
    GrokImagineImageToVideo,

    /// <summary>
    /// Grok Imagine - Text to Video generation.
    /// Model ID: grok-imagine-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use GrokImagine15TextToVideoPrivate (grok-imagine-1-5-text-to-video-private) instead.")]
    [Description("grok-imagine-text-to-video")]
    GrokImagineTextToVideo,

    /// <summary>
    /// Grok Imagine - Reference to Video generation.
    /// Model ID: grok-imagine-reference-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use GrokImagine15ReferenceToVideoPrivate (grok-imagine-1-5-reference-to-video-private) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use Wan27EnhancedImageToVideo (wan-2-7-enhanced-image-to-video) instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use another Kling motion control or video model instead.")]
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
    [Obsolete("This model is no longer available in the Venice AI API. Use RunwayGen45 (runway-gen4-5) instead.")]
    [Description("runway-gen4-aleph")]
    RunwayGen4Aleph,

    /// <summary>
    /// Seedance 2.0 Enhanced - Text to Video generation with enhanced quality.
    /// Model ID: seedance-2-0-enhanced-text-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance25TextToVideoBasic (seedance-2-5-text-to-video-basic) instead.")]
    [Description("seedance-2-0-enhanced-text-to-video")]
    Seedance20EnhancedTextToVideo,

    /// <summary>
    /// Seedance 2.0 Enhanced - Reference to Video generation with enhanced quality.
    /// Model ID: seedance-2-0-enhanced-reference-to-video
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use Seedance25ReferenceToVideoBasic (seedance-2-5-reference-to-video-basic) instead.")]
    [Description("seedance-2-0-enhanced-reference-to-video")]
    Seedance20EnhancedReferenceToVideo,

    /// <summary>
    /// Wan 2.7 Uncensored - Text to Video generation (uncensored).
    /// Model ID: wan-2-7-uncensored-text-to-video
    /// </summary>
    /// <summary>
    /// Flux 3 First Last Frame video generation model.
    /// Model ID: flux-3-first-last-frame-to-video
    /// </summary>
    [Description("flux-3-first-last-frame-to-video")]
    Flux3FirstLastFrameToVideo,

    /// <summary>
    /// Flux 3 video generation model.
    /// Model ID: flux-3-image-to-video
    /// </summary>
    [Description("flux-3-image-to-video")]
    Flux3ImageToVideo,

    /// <summary>
    /// Flux 3 video generation model.
    /// Model ID: flux-3-text-to-video
    /// </summary>
    [Description("flux-3-text-to-video")]
    Flux3TextToVideo,

    /// <summary>
    /// Gemini Omni Flash video generation model.
    /// Model ID: gemini-omni-flash-image-to-video
    /// </summary>
    [Description("gemini-omni-flash-image-to-video")]
    GeminiOmniFlashImageToVideo,

    /// <summary>
    /// Gemini Omni Flash R2V video generation model.
    /// Model ID: gemini-omni-flash-reference-to-video
    /// </summary>
    [Description("gemini-omni-flash-reference-to-video")]
    GeminiOmniFlashReferenceToVideo,

    /// <summary>
    /// Gemini Omni Flash video generation model.
    /// Model ID: gemini-omni-flash-text-to-video
    /// </summary>
    [Description("gemini-omni-flash-text-to-video")]
    GeminiOmniFlashTextToVideo,

    /// <summary>
    /// Grok Imagine 1.5 R2V video generation model.
    /// Model ID: grok-imagine-1-5-reference-to-video-private
    /// </summary>
    [Description("grok-imagine-1-5-reference-to-video-private")]
    GrokImagine15ReferenceToVideoPrivate,

    /// <summary>
    /// Grok Imagine 1.5 video generation model.
    /// Model ID: grok-imagine-1-5-text-to-video-private
    /// </summary>
    [Description("grok-imagine-1-5-text-to-video-private")]
    GrokImagine15TextToVideoPrivate,

    /// <summary>
    /// HappyHorse 1.1 video generation model.
    /// Model ID: happyhorse-1-1-image-to-video
    /// </summary>
    [Description("happyhorse-1-1-image-to-video")]
    HappyHorse11ImageToVideo,

    /// <summary>
    /// HappyHorse 1.1 Reference video generation model.
    /// Model ID: happyhorse-1-1-reference-to-video
    /// </summary>
    [Description("happyhorse-1-1-reference-to-video")]
    HappyHorse11ReferenceToVideo,

    /// <summary>
    /// HappyHorse 1.1 video generation model.
    /// Model ID: happyhorse-1-1-text-to-video
    /// </summary>
    [Description("happyhorse-1-1-text-to-video")]
    HappyHorse11TextToVideo,

    /// <summary>
    /// Kling V3 Turbo Pro video generation model.
    /// Model ID: kling-v3-turbo-pro-image-to-video
    /// </summary>
    [Description("kling-v3-turbo-pro-image-to-video")]
    KlingV3TurboProImageToVideo,

    /// <summary>
    /// Kling V3 Turbo Pro video generation model.
    /// Model ID: kling-v3-turbo-pro-text-to-video
    /// </summary>
    [Description("kling-v3-turbo-pro-text-to-video")]
    KlingV3TurboProTextToVideo,

    /// <summary>
    /// Kling V3 Turbo Standard video generation model.
    /// Model ID: kling-v3-turbo-standard-image-to-video
    /// </summary>
    [Description("kling-v3-turbo-standard-image-to-video")]
    KlingV3TurboStandardImageToVideo,

    /// <summary>
    /// Kling V3 Turbo Standard video generation model.
    /// Model ID: kling-v3-turbo-standard-text-to-video
    /// </summary>
    [Description("kling-v3-turbo-standard-text-to-video")]
    KlingV3TurboStandardTextToVideo,

    /// <summary>
    /// LTX Video 2.5 Fast video generation model.
    /// Model ID: ltx-2-5-fast-image-to-video
    /// </summary>
    [Description("ltx-2-5-fast-image-to-video")]
    Ltx2_5FastImageToVideo,

    /// <summary>
    /// LTX Video 2.5 Fast video generation model.
    /// Model ID: ltx-2-5-fast-text-to-video
    /// </summary>
    [Description("ltx-2-5-fast-text-to-video")]
    Ltx2_5FastTextToVideo,

    /// <summary>
    /// LTX Video 2.5 Pro video generation model.
    /// Model ID: ltx-2-5-pro-image-to-video
    /// </summary>
    [Description("ltx-2-5-pro-image-to-video")]
    Ltx2_5ProImageToVideo,

    /// <summary>
    /// LTX Video 2.5 Pro video generation model.
    /// Model ID: ltx-2-5-pro-text-to-video
    /// </summary>
    [Description("ltx-2-5-pro-text-to-video")]
    Ltx2_5ProTextToVideo,

    /// <summary>
    /// MiniMax H3 R2V Enhanced video generation model.
    /// Model ID: minimax-h3-enhanced-reference-to-video
    /// </summary>
    [Description("minimax-h3-enhanced-reference-to-video")]
    MinimaxH3EnhancedReferenceToVideo,

    /// <summary>
    /// MiniMax H3 Enhanced video generation model.
    /// Model ID: minimax-h3-enhanced-text-to-video
    /// </summary>
    [Description("minimax-h3-enhanced-text-to-video")]
    MinimaxH3EnhancedTextToVideo,

    /// <summary>
    /// MiniMax H3 video generation model.
    /// Model ID: minimax-h3-image-to-video
    /// </summary>
    [Description("minimax-h3-image-to-video")]
    MinimaxH3ImageToVideo,

    /// <summary>
    /// MiniMax H3 R2V video generation model.
    /// Model ID: minimax-h3-reference-to-video
    /// </summary>
    [Description("minimax-h3-reference-to-video")]
    MinimaxH3ReferenceToVideo,

    /// <summary>
    /// MiniMax H3 video generation model.
    /// Model ID: minimax-h3-text-to-video
    /// </summary>
    [Description("minimax-h3-text-to-video")]
    MinimaxH3TextToVideo,

    /// <summary>
    /// Seedance 1.5 Pro video generation model.
    /// Model ID: seedance-1-5-pro-image-to-video-basic
    /// </summary>
    [Description("seedance-1-5-pro-image-to-video-basic")]
    Seedance15ProImageToVideoBasic,

    /// <summary>
    /// Seedance 1.5 Pro video generation model.
    /// Model ID: seedance-1-5-pro-text-to-video-basic
    /// </summary>
    [Description("seedance-1-5-pro-text-to-video-basic")]
    Seedance15ProTextToVideoBasic,

    /// <summary>
    /// Seedance 2.0 Fast video generation model.
    /// Model ID: seedance-2-0-fast-image-to-video-basic
    /// </summary>
    [Description("seedance-2-0-fast-image-to-video-basic")]
    Seedance20FastImageToVideoBasic,

    /// <summary>
    /// Seedance 2.0 Fast R2V video generation model.
    /// Model ID: seedance-2-0-fast-reference-to-video-basic
    /// </summary>
    [Description("seedance-2-0-fast-reference-to-video-basic")]
    Seedance20FastReferenceToVideoBasic,

    /// <summary>
    /// Seedance 2.0 Fast video generation model.
    /// Model ID: seedance-2-0-fast-text-to-video-basic
    /// </summary>
    [Description("seedance-2-0-fast-text-to-video-basic")]
    Seedance20FastTextToVideoBasic,

    /// <summary>
    /// Seedance 2.0 video generation model.
    /// Model ID: seedance-2-0-image-to-video-basic
    /// </summary>
    [Description("seedance-2-0-image-to-video-basic")]
    Seedance20ImageToVideoBasic,

    /// <summary>
    /// Seedance 2.0 Mini video generation model.
    /// Model ID: seedance-2-0-mini-image-to-video-basic
    /// </summary>
    [Description("seedance-2-0-mini-image-to-video-basic")]
    Seedance20MiniImageToVideoBasic,

    /// <summary>
    /// Seedance 2.0 Mini R2V video generation model.
    /// Model ID: seedance-2-0-mini-reference-to-video-basic
    /// </summary>
    [Description("seedance-2-0-mini-reference-to-video-basic")]
    Seedance20MiniReferenceToVideoBasic,

    /// <summary>
    /// Seedance 2.0 Mini video generation model.
    /// Model ID: seedance-2-0-mini-text-to-video-basic
    /// </summary>
    [Description("seedance-2-0-mini-text-to-video-basic")]
    Seedance20MiniTextToVideoBasic,

    /// <summary>
    /// Seedance 2.0 R2V video generation model.
    /// Model ID: seedance-2-0-reference-to-video-basic
    /// </summary>
    [Description("seedance-2-0-reference-to-video-basic")]
    Seedance20ReferenceToVideoBasic,

    /// <summary>
    /// Seedance 2.0 video generation model.
    /// Model ID: seedance-2-0-text-to-video-basic
    /// </summary>
    [Description("seedance-2-0-text-to-video-basic")]
    Seedance20TextToVideoBasic,

    /// <summary>
    /// Seedance 2.5 video generation model.
    /// Model ID: seedance-2-5-image-to-video-basic
    /// </summary>
    [Description("seedance-2-5-image-to-video-basic")]
    Seedance25ImageToVideoBasic,

    /// <summary>
    /// Seedance 2.5 R2V video generation model.
    /// Model ID: seedance-2-5-reference-to-video-basic
    /// </summary>
    [Description("seedance-2-5-reference-to-video-basic")]
    Seedance25ReferenceToVideoBasic,

    /// <summary>
    /// Seedance 2.5 video generation model.
    /// Model ID: seedance-2-5-text-to-video-basic
    /// </summary>
    [Description("seedance-2-5-text-to-video-basic")]
    Seedance25TextToVideoBasic,

    /// <summary>
    /// Wan 2.2 Enhanced video generation model.
    /// Model ID: wan-2-2-enhanced-image-to-video
    /// </summary>
    [Description("wan-2-2-enhanced-image-to-video")]
    Wan22EnhancedImageToVideo,

    /// <summary>
    /// Wan 2.7 Enhanced video generation model.
    /// Model ID: wan-2-7-enhanced-image-to-video
    /// </summary>
    [Description("wan-2-7-enhanced-image-to-video")]
    Wan27EnhancedImageToVideo,

    /// <summary>
    /// Wan 2.7 Enhanced video generation model.
    /// Model ID: wan-2-7-enhanced-text-to-video
    /// </summary>
    [Description("wan-2-7-enhanced-text-to-video")]
    Wan27EnhancedTextToVideo,

    /// <summary>
    /// Wan 3.0 video generation model.
    /// Model ID: wan-3-0-image-to-video
    /// </summary>
    [Description("wan-3-0-image-to-video")]
    Wan30ImageToVideo,

    /// <summary>
    /// Wan 3.0 Prime video generation model.
    /// Model ID: wan-3-0-prime-image-to-video
    /// </summary>
    [Description("wan-3-0-prime-image-to-video")]
    Wan30PrimeImageToVideo,

    /// <summary>
    /// Wan 3.0 Prime Reference video generation model.
    /// Model ID: wan-3-0-prime-reference-to-video
    /// </summary>
    [Description("wan-3-0-prime-reference-to-video")]
    Wan30PrimeReferenceToVideo,

    /// <summary>
    /// Wan 3.0 Prime video generation model.
    /// Model ID: wan-3-0-prime-text-to-video
    /// </summary>
    [Description("wan-3-0-prime-text-to-video")]
    Wan30PrimeTextToVideo,

    /// <summary>
    /// Wan 3.0 Reference video generation model.
    /// Model ID: wan-3-0-reference-to-video
    /// </summary>
    [Description("wan-3-0-reference-to-video")]
    Wan30ReferenceToVideo,

    /// <summary>
    /// Wan 3.0 video generation model.
    /// Model ID: wan-3-0-text-to-video
    /// </summary>
    [Description("wan-3-0-text-to-video")]
    Wan30TextToVideo,
    [Obsolete("This model is no longer available in the Venice AI API. Use Wan27EnhancedTextToVideo (wan-2-7-enhanced-text-to-video) instead.")]
    [Description("wan-2-7-uncensored-text-to-video")]
    Wan27UncensoredTextToVideo
}

/// <summary>
/// Available embedding models.
/// </summary>
public enum EmbeddingModel
{
    /// <summary>
    /// BGE-M3
    /// Model ID: text-embedding-bge-m3
    /// </summary>
    [Description("text-embedding-bge-m3")]
    TextEmbeddingBGEM3,

    /// <summary>
    /// BGE-EN-ICL
    /// Model ID: text-embedding-bge-en-icl
    /// </summary>
    [Description("text-embedding-bge-en-icl")]
    TextEmbeddingBgeEnIcl,

    /// <summary>
    /// Qwen3 Embedding 8B
    /// Model ID: text-embedding-qwen3-8b
    /// </summary>
    [Description("text-embedding-qwen3-8b")]
    TextEmbeddingQwen38b,

    /// <summary>
    /// Qwen3 Embedding 0.6B
    /// Model ID: text-embedding-qwen3-0-6b
    /// </summary>
    [Description("text-embedding-qwen3-0-6b")]
    TextEmbeddingQwen306b,

    /// <summary>
    /// Multilingual E5 Large Instruct
    /// Model ID: text-embedding-multilingual-e5-large-instruct
    /// </summary>
    [Description("text-embedding-multilingual-e5-large-instruct")]
    TextEmbeddingMultilingualE5LargeInstruct,

    /// <summary>
    /// Text Embedding 3 Small
    /// Model ID: text-embedding-3-small
    /// </summary>
    [Description("text-embedding-3-small")]
    TextEmbedding3Small,

    /// <summary>
    /// Text Embedding 3 Large
    /// Model ID: text-embedding-3-large
    /// </summary>
    [Description("text-embedding-3-large")]
    TextEmbedding3Large,

    /// <summary>
    /// Gemini Embedding 2 Preview
    /// Model ID: gemini-embedding-2-preview
    /// </summary>
    [Description("gemini-embedding-2-preview")]
    GeminiEmbedding2Preview,

    /// <summary>
    /// Nemotron Embed VL 1B v2
    /// Model ID: text-embedding-nemotron-embed-vl-1b-v2
    /// </summary>
    [Description("text-embedding-nemotron-embed-vl-1b-v2")]
    TextEmbeddingNemotronEmbedVl1bV2

}


/// <summary>
/// Available text-to-speech models.
/// </summary>
public enum TextToSpeechModel
{
    /// <summary>
    /// Kokoro Text to Speech
    /// Model ID: tts-kokoro
    /// </summary>
    [Description("tts-kokoro")]
    TtsKokoro,

    /// <summary>
    /// Qwen 3 TTS 0.6B
    /// Model ID: tts-qwen3-0-6b
    /// </summary>
    [Description("tts-qwen3-0-6b")]
    TtsQwen306b,

    /// <summary>
    /// Qwen 3 TTS 1.7B
    /// Model ID: tts-qwen3-1-7b
    /// </summary>
    [Description("tts-qwen3-1-7b")]
    TtsQwen317b,

    /// <summary>
    /// xAI TTS v1
    /// Model ID: tts-xai-v1
    /// </summary>
    [Description("tts-xai-v1")]
    TtsXaiV1,

    /// <summary>
    /// Inworld TTS-1.5 Max
    /// Model ID: tts-inworld-1-5-max
    /// </summary>
    [Description("tts-inworld-1-5-max")]
    TtsInworld15Max,

    /// <summary>
    /// Chatterbox HD (Resemble AI)
    /// Model ID: tts-chatterbox-hd
    /// </summary>
    [Description("tts-chatterbox-hd")]
    TtsChatterboxHd,

    /// <summary>
    /// Orpheus TTS
    /// Model ID: tts-orpheus
    /// </summary>
    [Description("tts-orpheus")]
    TtsOrpheus,

    /// <summary>
    /// ElevenLabs Turbo v2.5
    /// Model ID: tts-elevenlabs-turbo-v2-5
    /// </summary>
    [Description("tts-elevenlabs-turbo-v2-5")]
    TtsElevenlabsTurboV25,

    /// <summary>
    /// Clone your voice from a short recording and generate natural speech in it across 30+ languages.
    /// Model ID: tts-minimax-speech-02-hd
    /// </summary>
    [Description("tts-minimax-speech-02-hd")]
    TtsMinimaxSpeech02Hd,

    /// <summary>
    /// Gemini 3.1 Flash TTS
    /// Model ID: tts-gemini-3-1-flash
    /// </summary>
    [Description("tts-gemini-3-1-flash")]
    TtsGemini31Flash,

    /// <summary>
    /// Gradium TTS
    /// Model ID: tts-gradium-v1
    /// </summary>
    [Description("tts-gradium-v1")]
    TtsGradiumV1

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
/// Available image editing (inpaint) models.
/// </summary>
public enum InpaintModel
{
    /// <summary>
    /// FireRed Edit
    /// Model ID: firered-image-edit
    /// </summary>
    [Description("firered-image-edit")]
    FireredImageEdit,

    /// <summary>
    /// Qwen Edit Uncensored
    /// Model ID: qwen-edit-uncensored
    /// </summary>
    [Description("qwen-edit-uncensored")]
    QwenEditUncensored,

    /// <summary>
    /// Grok Imagine
    /// Model ID: grok-imagine-edit
    /// </summary>
    [Description("grok-imagine-edit")]
    GrokImagineEdit,

    /// <summary>
    /// Grok Imagine High Quality
    /// Model ID: grok-imagine-quality-edit
    /// </summary>
    [Description("grok-imagine-quality-edit")]
    GrokImagineQualityEdit,

    /// <summary>
    /// Grok Imagine 2.0
    /// Model ID: grok-imagine-image-2-0-edit
    /// </summary>
    [Description("grok-imagine-image-2-0-edit")]
    GrokImagineImage20Edit,

    /// <summary>
    /// Qwen Image 2
    /// Model ID: qwen-image-2-edit
    /// </summary>
    [Description("qwen-image-2-edit")]
    QwenImage2Edit,

    /// <summary>
    /// Qwen Image 2 Pro
    /// Model ID: qwen-image-2-pro-edit
    /// </summary>
    [Description("qwen-image-2-pro-edit")]
    QwenImage2ProEdit,

    /// <summary>
    /// Wan 2.7 Pro Edit
    /// Model ID: wan-2-7-pro-edit
    /// </summary>
    [Description("wan-2-7-pro-edit")]
    Wan27ProEdit,

    /// <summary>
    /// Flux 2 Max
    /// Model ID: flux-2-max-edit
    /// </summary>
    [Description("flux-2-max-edit")]
    Flux2MaxEdit,

    /// <summary>
    /// GPT Image 2
    /// Model ID: gpt-image-2-edit
    /// </summary>
    [Description("gpt-image-2-edit")]
    GptImage2Edit,

    /// <summary>
    /// GPT Image 1.5
    /// Model ID: gpt-image-1-5-edit
    /// </summary>
    [Description("gpt-image-1-5-edit")]
    GptImage15Edit,

    /// <summary>
    /// Nano Banana 2
    /// Model ID: nano-banana-2-edit
    /// </summary>
    [Description("nano-banana-2-edit")]
    NanoBanana2Edit,

    /// <summary>
    /// Nano Banana Pro
    /// Model ID: nano-banana-pro-edit
    /// </summary>
    [Description("nano-banana-pro-edit")]
    NanoBananaProEdit,

    /// <summary>
    /// Nano Banana 2 Lite
    /// Model ID: nano-banana-2-lite-edit
    /// </summary>
    [Description("nano-banana-2-lite-edit")]
    NanoBanana2LiteEdit,

    /// <summary>
    /// Luma Uni-1
    /// Model ID: luma-uni-1-edit
    /// </summary>
    [Description("luma-uni-1-edit")]
    LumaUni1Edit,

    /// <summary>
    /// Luma Uni-1 Max
    /// Model ID: luma-uni-1-max-edit
    /// </summary>
    [Description("luma-uni-1-max-edit")]
    LumaUni1MaxEdit,

    /// <summary>
    /// Seedream V5 Lite
    /// Model ID: seedream-v5-lite-edit
    /// </summary>
    [Description("seedream-v5-lite-edit")]
    SeedreamV5LiteEdit,

    /// <summary>
    /// Seedream V5 Pro
    /// Model ID: seedream-v5-pro-edit
    /// </summary>
    [Description("seedream-v5-pro-edit")]
    SeedreamV5ProEdit,

    /// <summary>
    /// Seedream V4.5
    /// Model ID: seedream-v4-edit
    /// </summary>
    [Description("seedream-v4-edit")]
    SeedreamV4Edit,

    /// <summary>
    /// Qwen Image 3 Edit
    /// Model ID: qwen-image-3-edit
    /// </summary>
    [Description("qwen-image-3-edit")]
    QwenImage3Edit,

    /// <summary>
    /// Qwen Image 3 Pro Edit
    /// Model ID: qwen-image-3-pro-edit
    /// </summary>
    [Description("qwen-image-3-pro-edit")]
    QwenImage3ProEdit,

    /// <summary>
    /// Legacy generic image editing model.
    /// DEPRECATED: This model is no longer available in the Venice AI API.
    /// Model ID: edit-image
    /// </summary>
    [Obsolete("This model is no longer available in the Venice AI API. Use FireredImageEdit (firered-image-edit) instead.")]
    [Description("edit-image")]
    EditImage

}


/// <summary>
/// Available music and audio generation models.
/// </summary>
public enum MusicModel
{
    /// <summary>
    /// Feature-rich song generation with optional lyrics and detailed musical controls.
    /// Model ID: ace-step-15
    /// </summary>
    [Description("ace-step-15")]
    AceStep15,

    /// <summary>
    /// High-quality instrumental music generation with configurable duration. Best for polished, production-ready tracks across a wide range of genres.
    /// Model ID: elevenlabs-music
    /// </summary>
    [Description("elevenlabs-music")]
    ElevenlabsMusic,

    /// <summary>
    /// Full song generation with vocals and lyrics. Provide your own lyrics with verse/chorus structure for complete songs with singing.
    /// Model ID: minimax-music-v2
    /// </summary>
    [Description("minimax-music-v2")]
    MinimaxMusicV2,

    /// <summary>
    /// Advanced song generation with vocals, lyrics optimizer, and instrumental mode. Supports structure tags and up to 3500 character lyrics.
    /// Model ID: minimax-music-v25
    /// </summary>
    [Description("minimax-music-v25")]
    MinimaxMusicV25,

    /// <summary>
    /// Latest MiniMax song generation with vocals, instrumental mode, and support for rich structure tags in lyrics.
    /// Model ID: minimax-music-v26
    /// </summary>
    [Description("minimax-music-v26")]
    MinimaxMusicV26,

    /// <summary>
    /// Google's Lyria 3 Pro generates full-length, structured songs up to 3 minutes long from a single text prompt. Supports vocals, lyrics, and multi-language generation across genres.
    /// Model ID: lyria-3-pro
    /// </summary>
    [Description("lyria-3-pro")]
    Lyria3Pro,

    /// <summary>
    /// Fast, lightweight audio generation for sound effects, ambient textures, and short musical clips. Flexible duration from 5 seconds to over 3 minutes.
    /// Model ID: stable-audio-25
    /// </summary>
    [Description("stable-audio-25")]
    StableAudio25,

    /// <summary>
    /// Generate licensed, commercial-use-safe music with precise control over style, mood, instrumentation, and duration.
    /// Model ID: sonilo-v1-1-music
    /// </summary>
    [Description("sonilo-v1-1-music")]
    SoniloV11Music,

    /// <summary>
    /// Generate licensed, commercial-use-safe sound effects with precise control over type, texture, intensity, and duration.
    /// Model ID: sonilo-v1-1-sound-effects
    /// </summary>
    [Description("sonilo-v1-1-sound-effects")]
    SoniloV11SoundEffects,

    /// <summary>
    /// Generate high-quality sound effects from text descriptions using ElevenLabs. Ideal for films, games, and digital content with configurable duration.
    /// Model ID: elevenlabs-sound-effects-v2
    /// </summary>
    [Description("elevenlabs-sound-effects-v2")]
    ElevenlabsSoundEffectsV2,

    /// <summary>
    /// Generate synchronized audio and sound effects from text prompts with MMAudio V2.
    /// Model ID: mmaudio-v2-text-to-audio
    /// </summary>
    [Description("mmaudio-v2-text-to-audio")]
    MmaudioV2TextToAudio,

    /// <summary>
    /// Generate natural text-to-speech audio using ElevenLabs Eleven-v3. High-quality voices with stability control and automatic text normalization.
    /// Model ID: elevenlabs-tts-v3
    /// </summary>
    [Description("elevenlabs-tts-v3")]
    ElevenlabsTtsV3,

    /// <summary>
    /// Multilingual text-to-speech using ElevenLabs. Supports 29 languages with high-quality natural-sounding voices, configurable speed, and accent accuracy.
    /// Model ID: elevenlabs-tts-multilingual-v2
    /// </summary>
    [Description("elevenlabs-tts-multilingual-v2")]
    ElevenlabsTtsMultilingualV2,

    /// <summary>
    /// Generate expressive multilingual speech and audio from a text prompt with BytePlus Seed Audio 1.0 (20 languages, timestamp length control).
    /// Model ID: seed-audio-1-0
    /// </summary>
    [Description("seed-audio-1-0")]
    SeedAudio10

}

/// <summary>
/// Available speech-to-text (ASR) models.
/// </summary>
public enum AsrModel
{
    /// <summary>
    /// Parakeet ASR
    /// Model ID: nvidia/parakeet-tdt-0.6b-v3
    /// </summary>
    [Description("nvidia/parakeet-tdt-0.6b-v3")]
    ParakeetTdt06bV3,

    /// <summary>
    /// Whisper Large V3
    /// Model ID: openai/whisper-large-v3
    /// </summary>
    [Description("openai/whisper-large-v3")]
    WhisperLargeV3,

    /// <summary>
    /// Wizper (Whisper v3)
    /// Model ID: fal-ai/wizper
    /// </summary>
    [Description("fal-ai/wizper")]
    Wizper,

    /// <summary>
    /// ElevenLabs Scribe V2
    /// Model ID: elevenlabs/scribe-v2
    /// </summary>
    [Description("elevenlabs/scribe-v2")]
    ScribeV2,

    /// <summary>
    /// xAI Speech to Text v1
    /// Model ID: stt-xai-v1
    /// </summary>
    [Description("stt-xai-v1")]
    SttXaiV1

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
