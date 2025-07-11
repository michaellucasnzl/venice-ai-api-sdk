using FluentAssertions;
using VeniceAI.SDK.Models.Images;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Image service.
/// </summary>
public class ImageServiceIntegrationTests(ITestOutputHelper output) : IntegrationTestBase(output)
{
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
        var response = await Client.Images.GenerateImageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();

        Output.WriteLine($"Generated image with {response.Data[0].B64Json?.Length} characters in base64");

        await VerifyResult(response);
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
        var response = await Client.Images.GenerateImageSimpleAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();

        Output.WriteLine($"Generated simple image with {response.Data[0].B64Json?.Length} characters in base64");

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetImageStylesAsync_ShouldReturnStyles()
    {
        // Act
        var response = await Client.Images.GetImageStylesAsync(TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Styles.Should().NotBeEmpty();

        Output.WriteLine($"Found {response.Styles.Count} image styles:");
        foreach (var style in response.Styles.Take(5))
        {
            Output.WriteLine($"- {style.Name}: {style.Description}");
        }

        await VerifyResult(response);
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
        var response = await Client.Images.GenerateImageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();

        Output.WriteLine($"Generated styled image with {response.Data[0].B64Json?.Length} characters in base64");

        await VerifyResult(response);
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
        var response = await Client.Images.GenerateImageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].B64Json.Should().NotBeNullOrEmpty();

        Output.WriteLine(
            $"Generated image with negative prompt, {response.Data[0].B64Json?.Length} characters in base64");

        await VerifyResult(response);
    }
}