using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Hosting;
using VeniceAI.SDK.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Logging;

public class SerilogIntegrationTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly IHost _host;
    private readonly ILogger<SerilogIntegrationTests> _logger;

    public SerilogIntegrationTests(ITestOutputHelper output)
    {
        _output = output;

        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
            })
            .UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .WriteTo.TestOutput(output);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddVeniceAI(context.Configuration);
            });

        _host = hostBuilder.Build();
        _logger = _host.Services.GetRequiredService<ILogger<SerilogIntegrationTests>>();
    }

    [Fact]
    public void Logger_Should_BeResolved_FromDependencyInjection()
    {
        // Arrange & Act
        var logger = _host.Services.GetService<ILogger<SerilogIntegrationTests>>();

        // Assert
        Assert.NotNull(logger);

        // Test logging
        logger.LogInformation("Test log message from Serilog integration test");
        logger.LogDebug("Debug level message");
        logger.LogWarning("Warning level message");
    }

    [Fact]
    public void VeniceAI_Services_Should_UseLogger()
    {
        // Arrange
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["VeniceAI:ApiKey"] = "test-key-for-logging-test"
                });
            })
            .UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .WriteTo.TestOutput(_output);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddVeniceAI(context.Configuration);
            });

        using var host = hostBuilder.Build();
        var client = host.Services.GetRequiredService<IVeniceAIClient>();

        // Act & Assert
        Assert.NotNull(client);
        Assert.NotNull(client.Chat);
        Assert.NotNull(client.Models);
        Assert.NotNull(client.Images);
        Assert.NotNull(client.Audio);
        Assert.NotNull(client.Embeddings);
        Assert.NotNull(client.Billing);

        _logger.LogInformation("Venice AI client services successfully resolved with Serilog integration");
    }

    [Fact]
    public void Serilog_Configuration_Should_WriteToFile()
    {
        // Arrange
        var logFilePath = Path.Combine("logs", $"integration-tests-{DateTime.Now:yyyy-MM-dd}.log");

        // Act
        _logger.LogInformation("Test message for file logging verification");

        // Give a moment for async writing
        Thread.Sleep(100);

        // Assert
        Assert.True(Directory.Exists("logs"), "Logs directory should exist");

        // Check if any log files exist (they might have different timestamps)
        var logFiles = Directory.GetFiles("logs", "integration-tests-*.log");
        Assert.NotEmpty(logFiles);

        _logger.LogInformation("Log file verification completed. Found {LogFileCount} log files", logFiles.Length);
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
