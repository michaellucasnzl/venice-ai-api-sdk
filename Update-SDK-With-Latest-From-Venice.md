# Update SDK With Latest From Venice

This document guides an AI coding agent through the process of updating the Venice AI .NET SDK with the latest API definitions from Venice.

## Overview

The Venice AI SDK is a .NET 8.0 library that wraps the Venice AI REST API. When Venice updates their API (new models, endpoints, or parameters), this SDK must be updated to reflect those changes.

## Prerequisites

Before starting the update process, ensure:

1. You have access to the workspace at the root of this repository
2. .NET 8.0 SDK is installed
3. The project builds successfully before making changes

## Step 1: Fetch Latest API Specification

### 1.1 Download the Latest OpenAPI Spec

Venice publishes their OpenAPI specification. Fetch the latest version:

```
URL: https://api.venice.ai/doc/openapi.json
Alternative: https://docs.venice.ai (may have embedded spec)
```

Save the downloaded spec to:
```
openapi/latest-venice-ai-api.yaml
```

### 1.2 Fetch Live Model Data

Venice provides live endpoints to query available models. Use these to get current model lists:

**Text Models:**
```bash
curl "https://api.venice.ai/api/v1/models?type=text"
```

**Image Models:**
```bash
curl "https://api.venice.ai/api/v1/models?type=image"
```

**Video Models:**
```bash
curl "https://api.venice.ai/api/v1/models?type=video"
```

**All Models:**
```bash
curl "https://api.venice.ai/api/v1/models"
```

Save the response data for reference when updating model enums.

## Step 2: Update Model Enums

### 2.1 File Location
```
src/VeniceAI.SDK/Models/Common/ModelEnums.cs
```

### 2.2 Update TextModel Enum

Compare the live model data with the existing `TextModel` enum. For each new model:

1. Add a new enum value with a descriptive name (use PascalCase)
2. Add the `[JsonStringValue("model-id")]` attribute with the exact API model ID
3. Add XML documentation describing the model
4. Mark obsolete models with `[Obsolete("message")]` attribute

**Example pattern:**
```csharp
/// <summary>
/// Description of the model and its capabilities.
/// </summary>
[JsonStringValue("exact-model-id-from-api")]
NewModelName,
```

### 2.3 Update ImageModel Enum

Same pattern as TextModel. Image model IDs typically look like:
- `venice-sd35`
- `hidream`
- `nano-banana-pro`

### 2.4 Update VideoModel Enum (if exists)

Video models follow patterns like:
- `wan-2.5-preview-image-to-video`
- `kling-2.6-pro-text-to-video`
- `veo3-fast-text-to-video`

If the `VideoModel` enum doesn't exist, create it following the same pattern as `TextModel`.

### 2.5 Update ModelType Enum

Add any new model types (e.g., `asr`, `video`, `embedding`, `upscale`, `inpaint`).

## Step 3: Update JSON Converters

### 3.1 File Location
```
src/VeniceAI.SDK/Models/Common/ModelEnumJsonConverter.cs
```

### 3.2 Add Converters for New Enums

If you added a new enum (like `VideoModel`), create a corresponding JSON converter:

```csharp
public class VideoModelJsonConverter : JsonConverter<VideoModel>
{
    public override VideoModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return ModelEnumExtensions.ParseVideoModel(value ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, VideoModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToApiString());
    }
}
```

## Step 4: Update Extension Methods

### 4.1 File Location
```
src/VeniceAI.SDK/Extensions/ModelEnumExtensions.cs
```

### 4.2 Add Parse Methods for New Enums

```csharp
public static VideoModel ParseVideoModel(string value)
{
    if (TryParseVideoModel(value, out var result))
        return result;
    throw new ArgumentException($"Unknown video model: {value}", nameof(value));
}

public static bool TryParseVideoModel(string value, out VideoModel model)
{
    // Implementation using reflection or switch statement
}
```

## Step 5: Update Request/Response Models

### 5.1 Chat Completion Models
```
src/VeniceAI.SDK/Models/Chat/ChatCompletionRequest.cs
```

Check the OpenAPI spec for new properties in chat completion requests:
- New parameters in the request body
- New properties in `VeniceParameters`
- New nested configuration objects (e.g., `ReasoningConfig`)

### 5.2 Image Generation Models
```
src/VeniceAI.SDK/Models/Images/ImageModels.cs
```

Check for new image generation parameters:
- `variants`
- `aspect_ratio`
- `resolution`
- `enable_web_search`

### 5.3 Video Generation Models (New API)

If Venice adds a Video API, create new model files:
```
src/VeniceAI.SDK/Models/Video/VideoModels.cs
```

Include request/response classes for:
- Queue video generation
- Retrieve video status
- Complete/download video
- Quote video generation cost

### 5.4 Other New APIs

Check for any new API sections in the OpenAPI spec:
- Characters API
- API Keys API
- Billing updates

## Step 6: Update or Create Services

### 6.1 Existing Services
```
src/VeniceAI.SDK/Services/
├── AudioService.cs
├── BillingService.cs
├── ChatService.cs
├── EmbeddingService.cs
├── ImageService.cs
└── ModelService.cs
```

### 6.2 Create New Service Interfaces

For new APIs, create an interface:
```csharp
// src/VeniceAI.SDK/Services/IVideoService.cs
public interface IVideoService
{
    Task<QueueVideoResponse> QueueVideoAsync(QueueVideoRequest request, CancellationToken ct = default);
    Task<RetrieveVideoResponse> RetrieveVideoAsync(string generationId, CancellationToken ct = default);
    // ... other methods
}
```

### 6.3 Create New Service Implementations

