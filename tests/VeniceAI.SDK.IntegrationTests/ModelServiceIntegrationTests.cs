using Shouldly;
using Xunit;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Model service.
/// </summary>
public class ModelServiceIntegrationTests(ITestOutputHelper output) : IntegrationTestBase(output)
{
    [Fact]
    public async Task GetModelsAsync_ShouldReturnModels()
    {
        // Act
        var response = await Client.Models.GetModelsAsync(TestContext.Current.CancellationToken);

        // Assert
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        
        Output.WriteLine($"Found {response.Data.Count} models");
        
        // Check that we have different types of models
        var textModels = response.Data.Where(m => m.Type == "text").ToList();
        var imageModels = response.Data.Where(m => m.Type == "image").ToList();
        var embeddingModels = response.Data.Where(m => m.Type == "embedding").ToList();
        
        textModels.ShouldNotBeEmpty();
        imageModels.ShouldNotBeEmpty();
        embeddingModels.ShouldNotBeEmpty();
        
        Output.WriteLine($"Text models: {textModels.Count}");
        Output.WriteLine($"Image models: {imageModels.Count}");
        Output.WriteLine($"Embedding models: {embeddingModels.Count}");
        
        // Print first few models of each type
        foreach (var model in textModels.Take(3))
        {
            Output.WriteLine($"Text Model: {model.Id} - {model.ModelSpec.Name}");
        }
        
        foreach (var model in imageModels.Take(3))
        {
            Output.WriteLine($"Image Model: {model.Id} - {model.ModelSpec.Name}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelAsync_WithValidModelId_ShouldReturnModel()
    {
        // Arrange
        var modelId = "llama-3.3-70b";

        // Act
        var model = await Client.Models.GetModelAsync(modelId, TestContext.Current.CancellationToken);

        // Assert
        model.ShouldNotBeNull();
        model.Id.ShouldBe(modelId);
        model.Type.ShouldBe("text");
        model.ModelSpec.ShouldNotBeNull();
        model.ModelSpec.Name.ShouldNotBeNullOrEmpty();
        
        Output.WriteLine($"Model: {model.Id}");
        Output.WriteLine($"Name: {model.ModelSpec.Name}");
        Output.WriteLine($"Type: {model.Type}");
        Output.WriteLine($"Context Tokens: {model.ModelSpec.AvailableContextTokens}");
        
        if (model.ModelSpec.Capabilities != null)
        {
            Output.WriteLine($"Supports Vision: {model.ModelSpec.Capabilities.SupportsVision}");
            Output.WriteLine($"Supports Function Calling: {model.ModelSpec.Capabilities.SupportsFunctionCalling}");
            Output.WriteLine($"Supports Web Search: {model.ModelSpec.Capabilities.SupportsWebSearch}");
        }

        await VerifyResult(model);
    }

    [Fact]
    public async Task GetModelTraitsAsync_ShouldReturnTraits()
    {
        // Act
        var response = await Client.Models.GetModelTraitsAsync(TestContext.Current.CancellationToken);

        // Assert
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Traits.ShouldNotBeEmpty();
        
        Output.WriteLine($"Found {response.Traits.Count} model traits");
        
        foreach (var trait in response.Traits.Take(10))
        {
            Output.WriteLine($"Trait '{trait.Key}': {trait.Value}");
        }
        
        // Check for common traits
        response.Traits.ShouldContainKey("default");
        response.Traits.ShouldContainKey("fastest");

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelCompatibilityAsync_ShouldReturnCompatibilityMapping()
    {
        // Act
        var response = await Client.Models.GetModelCompatibilityAsync(TestContext.Current.CancellationToken);

        // Assert
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Compatibility.ShouldNotBeEmpty();
        
        Output.WriteLine($"Found {response.Compatibility.Count} compatibility mappings");
        
        foreach (var mapping in response.Compatibility.Take(10))
        {
            Output.WriteLine($"'{mapping.Key}' -> '{mapping.Value}'");
        }
        
        // Check for common OpenAI compatibility mappings
        response.Compatibility.Keys.ShouldContain(k => k.StartsWith("gpt"));

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_ModelCapabilities_ShouldBeAccurate()
    {
        // Act
        var response = await Client.Models.GetModelsAsync(TestContext.Current.CancellationToken);

        // Assert
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        
        // Find a vision model and verify capabilities
        var visionModel = response.Data.FirstOrDefault(m => 
            m.ModelSpec.Capabilities?.SupportsVision == true);
        
        if (visionModel != null)
        {
            visionModel.Type.ShouldBe("text");
            visionModel.ModelSpec.Capabilities?.SupportsVision.ShouldBeTrue();
            
            Output.WriteLine($"Vision Model: {visionModel.Id}");
            Output.WriteLine($"Supports Vision: {visionModel.ModelSpec.Capabilities?.SupportsVision}");
        }
        
        // Find a function calling model and verify capabilities
        var functionModel = response.Data.FirstOrDefault(m => 
            m.ModelSpec.Capabilities?.SupportsFunctionCalling == true);
        
        if (functionModel != null)
        {
            functionModel.Type.ShouldBe("text");
            functionModel.ModelSpec.Capabilities?.SupportsFunctionCalling.ShouldBeTrue();
            
            Output.WriteLine($"Function Calling Model: {functionModel.Id}");
            Output.WriteLine($"Supports Function Calling: {functionModel.ModelSpec.Capabilities?.SupportsFunctionCalling}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetModelsAsync_ImageModels_ShouldHaveCorrectConstraints()
    {
        // Act
        var response = await Client.Models.GetModelsAsync(TestContext.Current.CancellationToken);

        // Assert
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        
        var imageModels = response.Data.Where(m => m.Type == "image").ToList();
        imageModels.ShouldNotBeEmpty();
        
        foreach (var model in imageModels.Take(3))
        {
            model.ModelSpec.Constraints.ShouldNotBeNull();
            if (model.ModelSpec.Constraints?.PromptCharacterLimit.HasValue == true)
                model.ModelSpec.Constraints.PromptCharacterLimit.Value.ShouldBeGreaterThan(0);
            model.ModelSpec.Constraints?.Steps.ShouldNotBeNull();
            if (model.ModelSpec.Constraints?.Steps != null)
            {
                model.ModelSpec.Constraints.Steps.Default.ShouldBeGreaterThan(0);
                model.ModelSpec.Constraints.Steps.Max.ShouldBeGreaterThan(0);
            }
            
            Output.WriteLine($"Image Model: {model.Id}");
            Output.WriteLine($"  Prompt Limit: {model.ModelSpec.Constraints?.PromptCharacterLimit}");
            Output.WriteLine($"  Default Steps: {model.ModelSpec.Constraints?.Steps?.Default}");
            Output.WriteLine($"  Max Steps: {model.ModelSpec.Constraints?.Steps?.Max}");
            Output.WriteLine($"  Width/Height Divisor: {model.ModelSpec.Constraints?.WidthHeightDivisor}");
        }

        await VerifyResult(response);
    }
}
