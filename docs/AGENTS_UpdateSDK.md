# AGENTS_UpdateSDK.md

> **Purpose**: Step-by-step instructions for AI coding agents to update the Venice AI .NET SDK with the latest API definitions from Venice.

---

## Quick Reference

| Item | Value |
|------|-------|
| **SDK Location** | `src/VeniceAI.SDK/` |
| **Target Framework** | .NET 8.0 |
| **Test Command** | `dotnet test` |
| **Build Command** | `dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj` |
| **OpenAPI Spec URL** | `https://api.venice.ai/doc/openapi.json` |
| **Models API** | `https://api.venice.ai/api/v1/models` |

---

## Prerequisites

Run these commands to verify your environment before starting:

```bash
# Verify .NET 8.0 SDK is installed
dotnet --version

# Verify the project builds successfully
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj

# Verify tests pass before making changes
dotnet test tests/VeniceAI.SDK.Tests/VeniceAI.SDK.Tests.csproj
```

**STOP** if any command fails. Fix the issue before proceeding.

---

## Task Workflow Overview

Execute these steps **in order**. Each step has a validation checkpoint.

1. [Fetch Latest API Data](#step-1-fetch-latest-api-data)
2. [Update Model Enums](#step-2-update-model-enums)
3. [Update JSON Converters](#step-3-update-json-converters)
4. [Update Extension Methods](#step-4-update-extension-methods)
5. [Update Request/Response Models](#step-5-update-requestresponse-models)
6. [Update or Create Services](#step-6-update-or-create-services)
7. [Update Client Interface](#step-7-update-client-interface)
8. [Update Dependency Injection](#step-8-update-dependency-injection)
9. [Build and Test](#step-9-build-and-test)
10. [Update Documentation](#step-10-update-documentation)

---

## Step 1: Fetch Latest API Data

### 1.1 Download OpenAPI Specification

```bash
# Download the latest OpenAPI spec
curl -o openapi/latest-venice-ai-api.yaml "https://api.venice.ai/doc/openapi.json"
```

### 1.2 Fetch Live Model Data

Execute these commands and save the output for reference:

```bash
# Fetch all models (primary source of truth)
curl "https://api.venice.ai/api/v1/models" > openapi/models-all.json

# Fetch text models
curl "https://api.venice.ai/api/v1/models?type=text" > openapi/models-text.json

# Fetch image models
curl "https://api.venice.ai/api/v1/models?type=image" > openapi/models-image.json

# Fetch video models
curl "https://api.venice.ai/api/v1/models?type=video" > openapi/models-video.json
```

### ✅ Checkpoint 1
- [ ] `openapi/latest-venice-ai-api.yaml` exists and is not empty
- [ ] `openapi/models-all.json` contains valid JSON with model data

---

## Step 2: Update Model Enums

### File to Edit
```
src/VeniceAI.SDK/Models/Common/ModelEnums.cs
```

### 2.1 Update TextModel Enum

**Instructions:**
1. Open `src/VeniceAI.SDK/Models/Common/ModelEnums.cs`
2. Compare models from `openapi/models-text.json` with existing `TextModel` enum values
3. For each **new model** found in the API response, add an enum value

**Code Pattern for New Model:**
```csharp
/// <summary>
/// [Description from API response or Venice docs].
/// Model ID: [exact-model-id]
/// </summary>
[JsonStringValue("exact-model-id")]
ModelNameInPascalCase,
```

**Naming Convention:**
- Convert `model-id-with-dashes` to `ModelIdWithDashes` (PascalCase)
- Remove version numbers from name if they make it unwieldy
- Example: `llama-3.3-70b` → `Llama3_3_70B`

### 2.2 Update ImageModel Enum

Same process as TextModel. Common image model ID patterns:
- `venice-sd35`
- `hidream`
- `nano-banana-pro`
- `flux-dev`

### 2.3 Update or Create VideoModel Enum

If `VideoModel` enum does not exist in `ModelEnums.cs`, create it:

```csharp
/// <summary>
/// Available video generation models.
/// </summary>
[JsonConverter(typeof(VideoModelJsonConverter))]
public enum VideoModel
{
    /// <summary>
    /// [Description].
    /// </summary>
    [JsonStringValue("model-id")]
    ModelName,
}
```

Common video model ID patterns:
- `wan-2.5-preview-image-to-video`
- `kling-2.6-pro-text-to-video`
- `veo3-fast-text-to-video`

### 2.4 Update ModelType Enum

Add any new model types found in the API response:
- `text`, `image`, `video`, `asr`, `embedding`, `upscale`, `inpaint`

### 2.5 Mark Obsolete Models

For models that no longer appear in the API response:

```csharp
/// <summary>
/// DEPRECATED: This model is no longer available. Use [NewModel] instead.
/// </summary>
[Obsolete("This model is deprecated. Use [NewModel] instead.")]
[JsonStringValue("old-model-id")]
OldModelName,
```

**IMPORTANT:** Never delete enum values. Always mark as `[Obsolete]` to maintain backward compatibility.

### ✅ Checkpoint 2
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 3: Update JSON Converters

### File to Edit
```
src/VeniceAI.SDK/Models/Common/ModelEnumJsonConverter.cs
```

### 3.1 Add Converter for New Enums

If you created a new enum (e.g., `VideoModel`), add a corresponding converter:

```csharp
/// <summary>
/// JSON converter for <see cref="VideoModel"/> enum.
/// </summary>
public class VideoModelJsonConverter : JsonConverter<VideoModel>
{
    /// <inheritdoc />
    public override VideoModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return ModelEnumExtensions.ParseVideoModel(value ?? string.Empty);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, VideoModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToApiString());
    }
}
```

### ✅ Checkpoint 3
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 4: Update Extension Methods

### File to Edit
```
src/VeniceAI.SDK/Extensions/ModelEnumExtensions.cs
```

### 4.1 Add Parse Methods for New Enums

If you created a new enum, add corresponding extension methods:

```csharp
/// <summary>
/// Parses a string value to a <see cref="VideoModel"/>.
/// </summary>
/// <param name="value">The API string value.</param>
/// <returns>The parsed enum value.</returns>
/// <exception cref="ArgumentException">Thrown when the value is not recognized.</exception>
public static VideoModel ParseVideoModel(string value)
{
    if (TryParseVideoModel(value, out var result))
        return result;
    throw new ArgumentException($"Unknown video model: {value}", nameof(value));
}

/// <summary>
/// Tries to parse a string value to a <see cref="VideoModel"/>.
/// </summary>
/// <param name="value">The API string value.</param>
/// <param name="model">The parsed enum value if successful.</param>
/// <returns>True if parsing was successful; otherwise, false.</returns>
public static bool TryParseVideoModel(string value, out VideoModel model)
{
    // Use same pattern as existing TryParseTextModel/TryParseImageModel methods
    // Typically uses reflection to find [JsonStringValue] attribute
}
```

### 4.2 Add ToApiString Method for New Enums

```csharp
/// <summary>
/// Converts a <see cref="VideoModel"/> to its API string representation.
/// </summary>
public static string ToApiString(this VideoModel model)
{
    // Use same pattern as existing ToApiString methods
}
```

### ✅ Checkpoint 4
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 5: Update Request/Response Models

### 5.1 Chat Completion Models

**File:** `src/VeniceAI.SDK/Models/Chat/ChatCompletionRequest.cs`

Check the OpenAPI spec for new properties. Common additions:
- New parameters in request body
- New properties in `VeniceParameters` class
- New nested configuration objects (e.g., `ReasoningConfig`)

**Pattern for New Property:**
```csharp
/// <summary>
/// [Description of the parameter from OpenAPI spec].
/// </summary>
[JsonPropertyName("snake_case_name")]
public TypeName? PropertyName { get; set; }
```

### 5.2 Image Generation Models

**File:** `src/VeniceAI.SDK/Models/Images/ImageModels.cs`

Check for new image generation parameters:
- `variants`
- `aspect_ratio`
- `resolution`
- `enable_web_search`
- `seed`
- `style_preset`

### 5.3 Video Generation Models

**Directory:** `src/VeniceAI.SDK/Models/Video/`

If Venice has a Video API and the directory doesn't exist, create it:

```bash
mkdir -p src/VeniceAI.SDK/Models/Video
```

**Create File:** `src/VeniceAI.SDK/Models/Video/VideoModels.cs`

Include these classes based on the OpenAPI spec:
- `QueueVideoRequest`
- `QueueVideoResponse`
- `RetrieveVideoRequest`
- `RetrieveVideoResponse`
- `CompleteVideoRequest`
- `CompleteVideoResponse`
- `QuoteVideoRequest`
- `QuoteVideoResponse`

### 5.4 Other New APIs

Check OpenAPI spec for these sections and add models as needed:
- Characters API
- API Keys API
- Billing updates

### ✅ Checkpoint 5
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 6: Update or Create Services

### Existing Services Directory
```
src/VeniceAI.SDK/Services/
```

### 6.1 Create New Service Interface

**Example for Video Service:**

Create file: `src/VeniceAI.SDK/Services/IVideoService.cs`

```csharp
using VeniceAI.SDK.Models.Video;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for video generation operations.
/// </summary>
public interface IVideoService
{
    /// <summary>
    /// Queues a video generation request.
    /// </summary>
    /// <param name="request">The video generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The queue response containing the generation ID.</returns>
    Task<QueueVideoResponse> QueueVideoAsync(QueueVideoRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the status of a video generation.
    /// </summary>
    /// <param name="generationId">The generation ID from the queue response.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The current status of the video generation.</returns>
    Task<RetrieveVideoResponse> RetrieveVideoAsync(string generationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the completed video.
    /// </summary>
    /// <param name="generationId">The generation ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completed video response with download URL.</returns>
    Task<CompleteVideoResponse> CompleteVideoAsync(string generationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a cost quote for video generation.
    /// </summary>
    /// <param name="request">The quote request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cost quote.</returns>
    Task<QuoteVideoResponse> QuoteVideoAsync(QuoteVideoRequest request, CancellationToken cancellationToken = default);
}
```

### 6.2 Create Service Implementation

Create file: `src/VeniceAI.SDK/Services/VideoService.cs`

**Service Method Pattern:**
```csharp
public async Task<ResponseType> MethodAsync(RequestType request, CancellationToken cancellationToken = default)
{
    _logger.LogDebug("Calling [endpoint description]...");
    
    var response = await _httpClient.PostAsJsonAsync("api/v1/endpoint", request, cancellationToken);
    response.EnsureSuccessStatusCode();
    
    var result = await response.Content.ReadFromJsonAsync<ResponseType>(cancellationToken: cancellationToken);
    return result ?? throw new VeniceAIException("Null response from API");
}
```

### ✅ Checkpoint 6
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 7: Update Client Interface

### 7.1 Update IVeniceAIClient

**File:** `src/VeniceAI.SDK/IVeniceAIClient.cs`

Add properties for new services:

```csharp
/// <summary>
/// Gets the video generation service.
/// </summary>
IVideoService Video { get; }

/// <summary>
/// Gets the characters service.
/// </summary>
ICharacterService Characters { get; }
```

### 7.2 Update VeniceAIClient

**File:** `src/VeniceAI.SDK/VeniceAIClient.cs`

Add service properties and initialize in constructor:

```csharp
/// <inheritdoc />
public IVideoService Video { get; }

/// <inheritdoc />
public ICharacterService Characters { get; }

// In constructor, add:
Video = new VideoService(httpClient, loggerFactory.CreateLogger<VideoService>());
Characters = new CharacterService(httpClient, loggerFactory.CreateLogger<CharacterService>());
```

### ✅ Checkpoint 7
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 8: Update Dependency Injection

### File to Edit
```
src/VeniceAI.SDK/Extensions/ServiceCollectionExtensions.cs
```

### Register New Services

Add registrations for new services:

```csharp
services.AddScoped<IVideoService, VideoService>();
services.AddScoped<ICharacterService, CharacterService>();
```

### ✅ Checkpoint 8
```bash
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj
```
Build must succeed before proceeding.

---

## Step 9: Build and Test

### 9.1 Full Build

```bash
# Build the entire solution
dotnet build VeniceAI.sln
```

### 9.2 Run Unit Tests

```bash
# Run unit tests
dotnet test tests/VeniceAI.SDK.Tests/VeniceAI.SDK.Tests.csproj --verbosity normal
```

### 9.3 Run Integration Tests (Optional - Requires API Key)

```bash
# Set your API key first
export VENICE_API_KEY="your-api-key"

# Run integration tests
dotnet test tests/VeniceAI.SDK.IntegrationTests/VeniceAI.SDK.IntegrationTests.csproj --verbosity normal
```

### ✅ Checkpoint 9
- [ ] Solution builds with no errors
- [ ] Unit tests pass
- [ ] Integration tests pass (if API key available)

---

## Step 10: Update Documentation

### 10.1 Update CHANGELOG.md

**File:** `CHANGELOG.md`

Add a new version section at the top:

```markdown
## [X.Y.Z] - YYYY-MM-DD

### Added
- New text models: [list new models]
- New image models: [list new models]
- New video models: [list new models]
- Video generation API support
- New parameters: [list new parameters]

### Changed
- [List any changed behavior]

### Deprecated
- [List deprecated models with replacement suggestions]

### Fixed
- [List any bug fixes]
```

### 10.2 Update README.md

**File:** `README.md`

Add usage examples for new features:

```csharp
// Video generation example
var videoRequest = new QueueVideoRequest
{
    Model = VideoModel.Wan25PreviewTextToVideo,
    Prompt = "A cat walking on the beach"
};
var response = await client.Video.QueueVideoAsync(videoRequest);
```

### ✅ Checkpoint 10
- [ ] CHANGELOG.md has new version entry
- [ ] README.md includes examples for new features

---

## Final Validation

Run this complete validation sequence:

```bash
# 1. Clean and rebuild
dotnet clean VeniceAI.sln
dotnet build VeniceAI.sln

# 2. Run all tests
dotnet test VeniceAI.sln --verbosity normal

# 3. Check for warnings
dotnet build VeniceAI.sln --verbosity detailed | grep -i warning
```

---

## Completion Checklist

Mark each item as complete:

### API Data
- [ ] Downloaded latest OpenAPI spec to `openapi/latest-venice-ai-api.yaml`
- [ ] Fetched live model data from API

### Model Enums (`src/VeniceAI.SDK/Models/Common/ModelEnums.cs`)
- [ ] Updated `TextModel` enum with new models
- [ ] Updated `ImageModel` enum with new models
- [ ] Created/updated `VideoModel` enum (if applicable)
- [ ] Updated `ModelType` enum
- [ ] Marked obsolete models with `[Obsolete]` attribute

### JSON Converters (`src/VeniceAI.SDK/Models/Common/ModelEnumJsonConverter.cs`)
- [ ] Created JSON converters for any new enums

### Extension Methods (`src/VeniceAI.SDK/Extensions/ModelEnumExtensions.cs`)
- [ ] Created parse methods for any new enums
- [ ] Created `ToApiString` methods for any new enums

### Request/Response Models
- [ ] Updated `ChatCompletionRequest` with new parameters
- [ ] Updated `VeniceParameters` with new options
- [ ] Updated image request models
- [ ] Created video request/response models (if applicable)

### Services (`src/VeniceAI.SDK/Services/`)
- [ ] Created new service interfaces
- [ ] Created new service implementations

### Client (`src/VeniceAI.SDK/`)
- [ ] Updated `IVeniceAIClient` interface
- [ ] Updated `VeniceAIClient` implementation

### Dependency Injection (`src/VeniceAI.SDK/Extensions/ServiceCollectionExtensions.cs`)
- [ ] Registered new services

### Build & Test
- [ ] SDK builds successfully
- [ ] Unit tests pass
- [ ] Integration tests pass (if API key available)

### Documentation
- [ ] `CHANGELOG.md` updated with new version
- [ ] `README.md` updated with new examples

---

## API Endpoint Reference

| Feature | Endpoint | Method |
|---------|----------|--------|
| List Models | `/api/v1/models` | GET |
| List Models by Type | `/api/v1/models?type={text\|image\|video}` | GET |
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

---

## Troubleshooting

### Build Fails After Adding New Enum

**Symptom:** `error CS0246: The type or name 'VideoModelJsonConverter' could not be found`

**Solution:** Ensure you created the JSON converter class in `ModelEnumJsonConverter.cs` before adding `[JsonConverter(typeof(VideoModelJsonConverter))]` to the enum.

### Model Not Serializing Correctly

**Symptom:** Model serializes as integer instead of string

**Solution:** Ensure the enum has `[JsonConverter(typeof(ModelNameJsonConverter))]` attribute.

### Tests Fail with "Unknown model"

**Symptom:** `ArgumentException: Unknown video model: new-model-id`

**Solution:** Add the model to the enum with the correct `[JsonStringValue("new-model-id")]` attribute.

### Integration Tests Fail with 401

**Symptom:** `HttpRequestException: Response status code does not indicate success: 401 (Unauthorized)`

**Solution:** Ensure `VENICE_API_KEY` environment variable is set correctly.

---

## Code Style Requirements

- **JSON property names:** `snake_case` (matches API)
- **C# property names:** `PascalCase`
- **Enum values:** `PascalCase`
- **File names:** `PascalCase.cs`
- **All public members:** Must have XML documentation
- **Nullable types:** Use `?` for optional properties
- **Async methods:** Suffix with `Async`, include `CancellationToken` parameter
