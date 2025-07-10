using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Images;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Image service.
/// </summary>
public class ImageServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly IVeniceAIClient _client;
    private readonly ITestOutputHelper _output;

    public ImageServiceIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<ImageServiceIntegrationTests>();
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
    public async Task GenerateImageAsync_WithValidRequest_ShouldReturnImage()
    {
        // Arrange
        var request = new GenerateImageRequest
        {
            Model = "hidream",
            Prompt = "A beautiful sunset over a mountain range",
            Width = 1024,
            Height = 1024,
            Steps = 20,
            CfgScale = 7.5,
            Format = "webp"
        };

        // Act
        var response = await _client.Images.GenerateImageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();
        
        _output.WriteLine($"Generated image with {response.Data[0].B64Json?.Length} characters in base64");
    }

    [Fact]
    public async Task GenerateImageSimpleAsync_WithValidRequest_ShouldReturnImage()
    {
        // Arrange
        var request = new SimpleGenerateImageRequest
        {
            Prompt = "A futuristic city skyline at night",
            Model = "hidream",
            Size = "1024x1024",
            N = 1,
            ResponseFormat = "b64_json"
        };

        // Act
        var response = await _client.Images.GenerateImageSimpleAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();
        
        _output.WriteLine($"Generated simple image with {response.Data[0].B64Json?.Length} characters in base64");
    }

    [Fact]
    public async Task GetImageStylesAsync_ShouldReturnStyles()
    {
        // Act
        var response = await _client.Images.GetImageStylesAsync();

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Styles.Should().NotBeEmpty();
        
        _output.WriteLine($"Found {response.Styles.Count} image styles:");
        foreach (var style in response.Styles.Take(5))
        {
            _output.WriteLine($"- {style.Name}: {style.Description}");
        }
    }

    [Fact]
    public async Task GenerateImageAsync_WithStylePreset_ShouldReturnImage()
    {
        // Arrange
        var request = new GenerateImageRequest
        {
            Model = "hidream",
            Prompt = "A majestic dragon",
            Width = 1024,
            Height = 1024,
            Steps = 25,
            StylePreset = "3D Model",
            SafeMode = true
        };

        // Act
        var response = await _client.Images.GenerateImageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();
        
        _output.WriteLine($"Generated styled image with {response.Data[0].B64Json?.Length} characters in base64");
    }

    [Fact]
    public async Task GenerateImageAsync_WithNegativePrompt_ShouldReturnImage()
    {
        // Arrange
        var request = new GenerateImageRequest
        {
            Model = "hidream",
            Prompt = "A beautiful landscape with flowers",
            NegativePrompt = "people, animals, buildings",
            Width = 1024,
            Height = 1024,
            Steps = 20,
            Seed = 12345
        };

        // Act
        var response = await _client.Images.GenerateImageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();
        
        _output.WriteLine($"Generated image with negative prompt, {response.Data[0].B64Json?.Length} characters in base64");
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
