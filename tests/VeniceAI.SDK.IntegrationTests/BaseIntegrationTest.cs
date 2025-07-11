using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Base class for integration tests with common setup and Verify configuration.
/// </summary>
public abstract class BaseIntegrationTest : IDisposable
{
    protected readonly IHost _host;
    protected readonly IVeniceAIClient _client;
    protected readonly ITestOutputHelper _output;
    private bool _disposed = false;

    protected BaseIntegrationTest(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<BaseIntegrationTest>();
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

    /// <summary>
    /// Sanitizes response data for consistent verification by removing dynamic values.
    /// </summary>
    /// <param name="obj">The object to sanitize.</param>
    /// <returns>The sanitized object.</returns>
    protected object? SanitizeForVerification(object? obj)
    {
        if (obj == null) return null;
        
        var json = System.Text.Json.JsonSerializer.Serialize(obj, new System.Text.Json.JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
        
        // Replace dynamic values with placeholders
        json = System.Text.RegularExpressions.Regex.Replace(json, @"""id"": ""[^""]*""", @"""id"": ""<ID>""");
        json = System.Text.RegularExpressions.Regex.Replace(json, @"""created"": \d+", @"""created"": 0");
        json = System.Text.RegularExpressions.Regex.Replace(json, @"""usage"": \{[^}]*\}", @"""usage"": ""<USAGE>""");
        json = System.Text.RegularExpressions.Regex.Replace(json, @"""system_fingerprint"": ""[^""]*""", @"""system_fingerprint"": ""<FINGERPRINT>""");
        json = System.Text.RegularExpressions.Regex.Replace(json, @"""model"": ""[^""]*""", @"""model"": ""<MODEL>""");
        json = System.Text.RegularExpressions.Regex.Replace(json, @"""finish_reason"": ""[^""]*""", @"""finish_reason"": ""<FINISH_REASON>""");
        
        return json;
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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
