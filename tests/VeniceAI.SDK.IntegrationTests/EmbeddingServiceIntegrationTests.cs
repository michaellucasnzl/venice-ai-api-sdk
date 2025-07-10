using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Embeddings;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Embedding service.
/// </summary>
public class EmbeddingServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly IVeniceAIClient _client;
    private readonly ITestOutputHelper _output;

    public EmbeddingServiceIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<EmbeddingServiceIntegrationTests>();
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
    public async Task CreateEmbeddingAsync_WithStringInput_ShouldReturnEmbedding()
    {
        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "The quick brown fox jumps over the lazy dog",
            EncodingFormat = "float"
        };

        // Act
        var response = await _client.Embeddings.CreateEmbeddingAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].Embedding.Should().NotBeEmpty();
        response.Usage.Should().NotBeNull();
        response.Usage.PromptTokens.Should().BeGreaterThan(0);
        
        _output.WriteLine($"Embedding dimensions: {response.Data[0].Embedding.Count}");
        _output.WriteLine($"Tokens used: {response.Usage.PromptTokens}");
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithArrayInput_ShouldReturnMultipleEmbeddings()
    {
        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = new[]
            {
                "This is the first text to embed",
                "This is the second text to embed",
                "This is the third text to embed"
            },
            EncodingFormat = "float"
        };

        // Act
        var response = await _client.Embeddings.CreateEmbeddingAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().HaveCount(3);
        response.Data.All(d => d.Embedding.Count > 0).Should().BeTrue();
        response.Usage.Should().NotBeNull();
        response.Usage.PromptTokens.Should().BeGreaterThan(0);
        
        _output.WriteLine($"Generated {response.Data.Count} embeddings");
        _output.WriteLine($"Embedding dimensions: {response.Data[0].Embedding.Count}");
        _output.WriteLine($"Total tokens used: {response.Usage.PromptTokens}");
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithBase64Encoding_ShouldReturnEmbedding()
    {
        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "Venice AI is a powerful artificial intelligence platform",
            EncodingFormat = "base64"
        };

        // Act
        var response = await _client.Embeddings.CreateEmbeddingAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].Embedding.Should().NotBeEmpty();
        
        _output.WriteLine($"Base64 embedding dimensions: {response.Data[0].Embedding.Count}");
        _output.WriteLine($"Model used: {response.Model}");
    }

    [Fact]
    public async Task CreateEmbeddingAsync_WithDimensions_ShouldReturnEmbeddingWithSpecifiedDimensions()
    {
        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "Test text for embedding with specific dimensions",
            EncodingFormat = "float",
            Dimensions = 512
        };

        // Act
        var response = await _client.Embeddings.CreateEmbeddingAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data[0].Embedding.Should().HaveCount(512);
        
        _output.WriteLine($"Requested dimensions: {request.Dimensions}");
        _output.WriteLine($"Actual embedding dimensions: {response.Data[0].Embedding.Count}");
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
