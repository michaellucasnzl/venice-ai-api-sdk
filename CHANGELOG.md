# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.1.0] - 2026-04-10

### Added

#### New Text Models
- `ZAIGlm51` (`zai-org-glm-5-1`) — GLM 5.1 from Zhiyuan AI with enhanced reasoning and 200K context
- `ZAIGlm5Turbo` (`z-ai-glm-5-turbo`) — GLM 5 Turbo, fast inference model tuned for agent-driven coding
- `ZAIGlm5VTurbo` (`z-ai-glm-5v-turbo`) — GLM 5V Turbo, multimodal agent model supporting vision
- `GoogleGemma4_26B_A4B` (`google.gemma-4-26b-a4b-it`) — Google Gemma 4 26B MoE with vision and video input
- `GoogleGemma4_31B` (`google.gemma-4-31b-it`) — Google Gemma 4 31B dense model with frontier-level reasoning
- `ArceeTrinityLargeThinking` (`arcee-trinity-large-thinking`) — 398B sparse MoE reasoning model
- `Grok4_20Beta` (`grok-4-20-beta`) — xAI Grok 4.20 Beta with 2M-token context window
- `Grok4_20MultiAgentBeta` (`grok-4-20-multi-agent-beta`) — Grok 4.20 variant for collaborative multi-agent workflows
- `ClaudeOpus4_5` (`claude-opus-4-5`) — Anthropic Claude Opus 4.5 (corrected model ID)
- `ClaudeOpus46Fast` (`claude-opus-4-6-fast`) — Speed-optimized Claude Opus 4.6 with lower latency
- `ClaudeSonnet4_5` (`claude-sonnet-4-5`) — Anthropic Claude Sonnet 4.5 (corrected model ID)
- `Mercury2` (`mercury-2`) — Inception diffusion-based LLM delivering 1,000+ tokens/second
- `MistralSmall3_2_24B` (`mistral-small-3-2-24b-instruct`) — Mistral Small 3.2 24B
- `MistralSmall2603` (`mistral-small-2603`) — Mistral Small 4, 119B MoE with reasoning and vision
- `NvidiaNemotron3Nano30B` (`nvidia-nemotron-3-nano-30b-a3b`) — NVIDIA Nemotron 3 Nano 30B
- `NvidiaNemotronCascade2_30B` (`nvidia-nemotron-cascade-2-30b-a3b`) — NVIDIA Nemotron Cascade 2 30B
- `OpenAIGpt53Codex` (`openai-gpt-53-codex`) — GPT-5.3 Codex specialized coding model
- `OpenAIGpt54` (`openai-gpt-54`) — GPT-5.4 with 1M+ context window and adaptive reasoning
- `OpenAIGpt54Mini` (`openai-gpt-54-mini`) — GPT-5.4 Mini, efficient high-throughput variant
- `OpenAIGpt54Pro` (`openai-gpt-54-pro`) — GPT-5.4 Pro with enhanced reasoning
- `OpenAIGpt4o_Nov2024` (`openai-gpt-4o-2024-11-20`) — GPT-4o (2024-11-20)
- `OpenAIGpt4oMini_Jul2024` (`openai-gpt-4o-mini-2024-07-18`) — GPT-4o Mini (2024-07-18)
- `MinimaxM27` (`minimax-m27`) — MiniMax M2.7 with advanced multi-agent collaboration
- `Qwen35_9B` (`qwen3-5-9b`) — Qwen 3.5 9B dense model with 256K context
- `Qwen35_35B_A3B` (`qwen3-5-35b-a3b`) — Qwen 3.5 35B MoE, 3B active parameters
- `Qwen35_397B_A17B` (`qwen3-5-397b-a17b`) — Qwen 3.5 397B flagship MoE model
- `Qwen3_6Plus` (`qwen-3-6-plus`) — Qwen 3.6 Plus Uncensored, 1M context with multimodal support
- `Qwen3Coder480BTurbo` (`qwen3-coder-480b-a35b-instruct-turbo`) — Qwen 3 Coder 480B Turbo
- `VeniceUncensoredRolePlay` (`venice-uncensored-role-play`) — Venice Role Play Uncensored model
- `AionLabs2_0` (`aion-labs.aion-2-0`) — Aion 2.0, fine-tuned for immersive roleplaying
- E2EE TEE models (Trusted Execution Environment variants): `E2EEVeniceUncensored24B`, `E2EEGemma3_27B`, `E2EEGlm47`, `E2EEGlm47Flash`, `E2EEGlm5`, `E2EEGptOss20B`, `E2EEGptOss120B`, `E2EEQwen25_7B`, `E2EEQwen3_30B_A3B`, `E2EEQwen3VL_30B_A3B`, `E2EEQwen35_122B_A10B`

#### New Image Models
- `BriaBgRemover` (`bria-bg-remover`) — Bria Background Remover (replaces `bg-remover`)
- `HunyuanImageV3` (`hunyuan-image-v3`) — Hunyuan Image V3 from Tencent
- `NanoBanana2` (`nano-banana-2`) — Nano Banana 2 image generation
- `LustifyV8` (`lustify-v8`) — Updated Lustify model
- `SeedreamV5Lite` (`seedream-v5-lite`) — SeedreamV5 Lite image generation
- `QwenImage2` (`qwen-image-2`) — Qwen Image 2 from Alibaba
- `QwenImage2Pro` (`qwen-image-2-pro`) — Qwen Image 2 Pro premium variant
- `GrokImagineImage` (`grok-imagine-image`) — xAI Grok Imagine image generation
- `GrokImagineImagePro` (`grok-imagine-image-pro`) — xAI Grok Imagine Pro
- `Wan27TextToImage` (`wan-2-7-text-to-image`) — Wan 2.7 text-to-image
- `Wan27ProTextToImage` (`wan-2-7-pro-text-to-image`) — Wan 2.7 Pro text-to-image