```csharp
// src/VeniceAI.SDK/Services/VideoService.cs
public class VideoService : IVideoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VideoService> _logger;
    
    // Implementation following existing service patterns
}
```

## Step 7: Update Client Interface and Implementation

### 7.1 Update IVeniceAIClient
```
src/VeniceAI.SDK/IVeniceAIClient.cs
```

Add properties for new services:
```csharp
IVideoService Video { get; }
ICharacterService Characters { get; }
```

### 7.2 Update VeniceAIClient
```
src/VeniceAI.SDK/VeniceAIClient.cs
```

Initialize new services in the constructor:
```csharp
public IVideoService Video { get; }
public ICharacterService Characters { get; }

public VeniceAIClient(HttpClient httpClient, ILoggerFactory loggerFactory)
{
    // ... existing code
    Video = new VideoService(httpClient, loggerFactory.CreateLogger<VideoService>());
    Characters = new CharacterService(httpClient, loggerFactory.CreateLogger<CharacterService>());
}
```

## Step 8: Update Dependency Injection

### 8.1 File Location
```
src/VeniceAI.SDK/Extensions/ServiceCollectionExtensions.cs
```

### 8.2 Register New Services

```csharp
services.AddScoped<IVideoService, VideoService>();
services.AddScoped<ICharacterService, CharacterService>();
```

## Step 9: Build and Test

### 9.1 Build the SDK
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```

Fix any compilation errors before proceeding.

### 9.2 Run Unit Tests
```bash
dotnet test tests/VeniceAI.SDK.Tests/VeniceAI.SDK.Tests.csproj
```

### 9.3 Run Integration Tests (if API key available)
```bash
dotnet test tests/VeniceAI.SDK.IntegrationTests/VeniceAI.SDK.IntegrationTests.csproj
```

## Step 10: Update Documentation

### 10.1 Update CHANGELOG.md

Add a new version section with:
- New models added
- New APIs added
- New parameters added
- Deprecated models
- Breaking changes

### 10.2 Update README.md

Add examples for new features:
```csharp
// Video generation example
var videoRequest = new QueueVideoRequest
{
    Model = VideoModel.Wan25PreviewTextToVideo,
    Prompt = "A cat walking on the beach"
};
var response = await client.Video.QueueVideoAsync(videoRequest);
```

## Checklist

Use this checklist to ensure completeness:

- [ ] Downloaded latest OpenAPI spec
- [ ] Fetched live model data from API
- [ ] Updated `TextModel` enum with new models
- [ ] Updated `ImageModel` enum with new models
- [ ] Created/updated `VideoModel` enum (if applicable)
- [ ] Updated `ModelType` enum
- [ ] Created JSON converters for new enums
- [ ] Created extension methods for new enums
- [ ] Updated `ChatCompletionRequest` with new parameters
- [ ] Updated `VeniceParameters` with new options
- [ ] Updated image request models
- [ ] Created video request/response models (if applicable)
- [ ] Created new service interfaces
- [ ] Created new service implementations
- [ ] Updated `IVeniceAIClient` interface
- [ ] Updated `VeniceAIClient` implementation
- [ ] Updated DI registration
- [ ] SDK builds successfully
- [ ] Unit tests pass
- [ ] CHANGELOG.md updated
- [ ] README.md updated with new examples

## Common Patterns

### Adding a New Model to an Existing Enum

```csharp
/// <summary>
/// Model description from API documentation.
/// Model ID: exact-model-id
/// </summary>
[JsonStringValue("exact-model-id")]
ModelNameInPascalCase,
```

### Marking a Model as Obsolete

```csharp
/// <summary>
/// This model is deprecated. Use NewModel instead.
/// </summary>
[Obsolete("This model is deprecated. Use NewModel instead.")]
[JsonStringValue("old-model-id")]
OldModelName,
```

### Creating a New Venice Parameter

```csharp
/// <summary>
/// Description of the parameter.
/// </summary>
[JsonPropertyName("snake_case_name")]
public bool? PropertyName { get; set; }
```

### Service Method Pattern

```csharp
public async Task<ResponseType> MethodAsync(RequestType request, CancellationToken cancellationToken = default)
{
    _logger.LogDebug("Method description...");
    
    var response = await _httpClient.PostAsJsonAsync("api/v1/endpoint", request, cancellationToken);
    response.EnsureSuccessStatusCode();
    
    var result = await response.Content.ReadFromJsonAsync<ResponseType>(cancellationToken: cancellationToken);
    return result ?? throw new VeniceAIException("Null response from API");
}
```

## API Endpoint Reference

| Feature | Endpoint | Method |
|---------|----------|--------|
| List Models | `/api/v1/models` | GET |
| Chat Completions | `/api/v1/chat/completions` | POST |
| Generate Image | `/api/v1/images/generations` | POST |
| Upscale Image | `/api/v1/images/upscale` | POST |
| Generate Embeddings | `/api/v1/embeddings` | POST |
| Text-to-Speech | `/api/v1/audio/speech` | POST |
| Queue Video | `/api/v1/video/generate/queue` | POST |
| Retrieve Video | `/api/v1/video/generate/retrieve` | POST |
| Complete Video | `/api/v1/video/generate/complete` | POST |
| Quote Video | `/api/v1/video/generate/quote` | POST |
| List Characters | `/api/v1/characters` | GET |
| Get Character | `/api/v1/characters/{slug}` | GET |

## Notes

- Always maintain backward compatibility by marking removed models as `[Obsolete]` rather than deleting them
- Follow existing code patterns and naming conventions
- Keep JSON property names in snake_case to match the API
- Keep C# property names in PascalCase
- Add comprehensive XML documentation for all public members
- Test with real API calls when possible to verify model availability
