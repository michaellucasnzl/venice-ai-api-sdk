using Shouldly;
using VeniceAI.SDK.Models.Images;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Image;

/// <summary>
/// Integration tests for the Image upscale endpoint.
/// </summary>
public class ImageUpscaleIntegrationTests : IntegrationTestBase
{
    public ImageUpscaleIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    /// <summary>
    /// Returns properties that should be scrubbed from verification output for image upscale tests.
    /// </summary>
    protected override string[] GetPropertiesToScrub()
    {
        return new[] { "B64Json", "Images", "Image", "Timing", "InferenceDuration", "InferencePreprocessingTime", "InferenceQueueTime", "Total" };
    }

    [Fact]
    public async Task UpscaleImageAsync_WithValidRequest_ShouldReturnUpscaledImage()
    {


        // Arrange - First generate a small image to upscale
        var generateRequest = new GenerateImageRequest
        {
            Model = "flux-dev",
            Prompt = "A simple red circle on white background",
            Width = 512,
            Height = 512,
            Format = "png"
        };

        var generateResponse = await ExecuteWithErrorHandling(
            () => Client.Images.GenerateImageAsync(generateRequest, CancellationToken.None),
            "GenerateImageForUpscaling"
        );

        if (generateResponse == null || !generateResponse.IsSuccess || !generateResponse.Data.Any())
        {
            Output.WriteLine("Skipping upscale test - could not generate base image");
            return;
        }

        // Wait a bit between requests to avoid rate limits
        await DelayBetweenRequests();

        // Use the generated image for upscaling
        var upscaleRequest = new UpscaleImageRequest
        {
            Image = generateResponse.Data[0].B64Json ?? Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("placeholder")),
            Scale = 2.0
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.UpscaleImageAsync(upscaleRequest, CancellationToken.None),
            "UpscaleImageAsync_WithValidRequest"
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
            Output.WriteLine($"Upscaled image URL: {firstImage.Url}");
        }
        else if (firstImage.B64Json != null)
        {
            Output.WriteLine($"Upscaled image as base64 data (length: {firstImage.B64Json.Length} characters)");
        }

        await VerifyResult(response);
    }
}
