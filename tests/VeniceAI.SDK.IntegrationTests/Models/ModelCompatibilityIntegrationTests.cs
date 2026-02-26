using Shouldly;
using VeniceAI.SDK.Models.Models;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Models;

/// <summary>
/// Integration tests for the Model compatibility endpoint.
/// </summary>
public class ModelCompatibilityIntegrationTests : IntegrationTestBase
{
    public ModelCompatibilityIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetModelCompatibilityAsync_ShouldReturnCompatibilityMapping()
    {
        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Models.GetModelCompatibilityAsync(CancellationToken.None),
            "GetModelCompatibilityAsync"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Compatibility.ShouldNotBeNull();

        Output.WriteLine($"Model compatibility mappings: {response.Compatibility.Count}");

        foreach (var mapping in response.Compatibility.Take(10))
        {
            Output.WriteLine($"- {mapping.Key}: {mapping.Value}");
        }

        await VerifyResult(response);
    }
}
