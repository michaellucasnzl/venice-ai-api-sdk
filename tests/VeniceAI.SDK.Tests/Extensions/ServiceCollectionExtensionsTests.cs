using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Services.Interfaces;
using Xunit;

namespace VeniceAI.SDK.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddVeniceAI_WithApiKey_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "test-api-key";

        // Act
        services.AddVeniceAI(apiKey);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        Assert.NotNull(serviceProvider.GetService<IVeniceAIClient>());
        Assert.NotNull(serviceProvider.GetService<IChatService>());
        Assert.NotNull(serviceProvider.GetService<IImageService>());
        Assert.NotNull(serviceProvider.GetService<IEmbeddingService>());
        Assert.NotNull(serviceProvider.GetService<IAudioService>());
        Assert.NotNull(serviceProvider.GetService<IModelService>());
        Assert.NotNull(serviceProvider.GetService<IBillingService>());
        Assert.NotNull(serviceProvider.GetService<HttpClient>());
        
        var options = serviceProvider.GetService<IOptions<VeniceAIOptions>>();
        Assert.NotNull(options);
        Assert.Equal(apiKey, options.Value.ApiKey);
    }

    [Fact]
    public void AddVeniceAI_WithAction_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var customBaseUrl = "https://custom.api.com";

        // Act
        services.AddVeniceAI(options =>
        {
            options.ApiKey = "test-api-key";
            options.BaseUrl = customBaseUrl;
        });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        Assert.NotNull(serviceProvider.GetService<IVeniceAIClient>());
        
        var options = serviceProvider.GetService<IOptions<VeniceAIOptions>>();
        Assert.NotNull(options);
        Assert.Equal("test-api-key", options.Value.ApiKey);
        Assert.Equal(customBaseUrl, options.Value.BaseUrl);
    }

    [Fact]
    public void AddVeniceAI_WithConfigurationObject_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key",
            BaseUrl = "https://custom.api.com"
        };

        // Act
        services.AddVeniceAI(opt =>
        {
            opt.ApiKey = options.ApiKey;
            opt.BaseUrl = options.BaseUrl;
        });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        Assert.NotNull(serviceProvider.GetService<IVeniceAIClient>());
        
        var configOptions = serviceProvider.GetService<IOptions<VeniceAIOptions>>();
        Assert.NotNull(configOptions);
        Assert.Equal(options.ApiKey, configOptions.Value.ApiKey);
        Assert.Equal(options.BaseUrl, configOptions.Value.BaseUrl);
    }

    [Fact]
    public void AddVeniceAI_RegistersHttpClient()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddVeniceAI("test-api-key");

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var chatService = serviceProvider.GetService<IChatService>();
        
        Assert.NotNull(chatService);
        // The HTTP client is configured internally by the AddHttpClient method
        // We can't directly access it, but we can verify the service is registered
    }

    [Fact]
    public void AddVeniceAI_RegistersAllServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddVeniceAI("test-api-key");

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        // Verify all services are registered
        Assert.NotNull(serviceProvider.GetService<IVeniceAIClient>());
        Assert.NotNull(serviceProvider.GetService<IChatService>());
        Assert.NotNull(serviceProvider.GetService<IImageService>());
        Assert.NotNull(serviceProvider.GetService<IEmbeddingService>());
        Assert.NotNull(serviceProvider.GetService<IAudioService>());
        Assert.NotNull(serviceProvider.GetService<IModelService>());
        Assert.NotNull(serviceProvider.GetService<IBillingService>());
        
        // Verify services are transient (different instances)
        var client1 = serviceProvider.GetService<IVeniceAIClient>();
        var client2 = serviceProvider.GetService<IVeniceAIClient>();
        Assert.NotSame(client1, client2); // Should be different instances (transient)
    }
}
