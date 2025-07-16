using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK.Extensions;

// Quick test to get available models
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddUserSecrets(typeof(Program).Assembly);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddVeniceAI(context.Configuration);
    })
    .Build();

var client = host.Services.GetRequiredService<IVeniceAIClient>();

Console.WriteLine("Fetching available models...");
var models = await client.Models.GetModelsAsync();

Console.WriteLine("\nAvailable Models:");
foreach (var model in models.Models)
{
    Console.WriteLine($"- {model.Id} (Type: {model.Type})");
}
