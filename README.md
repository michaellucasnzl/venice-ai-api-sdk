# Venice AI .NET SDK

![Built in Venice](https://camo.githubusercontent.com/0b92f651701f98ff0df46106545c0fb03a4c5ed77ef2a0348ea3b1691f702c5e/68747470733a2f2f692e6962622e636f2f56635768663232502f626c6f622e706e67)

[![NuGet](https://img.shields.io/nuget/v/VeniceAI.SDK.svg)](https://www.nuget.org/packages/VeniceAI.SDK/)
[![Build and Publish](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Context7](https://img.shields.io/badge/Context7-Docs-blue)](https://context7.com/michaellucasnzl/venice-ai-api-sdk)

A .NET SDK for the [Venice AI API](https://venice.ai), providing typed access to chat completions, image generation, video generation, text-to-speech, embeddings, and more.

> **If you find this SDK useful, please give it a star!** It helps improve visibility and lets other developers discover the project.

## Status

This SDK is community-maintained and **not yet officially affiliated with Venice AI**. It has been developed with the intent to be offered to Venice AI as an official .NET SDK package. Contributions and feedback are welcome.

**Requirements:** .NET 10.0 or later

## Features

- **Chat Completions** — Text generation with streaming, vision, function calling, and reasoning
- **Responses API (Alpha)** — Create responses using the OpenAI-compatible `/api/v1/responses` endpoint
- **Image Generation** — Create, upscale, edit, multi-edit, and background-remove images with multiple models and styles
- **Video Generation** — Queue-based workflow with 100+ models (Wan, LTX, Kling, Veo, Sora, Seedance, Flux) plus YouTube transcription
- **Audio Generation** — Queue-based music/audio generation, text-to-speech with streaming, transcription, and voice cloning
- **Text-to-Speech** — Multiple voices with streaming audio support
- **Embeddings** — Generate text embeddings for semantic search
- **Web Search & Scraping** — Privacy-preserving web search, page scraping, and document text parsing
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

### Audio Generation (Queue-based)

```csharp
var queueRequest = new QueueAudioRequest
{
    Model = MusicModel.ElevenlabsMusic,
    Prompt = "A warm ambient track for a product launch",
    DurationSeconds = 60
};

var queued = await client.Audio.QueueAudioAsync(queueRequest);

// Poll for the result
var result = await client.Audio.RetrieveAudioAsync(new RetrieveAudioRequest
{
    Model = "elevenlabs-music",
    QueueId = queued.QueueId
});

if (result.Status == "completed" && result.AudioUrl != null)
{
    Console.WriteLine($"Audio ready: {result.AudioUrl}");
}
```

### Audio Transcription

```csharp
var request = new CreateTranscriptionRequest
{
    Model = AsrModel.WhisperLargeV3,
    File = await File.ReadAllBytesAsync("meeting.mp3"),
    Filename = "meeting.mp3"
};

var response = await client.Audio.TranscribeAudioAsync(request);
Console.WriteLine(response.Text);
```

### Video Generation (Queue-based)

```csharp
var queueRequest = new QueueVideoRequest
{
    Model = VideoModel.Wan30TextToVideo,
    Prompt = "A cat walking on the beach at sunset",
    Duration = "5s"
};

var queued = await client.Video.QueueVideoAsync(queueRequest);

var result = await client.Video.RetrieveVideoAsync(new RetrieveVideoRequest
{
    Model = "wan-3-0-text-to-video",
    QueueId = queued.QueueId
});
```

### Video Transcription

```csharp
var response = await client.Video.TranscribeVideoAsync(new VideoTranscriptionRequest
{
    Url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
});
Console.WriteLine(response.Text);
```

### Image Editing & Background Removal

```csharp
// Edit an image
var editResponse = await client.Images.EditImageAsync(new EditImageRequest
{
    Image = "https://example.com/image.jpg",
    Prompt = "Change the sky to a sunrise"
});

// Multi-image editing
var multiEditResponse = await client.Images.MultiEditImageAsync(new MultiEditImageRequest
{
    Images = new List<string> { baseImageBase64, layerBase64 },
    Prompt = "Add the object from the second image to the first"
});

// Remove background
var bgResponse = await client.Images.RemoveBackgroundAsync(new BackgroundRemoveImageRequest
{
    ImageUrl = "https://example.com/photo.jpg"
});
```

### Web Search & Scraping

```csharp
var search = await client.Augment.SearchWebAsync(new WebSearchRequest
{
    Query = "latest AI news",
    Limit = 10
});

foreach (var result in search.Results)
{
    Console.WriteLine($"{result.Title} - {result.Url}");
}

var scraped = await client.Augment.ScrapeWebAsync(new WebScrapeRequest
{
    Url = "https://example.com"
});
Console.WriteLine(scraped.Content);

var parsed = await client.Augment.ParseTextAsync(new TextParserRequest
{
    File = await File.ReadAllBytesAsync("document.pdf"),
    Filename = "document.pdf"
});
Console.WriteLine(parsed.Text);
```

### Responses API (Alpha)

```csharp
var request = new ResponsesRequest
{
    Model = TextModel.Glm51,
    Input = "Hello! What can you do?"
};

var response = await client.Responses.CreateResponseAsync(request);

foreach (var item in response.Output.Where(o => o.Type == "message"))
{
    var text = item.Content?.FirstOrDefault(c => c.Type == "output_text")?.Text;
    Console.WriteLine(text);
}
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
| **Text** | `llama-3.3-70b`, `claude-opus-5`, `openai-gpt-56-sol`, `gemini-3-7-flash` | `TextModel` |
| **Image** | `venice-sd35`, `flux-2-pro`, `qwen-image-3`, `nano-banana-pro` | `ImageModel` |
| **Video** | `wan-3-0-text-to-video`, `seedance-2-5-text-to-video-basic`, `kling-v3-turbo-pro-text-to-video` | `VideoModel` |
| **Music/Audio** | `elevenlabs-music`, `minimax-music-v26`, `lyria-3-pro` | `MusicModel` |
| **Speech-to-Text** | `openai/whisper-large-v3`, `nvidia/parakeet-tdt-0.6b-v3` | `AsrModel` |
| **Text-to-Speech** | `tts-kokoro`, `tts-elevenlabs-turbo-v2-5` | `TextToSpeechModel` |
| **Embedding** | `text-embedding-bge-m3`, `text-embedding-3-large` | `EmbeddingModel` |
| **Image Edit** | `firered-image-edit`, `gpt-image-2-edit` | `InpaintModel` |

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

Venice is a privacy-first AI platform providing unrestricted access to the world's leading AI models across text, image, video, code, and audio generation. Learn more at [venice.ai](https://venice.ai).

- [Venice AI](https://venice.ai) — The Venice AI platform
- [Venice AI API Docs](https://docs.venice.ai) — Official API documentation
- [Venice AI Discord](https://discord.gg/askvenice) — Community support

## AI Coding Assistant Docs (Context7)

This SDK is indexed on [Context7](https://context7.com/michaellucasnzl/venice-ai-api-sdk), which provides up-to-date documentation and code examples directly to AI coding assistants such as GitHub Copilot, Cursor, and Claude Code.

To use it, add `use context7` to your prompt or reference the library ID explicitly:

```
How do I stream a chat completion with the Venice AI .NET SDK? use library /michaellucasnzl/venice-ai-api-sdk
```

## Support

For SDK-specific issues, please [open an issue](https://github.com/michaellucasnzl/venice-ai-api-sdk/issues) on GitHub.

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
