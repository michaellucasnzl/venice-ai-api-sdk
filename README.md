# Venice AI .NET SDK

[![NuGet](https://img.shields.io/nuget/v/VeniceAI.SDK.svg)](https://www.nuget.org/packages/VeniceAI.SDK/)
[![Build and Publish](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A .NET SDK for the [Venice AI API](https://venice.ai), providing typed access to chat completions, image generation, video generation, text-to-speech, embeddings, and more.

> **If you find this SDK useful, please give it a star!** It helps improve visibility and lets other developers discover the project.

## Status

This SDK is community-maintained and **not yet officially affiliated with Venice AI**. It has been developed with the intent to be offered to Venice AI as an official .NET SDK package. Contributions and feedback are welcome.

**Requirements:** .NET 10.0 or later

## Features

- **Chat Completions** — Text generation with streaming, vision, function calling, and reasoning
- **Image Generation** — Create, upscale, and edit images with multiple models and styles
- **Video Generation** — Queue-based workflow with 30+ models (Wan, LTX, Kling, Veo, Sora)
- **Text-to-Speech** — Multiple voices with streaming audio support
- **Embeddings** — Generate text embeddings for semantic search
- **Model Management** — List, filter, and inspect available models
- **Billing** — Track API usage and costs
- **Characters** — Access Venice AI character definitions
- **Venice Parameters** — Web search, citations, scraping, thinking control
- **Dependency Injection** — Built-in DI with complete HttpClient isolation
- **Full Async/Await** — Async throughout the entire SDK

## Installation

```bash
dotnet add package VeniceAI.SDK
```

## API Key Setup

Get your API key from [Venice AI](https://venice.ai) and configure it using one of these methods:

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

**Configuration File (appsettings.json):**
```json
{
  "VeniceAI": {
    "ApiKey": "your-api-key-here"
  }
}
```

> **Never commit your API key to source control.**

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

You can also register the client with just an API key string:

```csharp
services.AddVeniceAI("your-api-key");
```

## Usage Examples

### Chat Completions

```csharp
var request = new ChatCompletionRequest
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

var response = await client.Chat.CreateChatCompletionAsync(request);
Console.WriteLine(response.Choices[0].Message.Content);
```

### Streaming

```csharp
await foreach (var chunk in client.Chat.CreateChatCompletionStreamAsync(request))
{
    if (chunk.IsSuccess && chunk.Choices?.Any() == true)
    {
        Console.Write(chunk.Choices[0].Message.Content);
    }
}
```

### Vision (Image Understanding)

```csharp
var request = new ChatCompletionRequest
{
    Model = "mistral-31-24b",
    Messages = new List<ChatMessage>
    {
        new UserMessage(new List<MessageContent>
        {
            new MessageContent { Type = "text", Text = "Describe this image." },
            new MessageContent
            {
                Type = "image_url",
                ImageUrl = new ImageUrl { Url = "https://example.com/image.jpg" }
            }
        })
    },
    MaxTokens = 200
};

var response = await client.Chat.CreateChatCompletionAsync(request);
```

### Image Generation

```csharp
var request = new GenerateImageRequest
{
    Model = "hidream",
    Prompt = "A beautiful sunset over mountains",
    Width = 1024,
    Height = 1024,
    Format = "png"
};

var response = await client.Images.GenerateImageAsync(request);
var imageBytes = Convert.FromBase64String(response.Data[0].B64Json);
await File.WriteAllBytesAsync("output.png", imageBytes);
```

### Text-to-Speech

```csharp
var request = new CreateSpeechRequest
{
    Model = "tts-kokoro",
    Input = "Hello, this is Venice AI speaking!",
    Voice = VoiceOptions.Female.Sky,
    ResponseFormat = AudioFormat.Mp3
};

var response = await client.Audio.CreateSpeechAsync(request);
await File.WriteAllBytesAsync("output.mp3", response.AudioContent);
```

### Embeddings

```csharp
var request = new CreateEmbeddingRequest
{
    Model = "text-embedding-bge-m3",
    Input = "The quick brown fox jumps over the lazy dog",
    EncodingFormat = "float"
};

var response = await client.Embeddings.CreateEmbeddingAsync(request);
Console.WriteLine($"Dimensions: {response.Data[0].Embedding.Count}");
```

### Function Calling

```csharp
var request = new ChatCompletionRequest
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

var response = await client.Chat.CreateChatCompletionAsync(request);
```

### Venice Parameters (Web Search, Thinking)

```csharp
var request = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("What are the latest developments in AI?")
    },
    VeniceParameters = new VeniceParameters
    {
        EnableWebSearch = "on",
        EnableWebCitations = true,
        IncludeVeniceSystemPrompt = true
    }
};

var response = await client.Chat.CreateChatCompletionAsync(request);

if (response.VeniceParameters?.WebSearchCitations?.Any() == true)
{
    foreach (var citation in response.VeniceParameters.WebSearchCitations)
    {
        Console.WriteLine($"Source: {citation.Title} - {citation.Url}");
    }
}
```

### Model Information

```csharp
// List all models
var models = await client.Models.GetModelsAsync();

// Filter by type
var textModels = await client.Models.GetModelsAsync(ModelType.Text);
var imageModels = await client.Models.GetModelsAsync(ModelType.Image);

// Get a specific model
var model = await client.Models.GetModelAsync("llama-3.3-70b");
Console.WriteLine($"Context: {model.ModelSpec.AvailableContextTokens}");
```

### Billing

```csharp
var request = new BillingUsageRequest
{
    StartDate = DateTime.UtcNow.AddDays(-30),
    EndDate = DateTime.UtcNow,
    Currency = Currency.USD
};

var response = await client.Billing.GetBillingUsageAsync(request);
foreach (var entry in response.Data)
{
    Console.WriteLine($"{entry.Timestamp}: {entry.Sku} - ${entry.Amount}");
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
        Console.WriteLine($"Error: {response.Error?.Error} (Status: {response.StatusCode})");
    }
}
catch (VeniceAIException ex)
{
    Console.WriteLine($"Venice AI Error: {ex.Message} (Status: {ex.StatusCode})");
}
```

## Available Models

The SDK provides strongly-typed enums for all Venice AI models:

| Category | Examples | Enum |
|----------|----------|------|
| **Text** | `llama-3.3-70b`, `claude-sonnet-4-6`, `openai-gpt-52`, `gemini-3-flash-preview` | `TextModel` |
| **Image** | `hidream`, `flux-2-max`, `gpt-image-1-5`, `venice-sd35` | `ImageModel` |
| **Video** | `wan-2.6-text-to-video`, `veo3-full-text-to-video`, `sora-2-pro-text-to-video` | `VideoModel` |
| **Audio** | `tts-kokoro` | — |
| **Embedding** | `text-embedding-bge-m3` | — |

Use `client.Models.GetModelsAsync()` for the full, up-to-date list of available models.

## Running the Quickstart Sample

```bash
cd samples/VeniceAI.SDK.Quickstart
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
dotnet run
```

## Testing

### Unit Tests

```bash
dotnet test tests/VeniceAI.SDK.Tests
```

### Integration Tests

Integration tests require a valid API key:

```bash
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key" --project tests/VeniceAI.SDK.IntegrationTests
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

## Contributing

This repository does not accept direct pushes. To contribute:

1. Fork the repository
2. Create a feature branch
3. Submit a Pull Request

All PRs will be reviewed and merged by the maintainer. See [CONTRIBUTING.md](.github/CONTRIBUTING.md) for details.

## Venice AI

- [Venice AI](https://venice.ai) — The Venice AI platform
- [Venice AI API Docs](https://docs.venice.ai) — Official API documentation
- [Venice AI Discord](https://discord.gg/veniceai) — Community support

## Support

For SDK-specific issues, please [open an issue](https://github.com/michaellucasnzl/venice-ai-api-sdk/issues) on GitHub.

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
