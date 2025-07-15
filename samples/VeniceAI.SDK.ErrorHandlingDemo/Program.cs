using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;

namespace VeniceAI.SDK.ErrorHandlingDemo;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Venice AI SDK - Improved Error Handling Demo");
        Console.WriteLine("===========================================");

        // Setup configuration and dependency injection
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // For demo purposes, we'll use an invalid API key
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["VeniceAI:ApiKey"] = "invalid-api-key-demo",
                    ["VeniceAI:BaseUrl"] = "https://api.venice.ai/api/v1"
                });
            })
            .ConfigureServices((context, services) =>
            {
                // Add Venice AI SDK with configuration
                services.AddVeniceAI(context.Configuration);
            })
            .Build();

        // Get the Venice AI client
        var client = host.Services.GetRequiredService<IVeniceAIClient>();

        // Demo 1: Authentication error
        Console.WriteLine("\n1. Testing authentication error:");
        Console.WriteLine("   Using invalid API key...");

        try
        {
            var request = new ChatCompletionRequest
            {
                Model = "llama-3.3-70b",
                Messages = new List<ChatMessage>
                {
                    new UserMessage("Hello, how are you?")
                }
            };

            var response = await client.Chat.CreateChatCompletionAsync(request);
            Console.WriteLine($"   Response: {response.Choices[0].Message.Content}");
        }
        catch (VeniceAIException ex)
        {
            Console.WriteLine($"   ✓ Caught VeniceAIException:");
            Console.WriteLine($"     Status Code: {ex.StatusCode}");
            Console.WriteLine($"     Error Code: {ex.ErrorCode}");
            Console.WriteLine($"     Message: {ex.Message}");

            if (ex.ValidationErrors?.Count > 0)
            {
                Console.WriteLine($"     Validation Errors:");
                foreach (var error in ex.ValidationErrors)
                {
                    Console.WriteLine($"       - {error.Key}: {string.Join(", ", error.Value)}");
                }
            }
        }

        // Demo 2: Validation error (simulated)
        Console.WriteLine("\n2. Testing validation error:");
        Console.WriteLine("   This would normally happen with invalid parameters...");

        // Create a validation error manually for demonstration
        var validationErrorJson = """
            {
                "_errors": [],
                "seed": {
                    "_errors": ["Number must be greater than 0"]
                },
                "temperature": {
                    "_errors": ["Temperature must be between 0 and 2"]
                }
            }
            """;

        var validationException = VeniceAIException.FromApiException(
            new VeniceAI.SDK.Generated.ApiException(
                "Validation failed",
                400,
                validationErrorJson,
                new Dictionary<string, IEnumerable<string>>(),
                null));

        Console.WriteLine($"   ✓ Simulated VeniceAIException:");
        Console.WriteLine($"     Status Code: {validationException.StatusCode}");
        Console.WriteLine($"     Message: {validationException.Message}");

        if (validationException.ValidationErrors?.Count > 0)
        {
            Console.WriteLine($"     Validation Errors:");
            foreach (var error in validationException.ValidationErrors)
            {
                Console.WriteLine($"       - {error.Key}: {string.Join(", ", error.Value)}");
            }
        }

        Console.WriteLine("\n===========================================");
        Console.WriteLine("Demo completed. The SDK now provides much better error handling!");
        Console.WriteLine("Users will receive clear, actionable error messages instead of");
        Console.WriteLine("raw JSON responses that are difficult to consume.");
    }
}
