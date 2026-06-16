# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.3.0] - 2026-06-16

### Added

#### New Text Models
- `ClaudeFable5` (`claude-fable-5`) — Anthropic Claude Fable 5 with 1M token context and always-on adaptive thinking
- `KimiK27Code` (`kimi-k2-7-code`) — Moonshot AI Kimi K2.7 Code, coding-focused agentic model with 1T parameters
- `MinimaxM3Preview` (`minimax-m3-preview`) — MiniMax M3 preview, 1.4T-parameter frontier model
- `NvidiaNemotron3Ultra550B` (`nvidia-nemotron-3-ultra-550b-a55b`) — NVIDIA Nemotron 3 Ultra for frontier reasoning
- `TencentHy3Preview` (`tencent-hy3-preview`) — Tencent Hy Team 295B MoE preview
- `XiaomiMimoV25` (`xiaomi-mimo-v2-5`) — Xiaomi MiMo V2.5 omnimodal model

#### New Video Models
- `Seedance20EnhancedTextToVideo` (`seedance-2-0-enhanced-text-to-video`) — Seedance 2.0 Enhanced text-to-video
- `Seedance20EnhancedReferenceToVideo` (`seedance-2-0-enhanced-reference-to-video`) — Seedance 2.0 Enhanced reference-to-video
- `Wan27UncensoredTextToVideo` (`wan-2-7-uncensored-text-to-video`) — Wan 2.7 Uncensored text-to-video

## [2.2.0] - 2026-07-19

### Added

#### New Text Models
- `VeniceUncensored12` (`venice-uncensored-1-2`) — Updated Venice uncensored model for unrestricted content generation
- `Qwen3_7Max` (`qwen-3-7-max`) — Alibaba Qwen 3.7 Max, premium flagship model optimized for maximum quality reasoning
- `Qwen3_7Plus` (`qwen-3-7-plus`) — Alibaba Qwen 3.7 Plus with strong reasoning and coding capabilities
- `Qwen36_27B` (`qwen3-6-27b`) — Alibaba Qwen3.6 27B dense model
- `Gemma4Uncensored` (`gemma-4-uncensored`) — Uncensored variant of Google's Gemma 4 model
- `Grok4_3` (`grok-4-3`) — xAI Grok 4.3 fast reasoning model
- `Grok4_20` (`grok-4-20`) — xAI Grok 4.20 multimodal reasoning model with 2M-token context window
- `Grok4_20MultiAgent` (`grok-4-20-multi-agent`) — xAI Grok 4.20 Multi-Agent for collaborative workflows
- `GrokBuild0_1` (`grok-build-0-1`) — xAI Grok Build 0.1 experimental model
- `Gemini35Flash` (`gemini-3-5-flash`) — Google Gemini 3.5 Flash high-speed model
- `ClaudeOpus4_7` (`claude-opus-4-7`) — Anthropic Claude Opus 4.7 advanced reasoning model
- `ClaudeOpus4_7Fast` (`claude-opus-4-7-fast`) — Speed-optimized Claude Opus 4.7
- `ClaudeOpus4_8` (`claude-opus-4-8`) — Anthropic Claude Opus 4.8 latest frontier reasoning model
- `ClaudeOpus4_8Fast` (`claude-opus-4-8-fast`) — Speed-optimized Claude Opus 4.8
- `KimiK26` (`kimi-k2-6`) — Moonshot AI Kimi K2.6 advanced open reasoning model
- `DeepSeekV4Pro` (`deepseek-v4-pro`) — DeepSeek V4 Pro premium flagship model
- `DeepSeekV4Flash` (`deepseek-v4-flash`) — DeepSeek V4 Flash fast inference model
- `OpenAIGpt55` (`openai-gpt-55`) — OpenAI GPT-5.5 next-generation frontier model
- `OpenAIGpt55Pro` (`openai-gpt-55-pro`) — OpenAI GPT-5.5 Pro premium next-generation model
- `MinimaxM3` (`minimax-m3`) — MiniMax M3 latest model
- `E2EEGemma4_26B_A4B_Uncensored` (`e2ee-gemma-4-26b-a4b-uncensored-p`) — Uncensored Gemma 4 26B in E2EE TEE
- `E2EEQwen36_35B_A3B_Uncensored` (`e2ee-qwen3-6-35b-a3b-uncensored-p`) — Uncensored Qwen3.6 in E2EE TEE
- `E2EEGlm51` (`e2ee-glm-5-1`) — GLM 5.1 in E2EE TEE
- `E2EEQwen36_35B_A3B` (`e2ee-qwen3-6-35b-a3b`) — Qwen3.6 35B A3B in E2EE TEE
- `E2EEGemma4_31B` (`e2ee-gemma-4-31b`) — Google Gemma 4 31B in E2EE TEE

