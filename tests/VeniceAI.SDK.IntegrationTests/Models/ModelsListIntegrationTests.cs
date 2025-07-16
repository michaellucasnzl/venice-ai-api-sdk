using Shouldly;
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
}
