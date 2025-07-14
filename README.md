# Venice AI .NET SDK

[![NuGet](https://img.shields.io/nuget/v/VeniceAI.SDK.svg)](https://www.nuget.org/packages/VeniceAI.SDK/)
[![Build and Publish](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml)
[![API Change Detection](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

The unofficial .NET SDK for the Venice AI API, providing easy access to advanced AI models for chat completions, image
generation, text-to-speech, embeddings, and more.

## Features

- **Chat Completions** - Generate text using advanced language models
- **Image Generation** - Create images from text descriptions
- **Text-to-Speech** - Convert text to natural-sounding speech
- **Embeddings** - Generate text embeddings for semantic search
- **Model Management** - List and manage available models
- **Billing Information** - Track API usage and costs
- **Streaming Support** - Real-time streaming for chat and audio
- **Async/Await** - Full async support throughout the SDK
- **Dependency Injection** - Built-in support for .NET DI container
- **Comprehensive Logging** - Detailed logging for debugging and monitoring

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

### Basic Usage

```csharp
using VeniceAI.SDK;
using VeniceAI.SDK.Models.Chat;

// Initialize the client
var client = new VeniceAIClient("your-api-key");

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

### Dependency Injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK.Extensions;

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
var visionRequest = new ChatCompletionRequest
{
    Model = "llama-3.2-11b-vision",
    Messages = new List<ChatMessage>
    {
        new UserMessage(new List<MessageContent>
        {
            new MessageContent
            {
                Type = "text",
                Text = "What do you see in this image?"
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
```

### Image Generation

```csharp
var imageRequest = new GenerateImageRequest
{
    Model = "hidream",
    Prompt = "A beautiful sunset over mountains",
    Width = 1024,
    Height = 1024,
    Steps = 20,
    CfgScale = 7.5,
    Format = "webp"
};

var imageResponse = await client.Images.GenerateImageAsync(imageRequest);
if (imageResponse.IsSuccess)
{
    var base64Image = imageResponse.Data[0].B64Json;
    // Save or process the image
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
var request = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("Tell me about recent news.")
    },
    VeniceParameters = new VeniceParameters
    {
        EnableWebSearch = "auto",
        EnableWebCitations = true,
        StripThinkingResponse = true,
        IncludeVeniceSystemPrompt = true
    }
};
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
    }
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

The SDK includes comprehensive unit tests and integration tests:

```bash
# Run unit tests
dotnet test tests/VeniceAI.SDK.Tests

# Run integration tests (requires API key)
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

Set the `VENICE_AI_API_KEY` environment variable for integration tests:

```bash
export VENICE_AI_API_KEY="your-api-key"
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

## Sample Applications

Check out the [samples](samples/) directory for complete example applications:

- [Basic Usage](samples/VeniceAI.SDK.Samples/Program.cs) - Demonstrates all SDK features
- [Chat Console App](samples/ChatConsole/) - Interactive chat application
- [Image Generator](samples/ImageGenerator/) - Batch image generation
- [Voice Assistant](samples/VoiceAssistant/) - Speech-to-text and text-to-speech

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
A .NET SDK for the Venice AI API

## Versioning

The Venice AI SDK follows semantic versioning with an auto-incrementing build number:

- **Format**: `{Major}.{Minor}.{Patch}.{Build}` (e.g., `1.2.0.12345`)
- **Major**: Breaking changes
- **Minor**: New features (backwards compatible)
- **Patch**: Bug fixes (backwards compatible)
- **Build**: Auto-incremented from GitHub workflow run number
