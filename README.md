# Venice AI .NET SDK

[![NuGet](https://img.shields.io/nuget/v/VeniceAI.SDK.svg)](https://www.nuget.org/packages/VeniceAI.SDK/)
[![Build and Publish](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml)
[![API Change Detection](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

The unofficial .NET SDK for the Venice AI API, providing easy access to advanced AI models for chat completions, image generation, text-to-speech, embeddings, and more.

**Status:** Beta - under active development with continuous improvements and new features.

## Features

- **Chat Completions** - Text generation with streaming support
- **Image Generation** - Create, edit, and upscale images from text descriptions
- **Text-to-Speech** - Convert text to natural-sounding speech with multiple voices
- **Embeddings** - Generate text embeddings for semantic search and analysis
- **Model Management** - List and manage available models
- **Billing Information** - Track API usage and costs
- **Vision Support** - Analyze and understand images with multimodal models
- **Function Calling** - Execute functions based on natural language requests
- **Streaming Support** - Real-time streaming for chat, audio, and other responses
- **Async/Await** - Full async support throughout the SDK
- **Dependency Injection** - Built-in support for .NET DI container

## Installation

```bash
dotnet add package VeniceAI.SDK
```

## Setup

### API Key Configuration

Set your Venice AI API key using one of these methods:

**User Secrets (recommended for development):**
```bash
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
```

**Environment Variable:**
```bash
# Windows
set VeniceAI__ApiKey=your-api-key-here

# Linux/Mac
export VeniceAI__ApiKey=your-api-key-here
```

**Configuration File:**
```json
{
  "VeniceAI": {
    "ApiKey": "your-api-key-here"
  }
}
```

⚠️ **Important**: Never commit your API key to source control.

## Quick Start

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddVeniceAI(context.Configuration);
    })
    .Build();

var client = host.Services.GetRequiredService<IVeniceAIClient>();

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

**Try the quickstart sample:**
```bash
cd samples/VeniceAI.SDK.Quickstart
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
dotnet run
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
        EnableWebSearch = "on",
        EnableWebCitations = true,
        StripThinkingResponse = false,
        IncludeVeniceSystemPrompt = true,
        DisableThinking = false
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
    }
}
catch (VeniceAIException ex)
{
    Console.WriteLine($"Venice AI Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network error: {ex.Message}");
}
```

## Testing

### Unit Tests
```bash
dotnet test tests/VeniceAI.SDK.Tests
```

### Integration Tests
```bash
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key" --project tests/VeniceAI.SDK.IntegrationTests
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

## Support

- [Documentation](https://docs.venice.ai)
- [GitHub Issues](https://github.com/michaellucasnzl/venice-ai-api-sdk/issues)
- [Discord](https://discord.gg/veniceai)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
