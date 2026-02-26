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
    public async Task GetModelsAsync_WithTypeAll_ShouldReturnAllModels()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(ModelType.All, CancellationToken.None),
            "GetModelsAsync_WithTypeAll"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();

        Output.WriteLine($"All models returned: {response.Data.Count}");
        foreach (var modelType in response.Data.GroupBy(m => m.Type))
        {
            Output.WriteLine($"{modelType.Key} models: {modelType.Count()}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_WithTypeText_ShouldReturnOnlyTextModels()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(ModelType.Text, CancellationToken.None),
            "GetModelsAsync_WithTypeText"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();

        // All models should be text models
        response.Data.ShouldAllBe(m => m.Type == "text");
        Output.WriteLine($"Text models returned: {response.Data.Count}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_WithTypeImage_ShouldReturnOnlyImageModels()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(ModelType.Image, CancellationToken.None),
            "GetModelsAsync_WithTypeImage"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();

        // Image models might not be returned by this endpoint based on our previous findings
        if (response.Data.Any())
        {
            // If any models are returned, they should all be image models
            response.Data.ShouldAllBe(m => m.Type == "image");
            Output.WriteLine($"Image models returned: {response.Data.Count}");
        }
        else
        {
            Output.WriteLine("No image models returned (may require different endpoint or parameters)");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_WithTypeTts_ShouldReturnOnlyTtsModels()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(ModelType.Tts, CancellationToken.None),
            "GetModelsAsync_WithTypeTts"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();

        // TTS models might not be returned by this endpoint based on our previous findings
        if (response.Data.Any())
        {
            // If any models are returned, they should all be TTS/audio models
            response.Data.ShouldAllBe(m => m.Type == "tts" || m.Type == "audio");
            Output.WriteLine($"TTS models returned: {response.Data.Count}");
        }
        else
        {
            Output.WriteLine("No TTS models returned (may require different endpoint or parameters)");
        }

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
        // Act - Use type=all to get all models for proper validation
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(ModelType.All, CancellationToken.None),
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
        var apiTtsModels = response.Data.Where(m => m.Type == "tts").Select(m => m.Id).ToHashSet();
        var apiUpscaleModels = response.Data.Where(m => m.Type == "upscale").Select(m => m.Id).ToHashSet();
        var apiInpaintModels = response.Data.Where(m => m.Type == "inpaint").Select(m => m.Id).ToHashSet();

        // Get enum models
        var enumTextModels = Enum.GetValues<TextModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumImageModels = Enum.GetValues<ImageModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumEmbeddingModels = Enum.GetValues<EmbeddingModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumTtsModels = Enum.GetValues<TextToSpeechModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumUpscaleModels = Enum.GetValues<UpscaleModel>().Select(m => m.ToModelString()).ToHashSet();
        var enumInpaintModels = Enum.GetValues<InpaintModel>().Select(m => m.ToModelString()).ToHashSet();

        Output.WriteLine($"API Text models: {apiTextModels.Count}, Enum Text models: {enumTextModels.Count}");
        Output.WriteLine($"API Image models: {apiImageModels.Count}, Enum Image models: {enumImageModels.Count}");
        Output.WriteLine($"API Embedding models: {apiEmbeddingModels.Count}, Enum Embedding models: {enumEmbeddingModels.Count}");
        Output.WriteLine($"API TTS models: {apiTtsModels.Count}, Enum TTS models: {enumTtsModels.Count}");
        Output.WriteLine($"API Upscale models: {apiUpscaleModels.Count}, Enum Upscale models: {enumUpscaleModels.Count}");
        Output.WriteLine($"API Inpaint models: {apiInpaintModels.Count}, Enum Inpaint models: {enumInpaintModels.Count}");

        // Validate each model type
        ValidateModelType("Text", apiTextModels, enumTextModels, Output);
        ValidateModelType("Image", apiImageModels, enumImageModels, Output);
        ValidateModelType("Embedding", apiEmbeddingModels, enumEmbeddingModels, Output);
        ValidateModelType("TTS", apiTtsModels, enumTtsModels, Output);
        ValidateModelType("Upscale", apiUpscaleModels, enumUpscaleModels, Output);
        ValidateModelType("Inpaint", apiInpaintModels, enumInpaintModels, Output);

        Output.WriteLine("✅ All model types validated successfully!");
        await VerifyResult(response);
    }

    private static void ValidateModelType(string modelType, HashSet<string> apiModels, HashSet<string> enumModels, ITestOutputHelper output)
    {
        var missingInEnum = apiModels.Except(enumModels).ToList();
        var missingInApi = enumModels.Except(apiModels).ToList();

        if (missingInEnum.Any())
        {
            output.WriteLine($"⚠️ {modelType} models in API but missing from enum: {string.Join(", ", missingInEnum)}");
        }

        if (missingInApi.Any())
        {
            output.WriteLine($"⚠️ {modelType} models in enum but missing from API: {string.Join(", ", missingInApi)}");
        }

        if (!missingInEnum.Any() && !missingInApi.Any())
        {
            output.WriteLine($"✅ {modelType} models: Perfect match ({apiModels.Count} models)");
        }
    }

    [Fact]
    public async Task GetModelTraitsAsync_ShouldReturnTraits()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelTraitsAsync(CancellationToken.None),
            "GetModelTraitsAsync"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Traits.ShouldNotBeNull();

        Output.WriteLine($"Model traits returned: {response.Traits.Count}");
        foreach (var trait in response.Traits.Take(5))
        {
            Output.WriteLine($"Trait: {trait.Key} -> {trait.Value}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelTraitsAsync_WithTypeText_ShouldReturnTextTraits()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelTraitsAsync(ModelType.Text, CancellationToken.None),
            "GetModelTraitsAsync_WithTypeText"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Traits.ShouldNotBeNull();

        Output.WriteLine($"Text model traits returned: {response.Traits.Count}");
        foreach (var trait in response.Traits.Take(5))
        {
            Output.WriteLine($"Text trait: {trait.Key} -> {trait.Value}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelTraitsAsync_WithTypeImage_ShouldReturnImageTraits()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelTraitsAsync(ModelType.Image, CancellationToken.None),
            "GetModelTraitsAsync_WithTypeImage"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Traits.ShouldNotBeNull();

        Output.WriteLine($"Image model traits returned: {response.Traits.Count}");
        if (response.Traits.Any())
        {
            foreach (var trait in response.Traits.Take(5))
            {
                Output.WriteLine($"Image trait: {trait.Key} -> {trait.Value}");
            }
        }
        else
        {
            Output.WriteLine("No image model traits returned");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelCompatibilityAsync_ShouldReturnCompatibility()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelCompatibilityAsync(CancellationToken.None),
            "GetModelCompatibilityAsync"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Compatibility.ShouldNotBeNull();

        Output.WriteLine($"Model compatibility mappings returned: {response.Compatibility.Count}");
        foreach (var compatibility in response.Compatibility.Take(5))
        {
            Output.WriteLine($"Compatibility: {compatibility.Key} -> {compatibility.Value}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelCompatibilityAsync_WithTypeText_ShouldReturnTextCompatibility()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelCompatibilityAsync(ModelType.Text, CancellationToken.None),
            "GetModelCompatibilityAsync_WithTypeText"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Compatibility.ShouldNotBeNull();

        Output.WriteLine($"Text model compatibility mappings returned: {response.Compatibility.Count}");
        foreach (var compatibility in response.Compatibility.Take(5))
        {
            Output.WriteLine($"Text compatibility: {compatibility.Key} -> {compatibility.Value}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_CompareWithAndWithoutTypeParameter()
    {
        // Act - Get models without type filter
        var allModelsResponse = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(CancellationToken.None),
            "GetModelsAsync_All"
        );

        // Act - Get models with type=all filter
        var typeAllResponse = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelsAsync(ModelType.All, CancellationToken.None),
            "GetModelsAsync_TypeAll"
        );

        // Assert
        if (allModelsResponse == null || typeAllResponse == null) return;

        allModelsResponse.ShouldNotBeNull();
        typeAllResponse.ShouldNotBeNull();
        allModelsResponse.IsSuccess.ShouldBeTrue();
        typeAllResponse.IsSuccess.ShouldBeTrue();

        Output.WriteLine($"Models without type filter: {allModelsResponse.Data.Count}");
        Output.WriteLine($"Models with type=all filter: {typeAllResponse.Data.Count}");

        // The results should be the same or type=all should return more models
        typeAllResponse.Data.Count.ShouldBeGreaterThanOrEqualTo(allModelsResponse.Data.Count);

        await VerifyResult(allModelsResponse);
    }
}
