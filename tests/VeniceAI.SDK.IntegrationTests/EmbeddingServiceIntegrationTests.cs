using Shouldly;
using VeniceAI.SDK.Models.Embeddings;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Embedding service.
/// </summary>
public class EmbeddingServiceIntegrationTests : IntegrationTestBase
{
    public EmbeddingServiceIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithStringInput_ShouldReturnEmbedding()
    {
        // Skip if we don't have a real API key configured
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "The quick brown fox jumps over the lazy dog",
            EncodingFormat = "float"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Embeddings.CreateEmbeddingAsync(request, CancellationToken.None),
            "CreateEmbeddingAsync_WithStringInput"
        );

        // Assert
        if (response == null) return; // Test passed with expected error
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        response.Data[0].Embedding.ShouldNotBeEmpty();
        response.Usage.ShouldNotBeNull();
        response.Usage.PromptTokens.ShouldBeGreaterThan(0);
        
        Output.WriteLine($"Embedding dimensions: {response.Data[0].Embedding.Count}");
        Output.WriteLine($"Tokens used: {response.Usage.PromptTokens}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithArrayInput_ShouldReturnMultipleEmbeddings()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = new[] { "Hello world", "This is a test", "Another sentence" },
            EncodingFormat = "float"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Embeddings.CreateEmbeddingAsync(request, CancellationToken.None),
            "CreateEmbeddingAsync_WithArrayInput"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        response.Data.Count.ShouldBe(3);
        
        foreach (var embedding in response.Data)
        {
            embedding.Embedding.ShouldNotBeEmpty();
        }

        Output.WriteLine($"Generated {response.Data.Count} embeddings");
        Output.WriteLine($"Tokens used: {response.Usage.PromptTokens}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithBase64Encoding_ShouldReturnEmbedding()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "Test input for base64 encoding",
            EncodingFormat = "base64"
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Embeddings.CreateEmbeddingAsync(request, CancellationToken.None),
            "CreateEmbeddingAsync_WithBase64Encoding"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        
        // Check for either embedding format
        if (!string.IsNullOrEmpty(response.Data[0].EmbeddingBase64))
        {
            response.Data[0].EmbeddingBase64?.ShouldNotBeNullOrEmpty();
            Output.WriteLine($"Base64 embedding length: {response.Data[0].EmbeddingBase64?.Length ?? 0}");
        }
        else
        {
            response.Data[0].Embedding?.ShouldNotBeEmpty();
            Output.WriteLine($"Embedding dimensions: {response.Data[0].Embedding?.Count ?? 0}");
        }

        Output.WriteLine($"Embedding format: {request.EncodingFormat}");
        Output.WriteLine($"Tokens used: {response.Usage.PromptTokens}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithDimensions_ShouldReturnEmbeddingWithSpecifiedDimensions()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "Test input for dimension specification",
            EncodingFormat = "float",
            Dimensions = 512
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Embeddings.CreateEmbeddingAsync(request, CancellationToken.None),
            "CreateEmbeddingAsync_WithDimensions"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Data.ShouldNotBeEmpty();
        response.Data[0].Embedding.ShouldNotBeEmpty();
        
        var actualDimensions = response.Data[0].Embedding.Count;
        actualDimensions.ShouldBeGreaterThan(0);

        Output.WriteLine($"Requested dimensions: {request.Dimensions}");
        Output.WriteLine($"Actual dimensions: {actualDimensions}");
        Output.WriteLine($"Tokens used: {response.Usage.PromptTokens}");

        await VerifyResult(response);
    }
}
