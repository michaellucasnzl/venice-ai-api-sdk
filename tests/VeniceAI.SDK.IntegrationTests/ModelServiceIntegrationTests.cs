using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Model service.
/// </summary>
public class ModelServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly IVeniceAIClient _client;
    private readonly ITestOutputHelper _output;

    public ModelServiceIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<ModelServiceIntegrationTests>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);
                });
                
                services.AddVeniceAI(context.Configuration);
            });

        _host = hostBuilder.Build();
        _client = _host.Services.GetRequiredService<IVeniceAIClient>();
    }

    [Fact]
    public async Task GetModelsAsync_ShouldReturnModels()
    {
        // Act
        var response = await _client.Models.GetModelsAsync();

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        
        _output.WriteLine($"Found {response.Data.Count} models");
        
        // Check that we have different types of models
        var textModels = response.Data.Where(m => m.Type == "text").ToList();
        var imageModels = response.Data.Where(m => m.Type == "image").ToList();
        var embeddingModels = response.Data.Where(m => m.Type == "embedding").ToList();
        
        textModels.Should().NotBeEmpty();
        imageModels.Should().NotBeEmpty();
        embeddingModels.Should().NotBeEmpty();
        
        _output.WriteLine($"Text models: {textModels.Count}");
        _output.WriteLine($"Image models: {imageModels.Count}");
        _output.WriteLine($"Embedding models: {embeddingModels.Count}");
        
        // Print first few models of each type
        foreach (var model in textModels.Take(3))
        {
            _output.WriteLine($"Text Model: {model.Id} - {model.ModelSpec.Name}");
        }
        
        foreach (var model in imageModels.Take(3))
        {
            _output.WriteLine($"Image Model: {model.Id} - {model.ModelSpec.Name}");
        }
    }

    [Fact]
    public async Task GetModelAsync_WithValidModelId_ShouldReturnModel()
    {
        // Arrange
        var modelId = "llama-3.3-70b";

        // Act
        var model = await _client.Models.GetModelAsync(modelId);

        // Assert
        model.Should().NotBeNull();
        model.Id.Should().Be(modelId);
        model.Type.Should().Be("text");
        model.ModelSpec.Should().NotBeNull();
        model.ModelSpec.Name.Should().NotBeNullOrEmpty();
        
        _output.WriteLine($"Model: {model.Id}");
        _output.WriteLine($"Name: {model.ModelSpec.Name}");
        _output.WriteLine($"Type: {model.Type}");
        _output.WriteLine($"Context Tokens: {model.ModelSpec.AvailableContextTokens}");
        
        if (model.ModelSpec.Capabilities != null)
        {
            _output.WriteLine($"Supports Vision: {model.ModelSpec.Capabilities.SupportsVision}");
            _output.WriteLine($"Supports Function Calling: {model.ModelSpec.Capabilities.SupportsFunctionCalling}");
            _output.WriteLine($"Supports Web Search: {model.ModelSpec.Capabilities.SupportsWebSearch}");
        }
    }

    [Fact]
    public async Task GetModelTraitsAsync_ShouldReturnTraits()
    {
        // Act
        var response = await _client.Models.GetModelTraitsAsync();

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Traits.Should().NotBeEmpty();
        
        _output.WriteLine($"Found {response.Traits.Count} model traits");
        
        foreach (var trait in response.Traits.Take(10))
        {
            _output.WriteLine($"Trait '{trait.Key}': {trait.Value}");
        }
        
        // Check for common traits
        response.Traits.Should().ContainKey("default");
        response.Traits.Should().ContainKey("fastest");
    }

    [Fact]
    public async Task GetModelCompatibilityAsync_ShouldReturnCompatibilityMapping()
    {
        // Act
        var response = await _client.Models.GetModelCompatibilityAsync();

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Compatibility.Should().NotBeEmpty();
        
        _output.WriteLine($"Found {response.Compatibility.Count} compatibility mappings");
        
        foreach (var mapping in response.Compatibility.Take(10))
        {
            _output.WriteLine($"'{mapping.Key}' -> '{mapping.Value}'");
        }
        
        // Check for common OpenAI compatibility mappings
        response.Compatibility.Keys.Should().Contain(k => k.StartsWith("gpt"));
    }

    [Fact]
    public async Task GetModelsAsync_ModelCapabilities_ShouldBeAccurate()
    {
        // Act
        var response = await _client.Models.GetModelsAsync();

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        
        // Find a vision model and verify capabilities
        var visionModel = response.Data.FirstOrDefault(m => 
            m.ModelSpec.Capabilities?.SupportsVision == true);
        
        if (visionModel != null)
        {
            visionModel.Type.Should().Be("text");
            visionModel.ModelSpec.Capabilities?.SupportsVision.Should().BeTrue();
            
            _output.WriteLine($"Vision Model: {visionModel.Id}");
            _output.WriteLine($"Supports Vision: {visionModel.ModelSpec.Capabilities?.SupportsVision}");
        }
        
        // Find a function calling model and verify capabilities
        var functionModel = response.Data.FirstOrDefault(m => 
            m.ModelSpec.Capabilities?.SupportsFunctionCalling == true);
        
        if (functionModel != null)
        {
            functionModel.Type.Should().Be("text");
            functionModel.ModelSpec.Capabilities?.SupportsFunctionCalling.Should().BeTrue();
            
            _output.WriteLine($"Function Calling Model: {functionModel.Id}");
            _output.WriteLine($"Supports Function Calling: {functionModel.ModelSpec.Capabilities?.SupportsFunctionCalling}");
        }
    }

    [Fact]
    public async Task GetModelsAsync_ImageModels_ShouldHaveCorrectConstraints()
    {
        // Act
        var response = await _client.Models.GetModelsAsync();

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        
        var imageModels = response.Data.Where(m => m.Type == "image").ToList();
        imageModels.Should().NotBeEmpty();
        
        foreach (var model in imageModels.Take(3))
        {
            model.ModelSpec.Constraints.Should().NotBeNull();
            model.ModelSpec.Constraints?.PromptCharacterLimit.Should().BeGreaterThan(0);
            model.ModelSpec.Constraints?.Steps.Should().NotBeNull();
            model.ModelSpec.Constraints?.Steps?.Default.Should().BeGreaterThan(0);
            model.ModelSpec.Constraints?.Steps?.Max.Should().BeGreaterThan(0);
            
            _output.WriteLine($"Image Model: {model.Id}");
            _output.WriteLine($"  Prompt Limit: {model.ModelSpec.Constraints?.PromptCharacterLimit}");
            _output.WriteLine($"  Default Steps: {model.ModelSpec.Constraints?.Steps?.Default}");
            _output.WriteLine($"  Max Steps: {model.ModelSpec.Constraints?.Steps?.Max}");
            _output.WriteLine($"  Width/Height Divisor: {model.ModelSpec.Constraints?.WidthHeightDivisor}");
        }
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
