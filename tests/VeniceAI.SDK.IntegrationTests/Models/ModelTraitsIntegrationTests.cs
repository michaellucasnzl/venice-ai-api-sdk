using Shouldly;
using VeniceAI.SDK.Models.Models;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Models;

/// <summary>
/// Integration tests for the Model traits endpoint.
/// </summary>
public class ModelTraitsIntegrationTests : IntegrationTestBase
{
    public ModelTraitsIntegrationTests(ITestOutputHelper output) : base(output)
    {
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

        Output.WriteLine($"Model traits: {response.Traits.Count}");

        foreach (var trait in response.Traits.Take(10))
        {
            Output.WriteLine($"- {trait.Key}: {trait.Value}");
        }

        await VerifyResult(response);
    }
}
