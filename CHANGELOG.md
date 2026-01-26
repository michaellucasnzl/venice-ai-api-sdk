# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.4.0] - 2026-01-26

### Changed
- **Upgraded to .NET 10**: Updated target framework from .NET 8.0 to .NET 10.0 (LTS)
  - All projects now target `net10.0`
  - Updated Microsoft.Extensions.* packages to version 10.0.1
  - Updated System.Text.Json to version 10.0.1
  - Updated Microsoft.SourceLink.GitHub to version 10.0.102
  - Removed System.ComponentModel.Annotations as it's now included in .NET 10 runtime

### Notes
- System.Text.Json warning (NU1510) is expected in .NET 10 due to new package pruning feature
- All unit tests pass successfully on .NET 10.0.1

## [1.3.0] - 2026-01-26

### Added
- **New Text Models**: Added latest Venice AI text models
  - `Glm47` (zai-org-glm-4.7) - Zhiyuan AI's model with strong reasoning and largest context window
  - `Gemini3FlashPreview` (gemini-3-flash-preview) - Google's high-speed thinking model
  - `ClaudeSonnet45` (claude-sonnet-45) - Anthropic's balanced model with strong coding
  - `OpenAIGpt52` (openai-gpt-52) - OpenAI's latest frontier model with adaptive reasoning
  - `OpenAIGpt52Codex` (openai-gpt-52-codex) - OpenAI specialized coding model
  - `MinimaxM21` (minimax-m21) - Lightweight model optimized for coding
  - `GrokCodeFast1` (grok-code-fast-1) - xAI's speedy reasoning model for coding
  - `Qwen3VL235B` (qwen3-vl-235b-a22b) - Vision-language model with superior visual perception
- **New Image Models**: Added latest Venice AI image models
  - `Flux2Pro` (flux-2-pro) - High-quality image generation
  - `Flux2Max` (flux-2-max) - Premium quality image generation
  - `GptImage15` (gpt-image-1-5) - OpenAI's image generation with 32K prompt limit
  - `SeedreamV4` (seedream-v4) - Advanced image generation model
  - `BgRemover` (bg-remover) - Tool for removing backgrounds from images
- **New Video Models**: Added latest Venice AI video models
  - `Wan26ImageToVideo` (wan-2.6-image-to-video) - Wan 2.6 with audio support
  - `Wan26FlashImageToVideo` (wan-2.6-flash-image-to-video) - Fast Wan 2.6 with audio
  - `Wan26TextToVideo` (wan-2.6-text-to-video) - Wan 2.6 text-to-video with audio
  - `Ltx2_19BFullTextToVideo` (ltx-2-19b-full-text-to-video) - LTX 2.0 19B full quality
  - `Ltx2_19BFullImageToVideo` (ltx-2-19b-full-image-to-video) - LTX 2.0 19B image-to-video
  - `Ltx2_19BDistilledTextToVideo` (ltx-2-19b-distilled-text-to-video) - LTX 2.0 19B distilled
  - `Ltx2_19BDistilledImageToVideo` (ltx-2-19b-distilled-image-to-video) - LTX 2.0 19B distilled

### Changed
- Updated to Venice AI API version as of 2026-01-26

### Deprecated
- `Glm46` (zai-org-glm-4.6) - Use `Glm47` (zai-org-glm-4.7) instead

### Recommended Migrations
- For GLM models: Migrate from `Glm46` to `Glm47` for improved performance
- For frontier reasoning: Use `OpenAIGpt52` for general tasks or `OpenAIGpt52Codex` for coding
- For fast thinking: Use `Gemini3FlashPreview` for high-speed reasoning
- For vision tasks: Use `Qwen3VL235B` for superior visual perception and OCR
- For video generation: Use Wan 2.6 models for latest audio-enabled generation

## [1.2.0] - 2025-07-18

### Added
- **Video Generation API**: Full support for Venice AI's video generation capabilities
  - New `IVideoService` interface and `VideoService` implementation
  - Queue-based video generation workflow with `QueueVideoAsync`, `RetrieveVideoAsync`, `CompleteVideoAsync`
  - Video generation quotes with `QuoteVideoAsync`
  - New `VideoModel` enum with 30+ video models (Wan, LTX, Kling, Veo, Sora families)
- **Characters API**: Access to Venice AI character definitions
  - New `ICharacterService` interface and `CharacterService` implementation
  - List all characters and get character by slug
