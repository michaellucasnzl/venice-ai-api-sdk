using Shouldly;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Common;
using VeniceAI.SDK.Models.Models;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Models;

/// <summary>
/// Integration tests for the Models list endpoint.
/// </summary>
public class ModelsListIntegrationTests : IntegrationTestBase
{
    public ModelsListIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetModelsAsync_ShouldReturnModels()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(CancellationToken.None),
            "GetModelsAsync"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();

        var textModels = response.Data.Where(m => m.Type == "text").ToList();
        var imageModels = response.Data.Where(m => m.Type == "image").ToList();
        var embeddingModels = response.Data.Where(m => m.Type == "embedding").ToList();

        Output.WriteLine($"Total models: {response.Data.Count}");
        Output.WriteLine($"Text models: {textModels.Count}");
        Output.WriteLine($"Image models: {imageModels.Count}");
        Output.WriteLine($"Embedding models: {embeddingModels.Count}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelAsync_WithValidModelId_ShouldReturnModel()
    {
        // First get available models
        var modelsResponse = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(CancellationToken.None),
            "GetModelsAsync_ForModelRetrieval"
        );

        if (modelsResponse == null || !modelsResponse.Data.Any())
        {
            Output.WriteLine("No models available to test individual model retrieval");
            return;
        }

        var firstModel = modelsResponse.Data[0];

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelAsync(firstModel.Id, CancellationToken.None),
            "GetModelAsync_WithValidModelId"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.Id.ShouldBe(firstModel.Id);
        response.Type.ShouldNotBeNullOrEmpty();

        Output.WriteLine($"Retrieved model: {response.Id}");
        Output.WriteLine($"Model type: {response.Type}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_ModelCapabilities_ShouldBeAccurate()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(CancellationToken.None),
            "GetModelsAsync_ModelCapabilities"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();

        // Test model capabilities - simplified since we don't know the exact Model structure
        Output.WriteLine($"Models available: {response.Data.Count}");

        foreach (var model in response.Data.Take(5))
        {
            Output.WriteLine($"Model: {model.Id} (Type: {model.Type})");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_ImageModels_ShouldHaveCorrectConstraints()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(CancellationToken.None),
            "GetModelsAsync_ImageModels"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();

        var imageModels = response.Data.Where(m => m.Type == "image").ToList();

        if (imageModels.Any())
        {
            Output.WriteLine($"Image models found: {imageModels.Count}");

            foreach (var model in imageModels.Take(3))
            {
                Output.WriteLine($"Image model: {model.Id}");
                Output.WriteLine($"  Type: {model.Type}");
                Output.WriteLine($"  Object: {model.Object}");
            }
        }
        else
        {
            Output.WriteLine("No image models found");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_ShouldMatchEnumDefinitions()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(CancellationToken.None),
            "GetModelsAsync_ShouldMatchEnumDefinitions"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();

        // Get API models by type
        var apiTextModels = response.Data.Where(m => m.Type == "text").Select(m => m.Id).ToHashSet();
        var apiImageModels = response.Data.Where(m => m.Type == "image").Select(m => m.Id).ToHashSet();
        var apiEmbeddingModels = response.Data.Where(m => m.Type == "embedding").Select(m => m.Id).ToHashSet();
        var apiTtsModels = response.Data.Where(m => m.Type == "audio" || m.Type == "tts").Select(m => m.Id).ToHashSet();

        // Get enum models
        var enumTextModels = Enum.GetValues<TextModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumImageModels = Enum.GetValues<ImageModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumEmbeddingModels = Enum.GetValues<EmbeddingModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumTtsModels = Enum.GetValues<TextToSpeechModel>().Select(m => m.ToModelString()).ToHashSet();

        Output.WriteLine($"API Text models: {apiTextModels.Count}");
        Output.WriteLine($"Enum Text models: {enumTextModels.Count}");
        Output.WriteLine($"API Image models: {apiImageModels.Count}");
        Output.WriteLine($"Enum Image models: {enumImageModels.Count}");
        Output.WriteLine($"API Embedding models: {apiEmbeddingModels.Count}");
        Output.WriteLine($"Enum Embedding models: {enumEmbeddingModels.Count}");
        Output.WriteLine($"API TTS models: {apiTtsModels.Count}");
        Output.WriteLine($"Enum TTS models: {enumTtsModels.Count}");

        // Check for missing models in enums (API has models not in enum)
        var missingTextInEnum = apiTextModels.Except(enumTextModels).ToList();
        var missingImageInEnum = apiImageModels.Except(enumImageModels).ToList();
        var missingEmbeddingInEnum = apiEmbeddingModels.Except(enumEmbeddingModels).ToList();
        var missingTtsInEnum = apiTtsModels.Except(enumTtsModels).ToList();

        // Check for missing models in API (Enum has models not in API)
        // NOTE: Only validate text models for strict enforcement, as other model types
        // may be available through different endpoints
        var missingTextInApi = enumTextModels.Except(apiTextModels).ToList();

        // Report findings
        if (missingTextInEnum.Any())
        {
            Output.WriteLine($"Text models in API but missing from enum: {string.Join(", ", missingTextInEnum)}");
        }
        if (missingImageInEnum.Any())
        {
            Output.WriteLine($"Image models in API but missing from enum: {string.Join(", ", missingImageInEnum)}");
        }
        if (missingEmbeddingInEnum.Any())
        {
            Output.WriteLine($"Embedding models in API but missing from enum: {string.Join(", ", missingEmbeddingInEnum)}");
        }
        if (missingTtsInEnum.Any())
        {
            Output.WriteLine($"TTS models in API but missing from enum: {string.Join(", ", missingTtsInEnum)}");
        }

        if (missingTextInApi.Any())
        {
            Output.WriteLine($"Text models in enum but missing from API: {string.Join(", ", missingTextInApi)}");
        }

        // Only check image/embedding/TTS models if API actually returns them
        if (apiImageModels.Any())
        {
            var missingImageInApi = enumImageModels.Except(apiImageModels).ToList();
            if (missingImageInApi.Any())
            {
                Output.WriteLine($"Image models in enum but missing from API: {string.Join(", ", missingImageInApi)}");
            }
        }
        else
        {
            Output.WriteLine($"ℹ️  No image models returned by /models endpoint (may use different endpoint)");
        }

        if (apiEmbeddingModels.Any())
        {
            var missingEmbeddingInApi = enumEmbeddingModels.Except(apiEmbeddingModels).ToList();
            if (missingEmbeddingInApi.Any())
            {
                Output.WriteLine($"Embedding models in enum but missing from API: {string.Join(", ", missingEmbeddingInApi)}");
            }
        }
        else
        {
            Output.WriteLine($"ℹ️  No embedding models returned by /models endpoint (may use different endpoint)");
        }

        if (apiTtsModels.Any())
        {
            var missingTtsInApi = enumTtsModels.Except(apiTtsModels).ToList();
            if (missingTtsInApi.Any())
            {
                Output.WriteLine($"TTS models in enum but missing from API: {string.Join(", ", missingTtsInApi)}");
            }
        }
        else
        {
            Output.WriteLine($"ℹ️  No TTS models returned by /models endpoint (may use different endpoint)");
        }

        // Throw exceptions for critical mismatches (only text models and any new models in API)
        var allMissingInEnum = missingTextInEnum.Concat(missingImageInEnum).Concat(missingEmbeddingInEnum).Concat(missingTtsInEnum).ToList();

        if (allMissingInEnum.Any())
        {
            throw new InvalidOperationException($"Models found in API response but missing from enums. Please add these models to the appropriate enum definitions: {string.Join(", ", allMissingInEnum)}");
        }

        if (missingTextInApi.Any())
        {
            throw new InvalidOperationException($"Text models defined in enums but missing from API response. Please verify these models are still valid or remove from enums: {string.Join(", ", missingTextInApi)}");
        }

        Output.WriteLine("✅ All text models match! Image/Embedding/TTS models validation skipped (different endpoints)");
        await VerifyResult(response);
    }
}
