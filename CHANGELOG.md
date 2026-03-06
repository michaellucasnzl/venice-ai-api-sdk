# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.1.0] - 2026-04-01

### Added
- New text models: GPT-5.3 Codex (`openai-gpt-53-codex`), GPT-5.4 (`openai-gpt-54`), GPT-4o (`openai-gpt-4o-2024-11-20`), GPT-4o Mini (`openai-gpt-4o-mini-2024-07-18`), Qwen 3.5 35B A3B (`qwen3-5-35b-a3b`), Qwen 3 Coder 480B Turbo (`qwen3-coder-480b-a35b-instruct-turbo`), NVIDIA Nemotron 3 Nano 30B (`nvidia-nemotron-3-nano-30b-a3b`)
- New image models: Grok Imagine (`grok-imagine`), Hunyuan Image V3 (`hunyuan-image-v3`), Nano Banana 2 (`nano-banana-2`), Seedream V5 Lite (`seedream-v5-lite`), Qwen Image 2 (`qwen-image-2`), Qwen Image 2 Pro (`qwen-image-2-pro`), BRIA Background Remover (`bria-bg-remover`)
- New video models: Grok Imagine Text-to-Video (`grok-imagine-text-to-video`), Grok Imagine Image-to-Video (`grok-imagine-image-to-video`), LTX Video 2.3 Fast Image-to-Video (`ltx-2-v2-3-fast-image-to-video`), LTX Video 2.3 Fast Text-to-Video (`ltx-2-v2-3-fast-text-to-video`), LTX Video 2.3 Full Image-to-Video (`ltx-2-v2-3-full-image-to-video`), LTX Video 2.3 Full Text-to-Video (`ltx-2-v2-3-full-text-to-video`)
- Un-deprecated `Glm46` (`zai-org-glm-4.6`) — model has returned to the Venice AI API

### Fixed
- Corrected model ID for `ClaudeOpus45`: was `claude-opus-45`, now correctly `claude-opus-4-5`
- Corrected model ID for `ClaudeSonnet45`: was `claude-sonnet-45`, now correctly `claude-sonnet-4-5`

### Deprecated
- `BgRemover` (`bg-remover`) marked as obsolete; use `BriaBgRemover` (`bria-bg-remover`) instead

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
