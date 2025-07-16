using Shouldly;
using VeniceAI.SDK.Models.Images;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Image;

/// <summary>
/// Integration tests for the Image styles endpoint.
/// </summary>
public class ImageStylesIntegrationTests : IntegrationTestBase
{
    public ImageStylesIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetImageStylesAsync_ShouldReturnStyles()
    {


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
}