- **New Text Models**: Added latest Venice AI text models
  - `Qwen3_235B_Thinking` (qwen3-235b-a22b-thinking-2507)
  - `Qwen3_235B_Instruct` (qwen3-235b-a22b-instruct-2507)
  - `Qwen3_Next_80B` (qwen3-next-80b)
  - `Qwen3Coder_480B` (qwen3-coder-480b-a35b-instruct)
  - `Grok41Fast` (grok-41-fast)
  - `Gemini3ProPreview` (gemini-3-pro-preview)
  - `ClaudeOpus45` (claude-opus-45)
  - `OpenAIGptOss120B` (openai-gpt-oss-120b)
  - `KimiK2Thinking` (kimi-k2-thinking)
  - `Glm46` (zai-org-glm-4.6)
  - `DeepSeekV32` (deepseek-v3.2)
- **New Image Models**: Added latest Venice AI image models
  - `NanoBananaPro` (nano-banana-pro)
  - `LustifyV7` (lustify-v7)
  - `ZImageTurbo` (z-image-turbo)
- **Reasoning Support**: Added reasoning configuration for chat completions
  - New `ReasoningConfig` class with `Effort` property
  - `Reasoning` and `ReasoningEffort` properties on `ChatCompletionRequest`
  - Support for "low", "medium", "high" reasoning effort levels
- **Enhanced Image Generation**: New parameters for image requests
  - `Variants` (1-4) for generating multiple images
  - `AspectRatio` for controlling output dimensions
  - `Resolution` for explicit resolution control
  - `EnableWebSearch` for web-assisted generation
- **Enhanced VeniceParameters**: New web scraping options
  - `EnableWebScraping` for web content extraction
  - `ReturnSearchResultsAsDocuments` for document-style search results
- **Prompt Caching**: New `PromptCacheKey` property for caching prompts
- **ModelType Enum**: Added new model types: `asr`, `video`, `embedding`, `upscale`, `inpaint`

### Changed
- Updated to Venice AI API version 20251212.081138
- Updated `IVeniceAIClient` interface with `Video` and `Characters` service properties
- Updated dependency injection registration for new services

### Deprecated
- Text models: `QwenReasonning`, `VeniceMedium32`, `Llama31_405B`, `Dolphin72B`, `Qwen25VL`, `Qwen25Coder32B`, `DeepSeekCoderV2Lite`
- Image models: `FluxStandard`, `FluxCustom`, `PonyRealism`, `StableDiffusion35`
- All deprecated models are marked with `[Obsolete]` attributes and will be removed in a future version

### Recommended Migrations
- For video generation: Use the new `IVideoService` with appropriate `VideoModel` enum values
- For reasoning tasks: Use `Qwen3_235B_Thinking` or `KimiK2Thinking` with `ReasoningConfig`
- For vision tasks: Use `VeniceMedium` (mistral-31-24b) instead of `Qwen25VL`
- For image generation: Use `HiDream`, `VeniceSD35`, `QwenImage`, or `NanoBananaPro`
- For coding tasks: Use `Qwen3Coder_480B` for best code generation results

## [1.0.0] - 2025-01-11

### Added
- Initial release of the Venice AI .NET SDK
- Support for Chat Completions with streaming
- Support for Image Generation with multiple models
- Support for Text-to-Speech with multiple voices
- Support for Embeddings generation
- Support for Model information retrieval
- Support for Billing usage tracking
- Comprehensive dependency injection support
- Built-in error handling and retry logic
- Extensive logging capabilities
- Vision model support for image understanding
- Function calling support for tool usage
- Venice-specific parameters for enhanced features
- Complete test suite with unit and integration tests
- Sample applications demonstrating all features
- Full async/await support throughout the SDK
- Strong typing for all API models and responses
- Comprehensive documentation and examples

### Features
- **Chat Service**: Create chat completions with streaming support
- **Image Service**: Generate, upscale, and edit images
- **Audio Service**: Text-to-speech conversion with streaming
- **Embedding Service**: Generate text embeddings for semantic search
- **Model Service**: List and retrieve model information
- **Billing Service**: Track API usage and costs
- **Configuration**: Flexible configuration options
- **Dependency Injection**: Built-in DI container support
- **Error Handling**: Comprehensive error handling and logging
- **Streaming**: Real-time streaming for chat and audio responses
- **Vision**: Support for image understanding models
- **Function Calling**: Tool usage with structured responses
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
