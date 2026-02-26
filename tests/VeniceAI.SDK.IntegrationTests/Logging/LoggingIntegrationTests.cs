using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Services.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Logging;

/// <summary>
/// Integration tests to verify the Venice AI SDK works with standard .NET logging.
/// </summary>
public class LoggingIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly ILogger<LoggingIntegrationTests> _logger;

    public LoggingIntegrationTests(ITestOutputHelper output)
    {
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets(GetType().Assembly);
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddVeniceAI(context.Configuration);
            });

        _host = hostBuilder.Build();

        // Get logger instance
        _logger = _host.Services.GetRequiredService<ILogger<LoggingIntegrationTests>>();
    }

    [Fact]
    public void ILogger_Should_BeAvailable()
    {
        // Arrange & Act
        var logger = _host.Services.GetService<ILogger<LoggingIntegrationTests>>();

        // Assert
        Assert.NotNull(logger);

        // Test that logging works
        logger.LogInformation("Test log message from logging integration test");

        // Should not throw
        Assert.True(true);
    }

    [Fact]
    public void VeniceAI_Services_Should_ResolveWithLogging()
    {
        // Arrange & Act
        var veniceClient = _host.Services.GetService<IVeniceAIClient>();
        var chatService = _host.Services.GetService<IChatService>();
        var imageService = _host.Services.GetService<IImageService>();
        var audioService = _host.Services.GetService<IAudioService>();
        var embeddingService = _host.Services.GetService<IEmbeddingService>();
        var modelService = _host.Services.GetService<IModelService>();
        var billingService = _host.Services.GetService<IBillingService>();

        // Assert
        Assert.NotNull(veniceClient);
        Assert.NotNull(chatService);
        Assert.NotNull(imageService);
        Assert.NotNull(audioService);
        Assert.NotNull(embeddingService);
        Assert.NotNull(modelService);
        Assert.NotNull(billingService);

        _logger.LogInformation("Venice AI client services successfully resolved with standard .NET logging integration");
    }

    [Fact]
    public void Logging_Configuration_Should_Work()
    {
        // Arrange
        var loggerFactory = _host.Services.GetRequiredService<ILoggerFactory>();
        var testLogger = loggerFactory.CreateLogger("TestLogger");

        // Act & Assert
        Assert.NotNull(testLogger);

        // Test that we can log at different levels
        testLogger.LogTrace("Trace message");
        testLogger.LogDebug("Debug message");
        testLogger.LogInformation("Information message");
        testLogger.LogWarning("Warning message");
        testLogger.LogError("Error message");
        testLogger.LogCritical("Critical message");

        // Should not throw
        Assert.True(true);
    }

    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _host?.Dispose();
            }
            _disposed = true;
        }
    }
}