#### New Image Models
- `GrokImagineImageQuality` (`grok-imagine-image-quality`) — xAI high-quality image generation model
- `GptImage2` (`gpt-image-2`) — OpenAI GPT Image 2 latest image generation model
- `IdeogramV4` (`ideogram-v4`) — Ideogram V4 advanced image generation
- `KreaV2Large` (`krea-v2-large`) — Krea V2 Large image generation model
- `KreaV2Medium` (`krea-v2-medium`) — Krea V2 Medium image generation model

#### New Video Models
- `Wan27VideoToVideo` (`wan-2-7-video-to-video`) — Wan 2.7 video-to-video generation
- `Wan27UncensoredImageToVideo` (`wan-2-7-uncensored-image-to-video`) — Wan 2.7 uncensored image-to-video
- `HappyHorse10TextToVideo` (`happyhorse-1-0-text-to-video`) — HappyHorse 1.0 text-to-video
- `HappyHorse10ImageToVideo` (`happyhorse-1-0-image-to-video`) — HappyHorse 1.0 image-to-video
- `HappyHorse10ReferenceToVideo` (`happyhorse-1-0-reference-to-video`) — HappyHorse 1.0 reference-to-video
- `HappyHorse10VideoToVideo` (`happyhorse-1-0-video-to-video`) — HappyHorse 1.0 video-to-video
- `GrokImagineTextToVideoPrivate` (`grok-imagine-text-to-video-private`) — Grok Imagine private text-to-video
- `GrokImagineImageToVideoPrivate` (`grok-imagine-image-to-video-private`) — Grok Imagine private image-to-video
- `GrokImagineReferenceToVideoPrivate` (`grok-imagine-reference-to-video-private`) — Grok Imagine private reference-to-video
- `GrokImagineVideoToVideoPrivate` (`grok-imagine-video-to-video-private`) — Grok Imagine private video-to-video
- `GrokImagine15ImageToVideoPrivate` (`grok-imagine-1-5-image-to-video-private`) — Grok Imagine 1.5 private image-to-video
- `KlingV3_4KTextToVideo` (`kling-v3-4k-text-to-video`) — Kling V3 4K text-to-video
- `KlingV3_4KReferenceToVideo` (`kling-v3-4k-reference-to-video`) — Kling V3 4K reference-to-video
- `KlingO3_4KTextToVideo` (`kling-o3-4k-text-to-video`) — Kling O3 4K text-to-video
- `KlingO3_4KImageToVideo` (`kling-o3-4k-image-to-video`) — Kling O3 4K image-to-video
- `KlingO3_4KReferenceToVideo` (`kling-o3-4k-reference-to-video`) — Kling O3 4K reference-to-video
- `KlingO3StandardMotionControl` (`kling-o3-standard-motion-control`) — Kling O3 Standard motion control
- `KlingV3ProMotionControl` (`kling-v3-pro-motion-control`) — Kling V3 Pro motion control
- `KlingV3StandardMotionControl` (`kling-v3-standard-motion-control`) — Kling V3 Standard motion control
- `PixVerseC1TextToVideo` (`pixverse-c1-text-to-video`) — PixVerse C1 text-to-video
- `PixVerseC1ImageToVideo` (`pixverse-c1-image-to-video`) — PixVerse C1 image-to-video
- `PixVerseC1ReferenceToVideo` (`pixverse-c1-reference-to-video`) — PixVerse C1 reference-to-video
- `PixVerseC1Transition` (`pixverse-c1-transition`) — PixVerse C1 transition effects
- `RunwayGen45` (`runway-gen4-5`) — Runway Gen4.5 image-to-video
- `RunwayGen45Text` (`runway-gen4-5-text`) — Runway Gen4.5 Text text-to-video
- `RunwayGen4Turbo` (`runway-gen4-turbo`) — Runway Gen4 Turbo fast video generation
- `RunwayGen4Aleph` (`runway-gen4-aleph`) — Runway Gen4 Aleph advanced video generation

