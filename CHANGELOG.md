# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
