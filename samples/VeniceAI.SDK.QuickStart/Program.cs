using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;

namespace VeniceAI.SDK.QuickStart;

/// <summary>
/// Quick start example for the Venice AI SDK.
/// This demonstrates how to set up and use the SDK with user secrets.
/// </summary>
public static class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Venice AI SDK Quick Start Example");
        Console.WriteLine("==================================");
        
        try
        {
            // Setup configuration and dependency injection
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // This will automatically load user secrets
                    config.AddUserSecrets(typeof(Program).Assembly);
                })
                .ConfigureServices((context, services) =>
                {
                    // Add Venice AI SDK with configuration
                    services.AddVeniceAI(context.Configuration);
                })
                .Build();

            // Get the Venice AI client
            var client = host.Services.GetRequiredService<IVeniceAIClient>();

            // Test the chat functionality
            var chatRequest = new ChatCompletionRequest
            {
                Model = "llama-3.3-70b",
                Messages = new List<ChatMessage>
                {
                    new UserMessage("Hello! Can you tell me a short joke?")
                },
                MaxTokens = 100
            };

            Console.WriteLine("Sending chat request...");
            var response = await client.Chat.CreateChatCompletionAsync(chatRequest);
            
            Console.WriteLine($"Response: {response.Choices[0].Message.Content}");
            Console.WriteLine($"Model: {response.Model}");
            Console.WriteLine($"Tokens used: {response.Usage?.TotalTokens ?? 0}");
            
            Console.WriteLine("\nSDK is working correctly!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("\nMake sure you have set your API key using:");
            Console.WriteLine("dotnet user-secrets set \"VeniceAI:ApiKey\" \"your-api-key-here\"");
        }
    }
}
