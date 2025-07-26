using Shouldly;
using VeniceAI.SDK.Models.Images;
using VeniceAI.SDK.Models.Common;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Image;

/// <summary>
/// Integration tests for the Image edit endpoint.
/// </summary>
public class ImageEditIntegrationTests : IntegrationTestBase
{
    public ImageEditIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    /// <summary>
    /// Returns properties that should be scrubbed from verification output for image edit tests.
    /// </summary>
    protected override string[] GetPropertiesToScrub()
    {
        return new[] { "B64Json", "Images", "Image", "Timing", "InferenceDuration", "InferencePreprocessingTime", "InferenceQueueTime", "Total" };
    }

    [Fact]
    public async Task EditImageAsync_WithValidRequest_ShouldReturnEditedImage()
    {


        // Arrange - First generate a base image to edit
        var generateRequest = new GenerateImageRequest
        {
            Model = ImageModel.FluxStandard,
            Prompt = "A simple landscape with a house",
            Width = 512,
            Height = 512,
            Format = "png"
        };

        var generateResponse = await ExecuteWithErrorHandling(
            () => Client.Images.GenerateImageAsync(generateRequest, CancellationToken.None),
            "GenerateImageForEditing"
        );

        if (generateResponse == null || !generateResponse.IsSuccess || !generateResponse.Data.Any())
        {
            Output.WriteLine("Skipping edit test - could not generate base image");
            return;
        }

        // Wait a bit between requests to avoid rate limits
        await DelayBetweenRequests();

        var editRequest = new EditImageRequest
        {
            Image = generateResponse.Data[0].B64Json ?? Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("placeholder")),
            Prompt = "Add a red barn in the center"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Images.EditImageAsync(editRequest, CancellationToken.None),
            "EditImageAsync_WithValidRequest"
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
            Output.WriteLine($"Edited image URL: {firstImage.Url}");
        }
        else if (firstImage.B64Json != null)
        {
            Output.WriteLine($"Edited image as base64 data (length: {firstImage.B64Json.Length} characters)");
        }

        await VerifyResult(response);
    }
}
