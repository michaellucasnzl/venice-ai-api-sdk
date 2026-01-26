# Venice AI .NET SDK

[![NuGet](https://img.shields.io/nuget/v/VeniceAI.SDK.svg)](https://www.nuget.org/packages/VeniceAI.SDK/)
[![Build and Publish](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/nuget-publish.yml)
[![API Change Detection](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml/badge.svg)](https://github.com/michaellucasnzl/venice-ai-api-sdk/actions/workflows/api-change-detection.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

The unofficial .NET SDK for the Venice AI API, providing easy access to advanced AI models for chat completions, image generation, text-to-speech, embeddings, and more.

**Status:** Beta - under active development with continuous improvements and new features.

**Requirements:** .NET 10.0 or later

## Features

- **Chat Completions** - Text generation with streaming support
- **Image Generation** - Create, edit, and upscale images from text descriptions
- **Text-to-Speech** - Convert text to natural-sounding speech with multiple voices
- **Embeddings** - Generate text embeddings for semantic search and analysis
- **Model Management** - List and manage available models with type filtering and validation
- **Billing Information** - Track API usage and costs
- **Vision Support** - Analyze and understand images with multimodal models
- **Function Calling** - Execute functions based on natural language requests
- **Streaming Support** - Real-time streaming for chat, audio, and other responses
- **Type Safety** - Comprehensive enum system with validation for models and parameters
- **Async/Await** - Full async support throughout the SDK
- **Dependency Injection** - Built-in support for .NET DI container
- **HttpClient Separation** - Complete isolation from your application's HttpClients

## Key Principles

🔐 **SDK Manages Venice AI Specifics**: The SDK automatically handles API endpoints, authentication, and Venice AI-specific configurations. You don't need to configure these manually.

🔗 **Complete HttpClient Separation**: Your application's HttpClients and the Venice AI HttpClient are completely isolated - no configuration conflicts.

⚙️ **Configure What Matters**: Focus on your application needs (timeouts, custom headers) while the SDK handles Venice AI requirements.

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

## Getting Started

### Running the Quickstart Sample

Try the comprehensive quickstart example:

```bash
cd samples/VeniceAI.SDK.Quickstart
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
dotnet run
```

**The quickstart demonstrates:**
- Setting up the Venice AI client with dependency injection
- Listing available models and their capabilities
- Creating basic chat completions
- Streaming chat responses in real-time
- Getting detailed model information
- Proper error handling

## HttpClient Configuration & Separation

The Venice AI SDK provides multiple options for HttpClient configuration to ensure complete separation from your application's other HttpClient instances. **The SDK automatically manages the Venice AI API endpoint (`https://api.venice.ai/api/v1/`) - you cannot and should not configure the base URL.**

### Key Configuration Principles

✅ **Only API Key Required**: The only setting you need to configure is your API key  
✅ **SDK Handles Everything Else**: Endpoints, authentication, and Venice AI-specific settings are managed internally  
❌ **No Base URL Override**: The Venice AI endpoint is fixed and cannot be changed  

### Configuration Options

The SDK accepts only these user-configurable options:

| Option | Description | Default | Required |
|--------|-------------|---------|----------|
| `ApiKey` | Your Venice AI API key | - | ✅ Yes |

All other settings (endpoints, timeouts, retry logic) are managed internally by the SDK.

### ✅ Recommended Usage Patterns

#### 1. Basic Setup (Recommended for Most Cases)
```csharp
services.AddVeniceAI("your-api-key");
```
**Benefits:**
- Automatic HttpClient separation via named client
- SDK manages all Venice AI-specific configuration
- No interference with your other HttpClients
- Only requires your API key

#### 2. Configuration File Setup
```csharp
// appsettings.json
{
  "VeniceAI": {
    "ApiKey": "your-api-key-here"
  }
}

// Startup/Program.cs
services.AddVeniceAI(context.Configuration);
```

#### 3. Multiple Services with Complete Separation
```csharp
// Your application's API service
services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://api.myservice.com/");
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
});

// Venice AI service - completely separate and automatic
services.AddVeniceAI("your-api-key");
// SDK automatically configures: BaseAddress, Authorization, Timeout, etc.
```

#### 4. Custom HttpClient Configuration (Advanced)
```csharp
services.AddVeniceAI("your-api-key", httpClient =>
{
    httpClient.Timeout = TimeSpan.FromMinutes(10); // Custom timeout
    httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
    // SDK automatically sets BaseAddress and Authorization
});
```

**Note:** The SDK automatically handles all Venice AI-specific configuration. You only need to configure additional settings like timeout and custom headers.


## Usage Examples

### Popular Models

The SDK includes strongly-typed enums for all Venice AI models. Here are some popular choices:

**Text Models:**
- `TextModel.OpenAIGpt52` - OpenAI's latest frontier model with adaptive reasoning
- `TextModel.ClaudeSonnet45` - Anthropic's balanced model with strong coding
- `TextModel.Glm47` - Zhiyuan AI's model with 200K+ context window
- `TextModel.Gemini3FlashPreview` - Google's high-speed thinking model
- `TextModel.Qwen3VL235B` - Vision-language model with superior OCR
- `TextModel.GrokCodeFast1` - xAI's fast reasoning model for coding
- `TextModel.VeniceMedium` - Balanced model with vision support

**Image Models:**
- `ImageModel.GptImage15` - OpenAI's image generation (32K prompt limit)
- `ImageModel.Flux2Max` - Premium quality image generation
- `ImageModel.HiDream` - High-quality image generation
- `ImageModel.VeniceSD35` - Venice's optimized Stable Diffusion
- `ImageModel.NanoBananaPro` - Premium with web search support

**Video Models:**
- `VideoModel.Wan26TextToVideo` - Latest Wan with audio support
- `VideoModel.Veo3FullTextToVideo` - Google's Veo 3 full quality
- `VideoModel.Sora2ProTextToVideo` - OpenAI's Sora 2 Pro (1080p)
- `VideoModel.Ltx2_19BFullTextToVideo` - LTX 2.0 19B multi-aspect ratio

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
    Model = "hidream",
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
    model: "hidream",
    width: 1024,
    height: 1024
);

// Image upscaling
var upscaleRequest = new UpscaleImageRequest
{
    Model = "venice-sd35",
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

// List models by type (text, image, tts, embedding, upscale, inpaint)
var textModels = await client.Models.GetModelsAsync(ModelType.Text);
var imageModels = await client.Models.GetModelsAsync(ModelType.Image);
var allModels = await client.Models.GetModelsAsync(ModelType.All);

// Get specific model
var model = await client.Models.GetModelAsync("llama-3.3-70b");
Console.WriteLine($"Context length: {model.ModelSpec.AvailableContextTokens}");

// Get model traits (with optional type filtering)
var traitsResponse = await client.Models.GetModelTraitsAsync();
var textTraits = await client.Models.GetModelTraitsAsync(ModelType.Text);
var defaultModel = traitsResponse.Traits["default"];
var fastestModel = traitsResponse.Traits["fastest"];

// Get model compatibility mappings (with optional type filtering)
var compatibilityResponse = await client.Models.GetModelCompatibilityAsync();
var textCompatibility = await client.Models.GetModelCompatibilityAsync(ModelType.Text);
// Maps alternative model names to Venice AI models (e.g., "gpt-4o" -> "llama-3.3-70b")
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

## Samples & Examples

### 🚀 Quick Start Sample
**Location:** `samples/VeniceAI.SDK.Quickstart/`

A comprehensive console application demonstrating core SDK features:

```bash
cd samples/VeniceAI.SDK.Quickstart
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
dotnet run
```

**Features demonstrated:**
- Setting up Venice AI client with dependency injection
- Listing available models and capabilities
- Basic chat completions with different models
- Real-time streaming chat responses
- Getting detailed model information and pricing
- Proper error handling and logging

### 🔧 HttpClient Separation Examples
**Location:** `samples/VeniceAI.SDK.HttpClientExamples/`

Advanced examples showing proper HttpClient configuration and separation:

```bash
cd samples/VeniceAI.SDK.HttpClientExamples
export VeniceAI__ApiKey="your-api-key-here"  # Linux/Mac
# or: set VeniceAI__ApiKey=your-api-key-here   # Windows
dotnet run
```

**Scenarios covered:**
1. Default HttpClient registration (simplest)
2. Custom HttpClient configuration 
3. Providing your own HttpClient instance
4. Multiple HttpClients with different configurations

**Benefits demonstrated:**
- Complete separation between your HttpClients and Venice AI's
- Flexible configuration for different application needs
- No conflicts or configuration interference
- Proper dependency injection patterns

## Testing

### Unit Tests
```bash
dotnet test tests/VeniceAI.SDK.Tests
```

### Integration Tests
Set your API key and run comprehensive integration tests:

```bash
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key" --project tests/VeniceAI.SDK.IntegrationTests
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

**Note:** Integration tests only require your API key - all other settings are managed by the SDK.

## Support

- [Venice AI Documentation](https://docs.venice.ai)
- [GitHub Issues](https://github.com/michaellucasnzl/venice-ai-api-sdk/issues)
- [Venice AI Discord](https://discord.gg/veniceai)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
