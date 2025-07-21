using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Configuration;

namespace VeniceAI.SDK.HttpClientExamples;

static class Program
{
    private const string ExampleApiUrl = "https://api.myservice.com/";
    private const string UserAgentHeader = "User-Agent";

    static async Task Main(string[] args)
    {
        Console.WriteLine("🔧 Venice AI SDK - HttpClient Separation Examples");
        Console.WriteLine("=================================================");

        var apiKey = Environment.GetEnvironmentVariable("VeniceAI__ApiKey") ?? "your-api-key-here";

        if (apiKey == "your-api-key-here")
        {
            Console.WriteLine("Please set the VeniceAI__ApiKey environment variable");
            return;
        }

        // Example 1: Default HttpClient (simplest approach)
        Console.WriteLine("\n1️⃣ Default HttpClient Registration:");
        await Example1_DefaultHttpClient(apiKey);

        // Example 2: Custom HttpClient configuration
        Console.WriteLine("\n2️⃣ Custom HttpClient Configuration:");
        await Example2_CustomHttpClientConfiguration(apiKey);

        // Example 3: Providing your own HttpClient instance
        Console.WriteLine("\n3️⃣ Providing Your Own HttpClient:");
        await Example3_ProvidedHttpClient(apiKey);

        // Example 4: Multiple HttpClients with different configurations
        Console.WriteLine("\n4️⃣ Multiple HttpClients with Different Configurations:");
        await Example4_MultipleHttpClients(apiKey);

        Console.WriteLine("\n✅ All examples completed successfully!");
    }

    private static async Task Example1_DefaultHttpClient(string apiKey)
    {
        // This is the simplest approach - SDK manages its own HttpClient
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddConsole());
        services.AddVeniceAI(apiKey);

        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetRequiredService<IVeniceAIClient>();

        await MakeTestCall(client, "Default HttpClient");
    }

    private static async Task Example2_CustomHttpClientConfiguration(string apiKey)
    {
        // Custom configuration for the Venice AI HttpClient
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddConsole());

        services.AddVeniceAI(apiKey, httpClient =>
        {
            // Custom configuration for Venice AI's HttpClient
            httpClient.Timeout = TimeSpan.FromSeconds(60); // Shorter timeout
            httpClient.DefaultRequestHeaders.Add(UserAgentHeader, "MyApp/1.0");
            httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "Venice-AI-Client");
            // Note: SDK automatically sets BaseAddress and Authorization
        });

        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetRequiredService<IVeniceAIClient>();

        await MakeTestCall(client, "Custom HttpClient Configuration");
    }

    private static async Task Example3_ProvidedHttpClient(string apiKey)
    {
        // You provide your own HttpClient instance
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddConsole());

        // Create and configure your own HttpClient
        // Note: You typically don't need to set BaseAddress as the SDK handles this automatically
        var customHttpClient = new HttpClient();
        customHttpClient.Timeout = TimeSpan.FromMinutes(10); // Very long timeout
        customHttpClient.DefaultRequestHeaders.Add("User-Agent", "MyCustomClient/2.0");

        // The SDK will automatically configure the BaseAddress for Venice AI API

        services.AddVeniceAI(apiKey, customHttpClient);

        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetRequiredService<IVeniceAIClient>();

        await MakeTestCall(client, "Provided HttpClient");
    }

    private static async Task Example4_MultipleHttpClients(string apiKey)
    {
        // Scenario: You have your own HttpClient for other APIs, and Venice AI has its own
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddConsole());

        // Your own HttpClient for other services
        services.AddHttpClient("MyApiClient", client =>
        {
            client.BaseAddress = new Uri(ExampleApiUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        // Venice AI HttpClient with its own configuration
        services.AddVeniceAI(apiKey, httpClient =>
        {
            httpClient.Timeout = TimeSpan.FromMinutes(5); // AI requests can take longer
            httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp-AI/1.0");
        });

        var serviceProvider = services.BuildServiceProvider();

        // Use your own HttpClient for other services
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var myApiClient = httpClientFactory.CreateClient("MyApiClient");
        Console.WriteLine($"   My API Client timeout: {myApiClient.Timeout}");

        // Use Venice AI client
        var veniceClient = serviceProvider.GetRequiredService<IVeniceAIClient>();
        await MakeTestCall(veniceClient, "Multiple HttpClients");
    }

    private static async Task MakeTestCall(IVeniceAIClient client, string scenario)
    {
        try
        {
            var request = new ChatCompletionRequest
            {
                Model = "llama-3.3-70b",
                Messages = new List<ChatMessage>
                {
                    new UserMessage($"Say 'Hello from {scenario}!' in exactly those words.")
                },
                MaxTokens = 50,
                Temperature = 0.1
            };

            var response = await client.Chat.CreateChatCompletionAsync(request);
            if (response.IsSuccess && response.Choices.Count > 0)
            {
                Console.WriteLine($"   ✅ {response.Choices[0].Message.Content}");
            }
            else
            {
                Console.WriteLine($"   ❌ Failed to get response");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Error: {ex.Message}");
        }
    }
}
