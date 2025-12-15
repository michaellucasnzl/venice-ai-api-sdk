# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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
