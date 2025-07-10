using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Services.Interfaces;
using Xunit;

namespace VeniceAI.SDK.Tests.Client;

public class VeniceAIClientTests
{
    [Fact]
    public void Constructor_WithServices_SetsProperties()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddVeniceAI("test-api-key");
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var client = serviceProvider.GetService<IVeniceAIClient>();

        // Assert
        Assert.NotNull(client);
        Assert.NotNull(client.Chat);
        Assert.NotNull(client.Images);
        Assert.NotNull(client.Embeddings);
        Assert.NotNull(client.Audio);
        Assert.NotNull(client.Models);
        Assert.NotNull(client.Billing);
    }

    [Fact]
    public void Services_AreNotNull()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddVeniceAI("test-api-key");
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<IVeniceAIClient>();

        // Assert
        Assert.NotNull(client);
        Assert.NotNull(client.Chat);
        Assert.NotNull(client.Images);
        Assert.NotNull(client.Embeddings);
        Assert.NotNull(client.Audio);
        Assert.NotNull(client.Models);
        Assert.NotNull(client.Billing);
    }

    [Fact]
    public void Services_AreCorrectTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddVeniceAI("test-api-key");
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<IVeniceAIClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsAssignableFrom<IChatService>(client.Chat);
        Assert.IsAssignableFrom<IImageService>(client.Images);
        Assert.IsAssignableFrom<IEmbeddingService>(client.Embeddings);
        Assert.IsAssignableFrom<IAudioService>(client.Audio);
        Assert.IsAssignableFrom<IModelService>(client.Models);
        Assert.IsAssignableFrom<IBillingService>(client.Billing);
    }

    [Fact]
    public void Client_IsTransient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddVeniceAI("test-api-key");
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var client1 = serviceProvider.GetService<IVeniceAIClient>();
        var client2 = serviceProvider.GetService<IVeniceAIClient>();

        // Assert
        Assert.NotSame(client1, client2);
    }

    [Fact]
    public void Client_WithCustomConfiguration_UsesCorrectSettings()
    {
        // Arrange
        var services = new ServiceCollection();
        var customBaseUrl = "https://custom.api.com";
        services.AddVeniceAI(options =>
        {
            options.ApiKey = "test-api-key";
            options.BaseUrl = customBaseUrl;
            options.TimeoutSeconds = 300;
        });
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var client = serviceProvider.GetService<IVeniceAIClient>();
        var options = serviceProvider.GetService<IOptions<VeniceAIOptions>>();

        // Assert
        Assert.NotNull(client);
        Assert.NotNull(options);
        Assert.Equal("test-api-key", options.Value.ApiKey);
        Assert.Equal(customBaseUrl, options.Value.BaseUrl);
        Assert.Equal(300, options.Value.TimeoutSeconds);
    }
}
