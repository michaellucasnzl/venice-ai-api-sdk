using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Extensions;
using Xunit;

namespace VeniceAI.SDK.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    private readonly VerifySettings _verifySettings = new();
    private readonly IHost _host;

    protected readonly IVeniceAIClient Client;
    protected readonly ITestOutputHelper Output;

    protected IntegrationTestBase(ITestOutputHelper output)
    {
        Output = output;
        ConfigureVerify();

        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets(GetType().Assembly);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Warning);
                });

                services.AddVeniceAI(context.Configuration);
            });

        _host = hostBuilder.Build();
        Client = _host.Services.GetRequiredService<IVeniceAIClient>();
    }

    protected async Task VerifyResult(object obj, [CallerFilePath] string sourceFilePath = "")
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        await Verify(obj, _verifySettings, sourceFilePath);
    }

    protected async Task VerifyResult(object obj, string suffix, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string methodName = "")
    {
        var settings = new VerifySettings(_verifySettings);
        settings.UseMethodName($"{methodName}_{suffix}");
        // ReSharper disable once ExplicitCallerInfoArgument
        await Verify(obj, settings, sourceFilePath);
    }

    private void ConfigureVerify()
    {
        // Configure Verify to scrub fields that could change between test runs
        _verifySettings.ScrubMembers(x => x.Name.Contains("Date"));
        _verifySettings.ScrubMembers(x => x.Name.Contains("Time"));
        _verifySettings.ScrubMembers(x => x.Name.Contains("Utc"));
        
        // Scrub large binary/base64 content fields to prevent huge test output files
        _verifySettings.ScrubMember("AudioContent");     // Audio data as byte array
        _verifySettings.ScrubMember("B64Json");          // Base64-encoded image data
        _verifySettings.ScrubMember("Images");           // Base64-encoded images list
        _verifySettings.ScrubMember("Embedding");        // Large embedding vectors
        _verifySettings.ScrubMember("EmbeddingBase64");  // Base64-encoded embeddings
        _verifySettings.ScrubMember("RawContent");       // Raw API response content

        _verifySettings.AutoVerify(true, true);
    }

    public virtual void Dispose()
    {
        _host?.Dispose();
    }
}