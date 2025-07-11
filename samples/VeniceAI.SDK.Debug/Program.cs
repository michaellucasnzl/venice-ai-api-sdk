using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK.Extensions;

namespace VeniceAI.SDK.Debug;

/// <summary>
/// Debug application to test HTTP client configuration.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Venice AI SDK Debug - HTTP Client Configuration");
        Console.WriteLine("================================================");
        
        try
        {
            // Setup configuration and dependency injection
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddUserSecrets<Program>();
                })
                .ConfigureServices((context, services) =>
                {
                    // Add Venice AI SDK with configuration
                    services.AddVeniceAI(context.Configuration);
                })
                .Build();

            // Get the HTTP client factory and chat service
            var httpClientFactory = host.Services.GetRequiredService<IHttpClientFactory>();
            var chatService = host.Services.GetRequiredService<VeniceAI.SDK.Services.Interfaces.IChatService>();
            
            // Get the HTTP client for the chat service by name
            var httpClient = httpClientFactory.CreateClient("ChatService");
            
            Console.WriteLine($"HTTP Client Base Address: {httpClient.BaseAddress}");
            Console.WriteLine($"HTTP Client Timeout: {httpClient.Timeout}");
            Console.WriteLine("Authorization Header: " + 
                (httpClient.DefaultRequestHeaders.Authorization?.ToString() ?? "Not set"));
            
            // Test the chat service type
            Console.WriteLine($"Chat Service Type: {chatService.GetType()}");
            
            // Also test if we can access the HttpClient from the service
            var serviceType = chatService.GetType();
            var httpClientField = serviceType.BaseType?.GetField("HttpClient", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            if (httpClientField != null)
            {
                var serviceHttpClient = httpClientField.GetValue(chatService) as HttpClient;
                Console.WriteLine($"Service HTTP Client Base Address: {serviceHttpClient?.BaseAddress}");
            }
            
            Console.WriteLine("\nConfiguration appears correct!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack: {ex.StackTrace}");
        }
    }
}
