# Venice AI .NET SDK

[![NuGet](https://img.shields.io/nuget/v/VeniceAI.SDK.svg)](https://www.nuget.org/packages/VeniceAI.SDK/)
[![Build and Publish](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml)
[![API Change Detection](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

The as-yet unofficial .NET SDK for the Venice AI API, providing easy access to advanced AI models for chat completions, image
generation, text-to-speech, embeddings, and more.

Note that this SDK is under review by the Venice team and may become the official Venice SDK for .NET.

The SDK is currently in *beta* with active development ensuring continuous improvements and new features.

## Features

- **Chat Completions** - Generate text using advanced language models with streaming support
- **Image Generation** - Create, edit, and upscale images from text descriptions
- **Text-to-Speech** - Convert text to natural-sounding speech with multiple voices
- **Embeddings** - Generate text embeddings for semantic search and analysis
- **Model Management** - List and manage available models with detailed specifications
- **Billing Information** - Track API usage and costs with detailed breakdowns
- **Vision Support** - Analyze and understand images with multimodal models
- **Function Calling** - Execute functions based on natural language requests
- **Web Search Integration** - Enhanced responses with real-time web search
- **Venice Parameters** - Advanced Venice-specific features and optimizations
- **Streaming Support** - Real-time streaming for chat, audio, and other responses
- **Async/Await** - Full async support throughout the SDK
- **Dependency Injection** - Built-in support for .NET DI container
- **Comprehensive Logging** - Detailed logging for debugging and monitoring
- **Error Handling** - Robust error handling with detailed error information
- **Configuration Management** - Flexible configuration options for all scenarios

## Installation

Install the Venice AI SDK via NuGet:

```bash
dotnet add package VeniceAI.SDK
```

Or via Package Manager Console:

```powershell
Install-Package VeniceAI.SDK
```

## Setup

### Setting Your API Key

The Venice AI SDK requires an API key to authenticate with the Venice AI API. You can set your API key in several ways:

#### Option 1: User Secrets (Recommended for Development)

```bash
# Set user secrets for your project
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
```

#### Option 2: Environment Variable

```bash
# Windows Command Prompt
set VeniceAI__ApiKey=your-api-key-here

# Windows PowerShell
$env:VeniceAI__ApiKey="your-api-key-here"

# Linux/Mac
export VeniceAI__ApiKey=your-api-key-here
```

#### Option 3: Configuration File

Add to your `appsettings.json`:

```json
{
  "VeniceAI": {
    "ApiKey": "your-api-key-here"
  }
}
```

⚠️ **Important**: Never commit your API key to source control. Use user secrets, environment variables, or secure
configuration providers in production.

## Quick Start

### Running the Quickstart Application

The easiest way to get started is to run the quickstart application:

```bash
cd samples/VeniceAI.SDK.Quickstart
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
dotnet run
```

This will demonstrate:
- Listing available models
- Creating chat completions
- Streaming responses
- Getting model information

See the [quickstart README](samples/VeniceAI.SDK.Quickstart/README.md) for detailed instructions.

### Basic Usage with Dependency Injection

The Venice AI SDK is designed to work with .NET's dependency injection container:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add Venice AI SDK with configuration
        services.AddVeniceAI(context.Configuration);
        
        // Or add with API key directly
        services.AddVeniceAI("your-api-key");
        
        // Or add with configuration action
        services.AddVeniceAI(options =>
        {
            options.ApiKey = "your-api-key";
            options.BaseUrl = "https://api.venice.ai/api/v1";
            options.TimeoutSeconds = 300;
        });
    })
    .Build();

var client = host.Services.GetRequiredService<IVeniceAIClient>();

// Create a chat completion
var request = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("Hello! How are you?")
    },
    MaxTokens = 100
};

var response = await client.Chat.CreateChatCompletionAsync(request);
Console.WriteLine(response.Choices[0].Message.Content);
```
var request = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("Hello! How are you?")
    },
    MaxTokens = 100
};

var response = await client.Chat.CreateChatCompletionAsync(request);
Console.WriteLine(response.Choices[0].Message.Content);
```

### Configuration

Add to your `appsettings.json`:

```json
{
  "VeniceAI": {
    "ApiKey": "your-api-key",
    "BaseUrl": "https://api.venice.ai/api/v1",
    "TimeoutSeconds": 300,
    "MaxRetryAttempts": 3,
    "RetryDelayMs": 1000,
    "EnableLogging": true
  }
}
```

## Usage Examples

### Chat Completions

```csharp
// Basic chat
var chatRequest = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new SystemMessage("You are a helpful assistant."),
        new UserMessage("What is the capital of France?")
    },
    MaxTokens = 150,
    Temperature = 0.7
};

var response = await client.Chat.CreateChatCompletionAsync(chatRequest);
Console.WriteLine(response.Choices[0].Message.Content);

// Streaming chat
await foreach (var chunk in client.Chat.CreateChatCompletionStreamAsync(chatRequest))
{
    if (chunk.IsSuccess && chunk.Choices?.Any() == true)
    {
        Console.Write(chunk.Choices[0].Message.Content);
    }
}
```

### Vision (Image Understanding)

```csharp
// Analyze an image with vision models
var visionRequest = new ChatCompletionRequest
{
    Model = "mistral-31-24b", // Vision-enabled model
    Messages = new List<ChatMessage>
    {
        new UserMessage(new List<MessageContent>
        {
            new MessageContent
            {
                Type = "text",
                Text = "What do you see in this image? Describe it in detail."
            },
            new MessageContent
            {
                Type = "image_url",
                ImageUrl = new ImageUrl
                {
                    Url = "https://example.com/image.jpg"
                }
            }
        })
    },
    MaxTokens = 200
};

var response = await client.Chat.CreateChatCompletionAsync(visionRequest);
Console.WriteLine($"Vision analysis: {response.Choices[0].Message.Content}");
```

### Image Generation

```csharp
// Basic image generation
var imageRequest = new GenerateImageRequest
{
    Model = "flux-dev",
    Prompt = "A beautiful sunset over mountains",
    Width = 1024,
    Height = 1024,
    Steps = 25,
    CfgScale = 7.5,
    Format = "png"
};

var imageResponse = await client.Images.GenerateImageAsync(imageRequest);
if (imageResponse.IsSuccess)
{
    var base64Image = imageResponse.Data[0].B64Json;
    // Save or process the image
    var imageBytes = Convert.FromBase64String(base64Image);
    await File.WriteAllBytesAsync("generated_image.png", imageBytes);
}

// Simple image generation
var simpleImageResponse = await client.Images.GenerateImageSimpleAsync(
    "A futuristic cityscape at night", 
    model: "flux-dev",
    width: 1024,
    height: 1024
);

// Image upscaling
var upscaleRequest = new UpscaleImageRequest
{
    Model = "flux-dev",
    Image = Convert.ToBase64String(imageBytes),
    Scale = 2
};

var upscaleResponse = await client.Images.UpscaleImageAsync(upscaleRequest);

// Get available image styles
var stylesResponse = await client.Images.GetImageStylesAsync();
foreach (var style in stylesResponse.Data)
{
    Console.WriteLine($"Available style: {style}");
}
```

### Text-to-Speech

```csharp
var ttsRequest = new CreateSpeechRequest
{
    Model = "tts-kokoro",
    Input = "Hello, this is Venice AI speaking!",
    Voice = VoiceOptions.Female.Sky,
    ResponseFormat = AudioFormat.Mp3,
    Speed = 1.0
};

var audioResponse = await client.Audio.CreateSpeechAsync(ttsRequest);
if (audioResponse.IsSuccess)
{
    await File.WriteAllBytesAsync("output.mp3", audioResponse.AudioContent);
}

// Streaming TTS
await foreach (var chunk in client.Audio.CreateSpeechStreamAsync(ttsRequest))
{
    // Process audio chunk
}
```

### Embeddings

```csharp
var embeddingRequest = new CreateEmbeddingRequest
{
    Model = "text-embedding-bge-m3",
    Input = "The quick brown fox jumps over the lazy dog",
    EncodingFormat = "float"
};

var embeddingResponse = await client.Embeddings.CreateEmbeddingAsync(embeddingRequest);
if (embeddingResponse.IsSuccess)
{
    var embedding = embeddingResponse.Data[0].Embedding;
    Console.WriteLine($"Embedding dimensions: {embedding.Count}");
}
```

### Function Calling

```csharp
var functionRequest = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("What's the weather like in New York?")
    },
    Tools = new List<Tool>
    {
        new Tool
        {
            Function = new FunctionDefinition
            {
                Name = "get_weather",
                Description = "Get current weather for a location",
                Parameters = new Dictionary<string, object>
                {
                    ["type"] = "object",
                    ["properties"] = new Dictionary<string, object>
                    {
                        ["location"] = new Dictionary<string, object>
                        {
                            ["type"] = "string",
                            ["description"] = "The city and state"
                        }
                    },
                    ["required"] = new[] { "location" }
                }
            }
        }
    },
    ToolChoice = "auto"
};

var response = await client.Chat.CreateChatCompletionAsync(functionRequest);
```

### Model Information

```csharp
// List all models
var modelsResponse = await client.Models.GetModelsAsync();
foreach (var model in modelsResponse.Data)
{
    Console.WriteLine($"{model.Id}: {model.ModelSpec.Name} ({model.Type})");
}

// Get specific model
var model = await client.Models.GetModelAsync("llama-3.3-70b");
Console.WriteLine($"Context length: {model.ModelSpec.AvailableContextTokens}");

// Get model traits
var traitsResponse = await client.Models.GetModelTraitsAsync();
var defaultModel = traitsResponse.Traits["default"];
```

### Billing Information

```csharp
var billingRequest = new BillingUsageRequest
{
    StartDate = DateTime.UtcNow.AddDays(-30),
    EndDate = DateTime.UtcNow,
    Currency = Currency.USD,
    Limit = 100,
    Page = 1
};

var billingResponse = await client.Billing.GetBillingUsageAsync(billingRequest);
foreach (var entry in billingResponse.Data)
{
    Console.WriteLine($"{entry.Timestamp}: {entry.Sku} - ${entry.Amount}");
}
```

## Advanced Features

### Venice Parameters

```csharp
// Use Venice-specific features for enhanced responses
var request = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("Tell me about recent developments in AI technology.")
    },
    VeniceParameters = new VeniceParameters
    {
        EnableWebSearch = "on", // Enable web search for current information
        EnableWebCitations = true, // Include citations in response
        StripThinkingResponse = false, // Show reasoning process
        IncludeVeniceSystemPrompt = true, // Use Venice optimizations
        DisableThinking = false // Allow model to show reasoning
    }
};

var response = await client.Chat.CreateChatCompletionAsync(request);
Console.WriteLine($"Enhanced response: {response.Choices[0].Message.Content}");

// Check for web search citations
if (response.VeniceParameters?.WebSearchCitations?.Any() == true)
{
    Console.WriteLine("Sources:");
    foreach (var citation in response.VeniceParameters.WebSearchCitations)
    {
        Console.WriteLine($"- {citation.Title}: {citation.Url}");
    }
}
```

### Error Handling

```csharp
try
{
    var response = await client.Chat.CreateChatCompletionAsync(request);
    
    if (response.IsSuccess)
    {
        Console.WriteLine(response.Choices[0].Message.Content);
    }
    else
    {
        Console.WriteLine($"Error: {response.Error?.Error}");
        Console.WriteLine($"Status Code: {response.StatusCode}");
        Console.WriteLine($"Error Type: {response.Error?.Type}");
        Console.WriteLine($"Error Code: {response.Error?.Code}");
    }
}
catch (VeniceAIException ex)
{
    Console.WriteLine($"Venice AI Error: {ex.Message}");
    Console.WriteLine($"Error Code: {ex.ErrorCode}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network error: {ex.Message}");
}
catch (TaskCanceledException ex)
{
    Console.WriteLine($"Request timeout: {ex.Message}");
}
```

### Custom HTTP Client

```csharp
services.AddHttpClient<IVeniceAIClient, VeniceAIClient>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(10);
    client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
});
```

## Configuration Options

| Option             | Description                     | Default                        |
|--------------------|---------------------------------|--------------------------------|
| `ApiKey`           | Your Venice AI API key          | Required                       |
| `BaseUrl`          | API base URL                    | `https://api.venice.ai/api/v1` |
| `TimeoutSeconds`   | Request timeout in seconds      | `300`                          |
| `MaxRetryAttempts` | Maximum retry attempts          | `3`                            |
| `RetryDelayMs`     | Delay between retries           | `1000`                         |
| `EnableLogging`    | Enable request/response logging | `false`                        |
| `CustomHeaders`    | Additional HTTP headers         | `{}`                           |

## Testing

The SDK includes comprehensive testing to ensure reliability:

### Unit Tests

```bash
# Run unit tests
dotnet test tests/VeniceAI.SDK.Tests

# Run with coverage
dotnet test tests/VeniceAI.SDK.Tests --collect:"XPlat Code Coverage"
```

### Integration Tests

Integration tests verify actual API functionality:

```bash
# Set API key for integration tests
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key" --project tests/VeniceAI.SDK.IntegrationTests

# Run integration tests
dotnet test tests/VeniceAI.SDK.IntegrationTests

# Run specific test category
dotnet test tests/VeniceAI.SDK.IntegrationTests --filter "Category=Chat"
```

Integration tests cover:
- ✅ **Chat completions** - Standard and streaming
- ✅ **Image generation** - All formats and styles
- ✅ **Audio synthesis** - TTS with various voices
- ✅ **Embeddings** - Text embedding generation
- ✅ **Model management** - Listing and capabilities
- ✅ **Billing** - Usage tracking and reporting

### Test Categories

Tests are organized by functionality:

```csharp
[TestCategory("Chat")]
[TestCategory("Images")]
[TestCategory("Audio")]
[TestCategory("Embeddings")]
[TestCategory("Models")]
[TestCategory("Billing")]
```

### Running All Tests

```bash
# Run all tests (unit + integration)
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run only integration tests
dotnet test --filter "Category!=Unit"
```

### Test Configuration

Integration tests use the same configuration as the SDK:

```json
{
  "VeniceAI": {
    "ApiKey": "your-api-key",
    "BaseUrl": "https://api.venice.ai/api/v1",
    "TimeoutSeconds": 300
  }
}
```

## Sample Applications

Check out the [samples](samples/) directory for complete example applications:

- [Quickstart](samples/VeniceAI.SDK.Quickstart/Program.cs) - Complete demonstration of all SDK features including chat, models, and streaming
- [Quickstart README](samples/VeniceAI.SDK.Quickstart/README.md) - Detailed setup and usage instructions

The quickstart application demonstrates:
- 📋 Model listing and capabilities
- 💬 Chat completions with token usage
- 📡 Real-time streaming responses  
- 🔍 Model details and pricing information
- ⚙️ Configuration and dependency injection setup

## API Reference

For detailed API documentation, visit [https://docs.venice.ai](https://docs.venice.ai).

## Contributing

Contributions are welcome! Please read our [Contributing Guide](CONTRIBUTING.md) for details.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- **Documentation**: [https://docs.venice.ai](https://docs.venice.ai)
- **Issues**: [GitHub Issues](https://github.com/veniceai/venice-ai-api-sdk/issues)
- **Discord**: [Venice AI Community](https://discord.gg/veniceai)
- **Email**: support@venice.ai

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for release notes and version history.

# A .NET SDK for the Venice AI API

## Versioning

The Venice AI SDK follows semantic versioning with an auto-incrementing build number:

- **Format**: `{Major}.{Minor}.{Patch}.{Build}` (e.g., `1.2.0.12345`)
- **Major**: Breaking changes
- **Minor**: New features (backwards compatible)
- **Patch**: Bug fixes (backwards compatible)
- **Build**: Auto-incremented from GitHub workflow run number

## Available Models

The Venice AI SDK supports various models for different use cases:

### Text Models
- **llama-3.3-70b** - High-performance general-purpose model (default)
- **qwen3-235b** - Large model for complex reasoning tasks
- **mistral-31-24b** - Vision-enabled model for image understanding
- **venice-uncensored** - Uncensored model for creative tasks
- **qwen-2.5-qwq-32b** - Reasoning-focused model
- **deepseek-r1-671b** - Advanced reasoning model

### Image Models
- **flux-dev** - High-quality image generation (recommended)
- **venice-sd35** - Stable Diffusion 3.5 model
- **hidream** - Artistic image generation
- **stable-diffusion-3.5** - Latest Stable Diffusion model
- **pony-realism** - Realistic image generation

### Audio Models
- **tts-kokoro** - Text-to-speech synthesis
- **whisper** - Speech recognition and transcription

### Embedding Models
- **text-embedding-bge-m3** - Multilingual embeddings
- **text-embedding-ada-002** - General-purpose embeddings

```csharp
// Get all available models
var modelsResponse = await client.Models.GetModelsAsync();
foreach (var model in modelsResponse.Data)
{
    Console.WriteLine($"{model.Id}: {model.ModelSpec.Name}");
    Console.WriteLine($"Type: {model.Type}");
    Console.WriteLine($"Context Length: {model.ModelSpec.AvailableContextTokens}");
    Console.WriteLine($"Supports Vision: {model.ModelSpec.Capabilities.SupportsVision}");
    Console.WriteLine($"Supports Function Calling: {model.ModelSpec.Capabilities.SupportsFunctionCalling}");
    Console.WriteLine();
}
```

## Project Structure

The Venice AI SDK is organized as follows:

```
venice-ai-api-sdk/
├── src/
│   └── VeniceAI.SDK/                    # Main SDK library
│       ├── Configuration/               # Configuration classes
│       ├── Extensions/                  # DI extensions
│       ├── Models/                      # Request/response models
│       │   ├── Audio/                   # Audio/TTS models
│       │   ├── Billing/                 # Billing models
│       │   ├── Chat/                    # Chat completion models
│       │   ├── Common/                  # Shared models
│       │   ├── Embeddings/              # Embedding models
│       │   ├── Images/                  # Image generation models
│       │   └── Models/                  # Model management models
│       └── Services/                    # Service implementations
│           ├── Base/                    # Base service classes
│           └── Interfaces/              # Service interfaces
├── samples/
│   └── VeniceAI.SDK.Quickstart/        # Quickstart console application
├── tests/
│   ├── VeniceAI.SDK.Tests/              # Unit tests
│   └── VeniceAI.SDK.IntegrationTests/   # Integration tests
└── openapi/                             # OpenAPI specifications
```

### Key Components

- **VeniceAI.SDK** - Main SDK package providing all functionality
- **Services** - Core service implementations (Chat, Images, Audio, etc.)
- **Models** - Strongly-typed request/response models
- **Configuration** - Configuration options and validation
- **Extensions** - Dependency injection extensions
