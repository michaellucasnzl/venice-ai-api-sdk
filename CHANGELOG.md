# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