#### New Video Models
- `KlingV3ProTextToVideo`, `KlingV3ProImageToVideo` — Kling V3 Pro
- `KlingV3StandardTextToVideo`, `KlingV3StandardImageToVideo` — Kling V3 Standard
- `KlingO3StandardTextToVideo`, `KlingO3StandardImageToVideo`, `KlingO3StandardReferenceToVideo` — Kling O3 Standard
- `KlingO3ProReferenceToVideo` — Kling O3 Pro reference-to-video
- `Ltx2V23FastImageToVideo`, `Ltx2V23FastTextToVideo`, `Ltx2V23FullImageToVideo`, `Ltx2V23FullTextToVideo` — LTX Video 2.0 V2.3
- `Seedance15ProImageToVideo`, `Seedance15ProTextToVideo` — Seedance 1.5 Pro
- `Seedance20ImageToVideo`, `Seedance20TextToVideo`, `Seedance20ReferenceToVideo` — Seedance 2.0
- `Seedance20FastImageToVideo`, `Seedance20FastTextToVideo`, `Seedance20FastReferenceToVideo` — Seedance 2.0 Fast
- `GrokImagineImageToVideo`, `GrokImagineTextToVideo`, `GrokImagineReferenceToVideo` — Grok Imagine video
- `TopazVideoUpscale` (`topaz-video-upscale`) — Topaz Video Upscale tool
- `Wan27ImageToVideo`, `Wan27TextToVideo`, `Wan27ReferenceToVideo` — Wan 2.7 video generation

### Deprecated
- `VeniceUncensored` (`venice-uncensored`) — Being removed by Venice AI on 2026-04-15. Use `VeniceUncensoredRolePlay` instead.
- `KimiK2Thinking` (`kimi-k2-thinking`) — Being removed by Venice AI on 2026-05-06. Use `KimiK25` instead.
- `ClaudeOpus45` (`claude-opus-45`) — Incorrect model ID. Use `ClaudeOpus4_5` (`claude-opus-4-5`) instead.
- `ClaudeSonnet45` (`claude-sonnet-45`) — Incorrect model ID. Use `ClaudeSonnet4_5` (`claude-sonnet-4-5`) instead.
- `VeniceSmall` (`qwen3-4b`) — Model no longer available. Use `Llama32_3B` or similar instead.
- `VeniceMedium` (`mistral-31-24b`) — Model no longer available. Use `MistralSmall3_2_24B` or `GoogleGemma3_27B` instead.
- `Gemini3ProPreview` (`gemini-3-pro-preview`) — Model no longer available. Use `Gemini31ProPreview` instead.
- `GrokCodeFast1` (`grok-code-fast-1`) — Model no longer available. Use `Grok41Fast` instead.
- `BgRemover` (`bg-remover`) — Replaced by `BriaBgRemover` (`bria-bg-remover`).

---

## [2.0.0] - 2026-02-26

### Initial Public Release

The Venice AI .NET SDK — a comprehensive .NET client library for the [Venice AI API](https://docs.venice.ai).

#### Features
- **Chat Completions** — Text generation with streaming support, vision (image understanding), function calling, and reasoning configuration
- **Image Generation** — Create, upscale, and edit images with multiple models and style options
- **Video Generation** — Queue-based video generation workflow with 30+ models (Wan, LTX, Kling, Veo, Sora families)
- **Text-to-Speech** — Convert text to natural-sounding speech with multiple voices and streaming audio
- **Embeddings** — Generate text embeddings for semantic search and analysis
- **Model Management** — List models with type filtering, get traits, and compatibility mappings
- **Billing** — Track API usage and costs
- **Characters** — Access Venice AI character definitions
- **Venice Parameters** — Web search, web citations, web scraping, thinking control, and prompt caching

#### SDK Capabilities
- Full async/await support throughout
- Real-time streaming for chat, audio, and other responses
- Strongly-typed enums for all models (text, image, video, TTS, embedding)
- Built-in dependency injection support with complete HttpClient isolation
- Comprehensive error handling with `VeniceAIException`
- .NET 10.0 target framework
- NuGet package with source link and symbol packages
- **Venice Parameters**: Access to Venice-specific features

### Technical Details
- .NET 8.0 target framework
- Full async/await support
- Comprehensive error handling
- Built-in retry logic with exponential backoff
- Structured logging with Microsoft.Extensions.Logging
- Dependency injection with Microsoft.Extensions.DependencyInjection
- JSON serialization with System.Text.Json
- HTTP client factory integration
- Configuration binding with IOptions pattern
- Comprehensive test coverage with xUnit, FluentAssertions, and Moq

### Models and Endpoints
- `/chat/completions` - Chat completions with streaming
- `/image/generate` - Image generation
- `/images/generations` - OpenAI-compatible image generation
- `/image/upscale` - Image upscaling and enhancement
- `/image/edit` - Image editing
- `/image/styles` - Available image styles
- `/embeddings` - Text embeddings
- `/audio/speech` - Text-to-speech conversion
- `/models` - Model listing and information
- `/models/traits` - Model traits mapping
- `/models/compatibility_mapping` - Model compatibility
- `/billing/usage` - Billing usage tracking

### Supported Models
- **Text Models**: llama-3.3-70b, llama-3.2-11b-vision, and more
- **Image Models**: hidream, stable-diffusion-3.5, flux-dev, and more
- **Embedding Models**: text-embedding-bge-m3
- **Audio Models**: tts-kokoro with 50+ voices
- **Vision Models**: Support for image understanding

### Documentation
- Comprehensive README with usage examples
- API reference documentation
- Sample applications
- Integration test examples
- Configuration guide
- Error handling best practices