#### New Request Parameters
- `VeniceParameters.EnableE2ee` — Enable E2EE for E2EE-capable models (when combined with E2EE headers)
- `VeniceParameters.EnableXSearch` — Enable xAI native web + X/Twitter search for supported models (e.g. `grok-4-20`)
- `ChatCompletionRequest.PromptCacheRetention` — Control prompt cache retention duration (`"extended"`, `"24h"`)
- `ChatCompletionRequest.Verbosity` — Control text response verbosity (`"low"`, `"medium"`, `"high"`, `"auto"`)
- `ChatCompletionRequest.Metadata` — Request tracking metadata dictionary
- `QueueVideoRequest.EndImageUrl` — End frame image for transition models
- `QueueVideoRequest.AudioUrl` — Background music input (WAV/MP3, max 30s, 15MB)
- `QueueVideoRequest.VideoUrl` — Video input for video-to-video and upscale models
- `QueueVideoRequest.UpscaleFactor` — Upscale factor: 1 (quality), 2 (2× res), 4 (4× res)
- `QueueVideoRequest.ReferenceImageUrls` — Up to 9 reference images for character/style consistency
- `QueueVideoRequest.ReferenceVideoUrls` — Up to 3 reference video URLs
- `QueueVideoRequest.ReferenceAudioUrls` — Up to 3 reference audio URLs
- `QueueVideoRequest.SceneImageUrls` — Up to 4 scene reference images (referenced as @Image1…@Image4 in prompt)
- `EditImageRequest.Model` — Explicit model selection for image editing
- `EditImageRequest.AspectRatio` — Output aspect ratio (e.g. `"16:9"`)
- `EditImageRequest.Resolution` — Output resolution (e.g. `"1024x1024"`)
- `EditImageRequest.OutputFormat` — Output format (`"png"`, `"jpeg"`, `"webp"`)
- `EditImageRequest.SafeMode` — Safe mode filtering for edited images

#### New Classes
- `QuoteVideoRequest` — Dedicated request type for `QuoteVideoAsync` (separate from `QueueVideoRequest`)

### Changed
- `IVideoService.QuoteVideoAsync` now accepts `QuoteVideoRequest` instead of `QueueVideoRequest`
- `VideoService.QuoteVideoAsync` now accepts `QuoteVideoRequest` instead of `QueueVideoRequest`

### Fixed
- `GoogleGemma4_26B_A4B`: corrected model ID from `google.gemma-4-26b-a4b-it` → `google-gemma-4-26b-a4b-it`
- `GoogleGemma4_31B`: corrected model ID from `google.gemma-4-31b-it` → `google-gemma-4-31b-it`
- `AionLabs2_0`: corrected model ID from `aion-labs.aion-2-0` → `aion-labs-aion-2-0`

### Deprecated
- `TextModel.Grok41Fast` — Use `Grok4_3` instead
- `TextModel.Grok4_20Beta` — Use `Grok4_20` instead
- `TextModel.Grok4_20MultiAgentBeta` — Use `Grok4_20MultiAgent` instead
- `TextModel.MinimaxM21` — Use `MinimaxM3` instead
- `TextModel.E2EEQwen35_122B_A10B` — No longer available in the API
- `TextModel.E2EEGlm5` — Use `E2EEGlm51` instead
- `ImageModel.HiDream` — No longer available in the API; use `VeniceSD35` or another model
- `ImageModel.GrokImagineImagePro` — No longer available in the API; use `GrokImagineImageQuality` instead



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
