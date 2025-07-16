using Shouldly;
using VeniceAI.SDK.Models.Images;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Image service.
/// </summary>
public class ImageServiceIntegrationTests : IntegrationTestBase
{
    public ImageServiceIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GenerateImageAsync_WithValidRequest_ShouldReturnImage()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new GenerateImageRequest
        {
            Model = "flux-dev",
            Prompt = "A beautiful sunset over the ocean with sailboats in the distance",
            Width = 1024,
            Height = 1024,
            Format = "png"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.GenerateImageAsync(request, CancellationToken.None),
            "GenerateImageAsync_WithValidRequest"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        
        // Check if we have either URL or base64 data
        var firstImage = response.Data[0];
        (firstImage.Url != null || firstImage.B64Json != null).ShouldBeTrue("Image should have either URL or base64 data");

        if (firstImage.Url != null)
        {
            Output.WriteLine($"Generated image URL: {firstImage.Url}");
        }
        else if (firstImage.B64Json != null)
        {
            Output.WriteLine($"Generated image as base64 data (length: {firstImage.B64Json.Length} characters)");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GenerateImageSimpleAsync_WithValidRequest_ShouldReturnImage()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new SimpleGenerateImageRequest
        {
            Prompt = "A majestic mountain landscape with snow-capped peaks",
            Model = "flux-dev",
            Size = "1024x1024",
            Quality = "standard",
            N = 1
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.GenerateImageSimpleAsync(request, CancellationToken.None),
            "GenerateImageSimpleAsync_WithValidRequest"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        
        // Check if we have either URL or base64 data
        var firstImage = response.Data[0];
        (firstImage.Url != null || firstImage.B64Json != null).ShouldBeTrue("Image should have either URL or base64 data");

        if (firstImage.Url != null)
        {
            Output.WriteLine($"Generated simple image URL: {firstImage.Url}");
        }
        else if (firstImage.B64Json != null)
        {
            Output.WriteLine($"Generated simple image as base64 data (length: {firstImage.B64Json.Length} characters)");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GetImageStylesAsync_ShouldReturnStyles()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.GetImageStylesAsync(CancellationToken.None),
            "GetImageStylesAsync"
        );

        // Assert - Allow empty styles as it may be endpoint-specific
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Styles.ShouldNotBeNull();

        if (response.Styles.Any())
        {
            Output.WriteLine($"Available styles: {response.Styles.Count}");
            foreach (var style in response.Styles.Take(5))
            {
                Output.WriteLine($"- {style}");
            }
        }
        else
        {
            Output.WriteLine("No styles returned - this may be expected for this endpoint");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task GenerateImageAsync_WithStylePreset_ShouldReturnImage()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new GenerateImageRequest
        {
            Model = "flux-dev",
            Prompt = "A cyberpunk cityscape at night with neon lights",
            Width = 1024,
            Height = 1024,
            StylePreset = "Neon Punk",
            Format = "png"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.GenerateImageAsync(request, CancellationToken.None),
            "GenerateImageAsync_WithStylePreset"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        
        // Check if we have either URL or base64 data
        var firstImage = response.Data[0];
        (firstImage.Url != null || firstImage.B64Json != null).ShouldBeTrue("Image should have either URL or base64 data");

        if (firstImage.Url != null)
        {
            Output.WriteLine($"Generated styled image URL: {firstImage.Url}");
        }
        else if (firstImage.B64Json != null)
        {
            Output.WriteLine($"Generated styled image as base64 data (length: {firstImage.B64Json.Length} characters)");
        }
        
        Output.WriteLine($"Style preset used: {request.StylePreset}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task GenerateImageAsync_WithNegativePrompt_ShouldReturnImage()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new GenerateImageRequest
        {
            Model = "flux-dev",
            Prompt = "A peaceful forest scene with wildlife",
            NegativePrompt = "dark, scary, horror, violence",
            Width = 1024,
            Height = 1024,
            Format = "png"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.GenerateImageAsync(request, CancellationToken.None),
            "GenerateImageAsync_WithNegativePrompt"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        
        // Check if we have either URL or base64 data
        var firstImage = response.Data[0];
        (firstImage.Url != null || firstImage.B64Json != null).ShouldBeTrue("Image should have either URL or base64 data");

        if (firstImage.Url != null)
        {
            Output.WriteLine($"Generated image with negative prompt: {firstImage.Url}");
        }
        else if (firstImage.B64Json != null)
        {
            Output.WriteLine($"Generated image with negative prompt as base64 data (length: {firstImage.B64Json.Length} characters)");
        }
        
        Output.WriteLine($"Negative prompt: {request.NegativePrompt}");

        await VerifyResult(response);
    }
}
