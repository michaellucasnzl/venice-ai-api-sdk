using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;

namespace VeniceAI.SDK.Quickstart;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🚀 Venice AI SDK Quickstart");
        Console.WriteLine("===========================");

        // Load configuration
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();

        // Get API key from configuration
        var apiKey = config["VeniceAI:ApiKey"] ??
                     config["VENICE_AI_API_KEY"] ??
                     Environment.GetEnvironmentVariable("VeniceAI__ApiKey");

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("❌ API key not found!");
            Console.WriteLine("Please set your API key using one of these methods:");
            Console.WriteLine("1. User secrets: dotnet user-secrets set \"VeniceAI:ApiKey\" \"your-api-key\"");
            Console.WriteLine("2. Environment variable: set VeniceAI__ApiKey=your-api-key");
            Console.WriteLine("3. appsettings.json: { \"VeniceAI\": { \"ApiKey\": \"your-api-key\" } }");
            return;
        }

        // Create host with dependency injection
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddVeniceAI(apiKey);
            })
            .Build();

        // Get the Venice AI client from DI
        var client = host.Services.GetRequiredService<IVeniceAIClient>();

        try
        {
            // Example 1: List available models
            Console.WriteLine("\n📋 Available Models:");
            Console.WriteLine("===================");

            var modelsResponse = await client.Models.GetModelsAsync();
            var topModels = modelsResponse.Data.Take(5);

            foreach (var model in topModels)
            {
                Console.WriteLine($"• {model.Id}");
                Console.WriteLine($"  Type: {model.Type}");
                Console.WriteLine($"  Context: {model.ModelSpec.AvailableContextTokens:N0} tokens");
                Console.WriteLine($"  Vision: {(model.ModelSpec.Capabilities?.SupportsVision == true ? "✓" : "✗")}");
                Console.WriteLine($"  Functions: {(model.ModelSpec.Capabilities?.SupportsFunctionCalling == true ? "✓" : "✗")}");
                Console.WriteLine();
            }

            // Example 2: Simple chat completion
            Console.WriteLine("💬 Chat Completion Example:");
            Console.WriteLine("============================");

            var chatRequest = new ChatCompletionRequest
            {
                Model = "llama-3.3-70b",
                Messages = new List<ChatMessage>
                {
                    new SystemMessage("You are a helpful assistant that provides concise, friendly responses."),
                    new UserMessage("Hello! Can you tell me what Venice AI is in 2-3 sentences?")
                },
                MaxTokens = 150,
                Temperature = 0.7
            };

            Console.WriteLine("🤖 AI Response:");
            var chatResponse = await client.Chat.CreateChatCompletionAsync(chatRequest);

            if (chatResponse.IsSuccess)
            {
                var message = chatResponse.Choices[0].Message;
                Console.WriteLine($"{message.Content}");
                Console.WriteLine($"\nTokens used: {chatResponse.Usage?.TotalTokens ?? 0}");
            }
            else
            {
                Console.WriteLine($"❌ Error: {chatResponse.Error?.Error}");
            }

            // Example 3: Streaming chat completion
            Console.WriteLine("\n📡 Streaming Chat Example:");
            Console.WriteLine("==========================");

            var streamingRequest = new ChatCompletionRequest
            {
                Model = "llama-3.3-70b",
                Messages = new List<ChatMessage>
                {
                    new SystemMessage("You are a creative storyteller."),
                    new UserMessage("Tell me a very short story about a robot discovering emotions. Keep it under 100 words.")
                },
                MaxTokens = 120,
                Temperature = 0.8,
                Stream = true
            };

            Console.WriteLine("🤖 AI Response (streaming):");
            await foreach (var chunk in client.Chat.CreateChatCompletionStreamAsync(streamingRequest))
            {
                if (chunk.IsSuccess && chunk.Choices?.Any() == true)
                {
                    var content = chunk.Choices[0].Message?.Content?.ToString();
                    if (!string.IsNullOrEmpty(content))
                    {
                        Console.Write(content);
                    }
                }
            }

            Console.WriteLine("\n");

            // Example 4: Get model information (using the default model from the list)
            Console.WriteLine("🔍 Model Details Example:");
            Console.WriteLine("=========================");

            var defaultModel = modelsResponse.Data.FirstOrDefault(m => m.Id == "llama-3.3-70b");
            if (defaultModel != null)
            {
                Console.WriteLine($"Model: {defaultModel.ModelSpec.Name}");
                Console.WriteLine($"Context Length: {defaultModel.ModelSpec.AvailableContextTokens:N0} tokens");

                // Check if pricing information is available
                if (defaultModel.ModelSpec.Pricing?.Input?.Usd.HasValue == true)
                {
                    Console.WriteLine($"Input Cost: ${defaultModel.ModelSpec.Pricing.Input.Usd.Value:F6} per 1K tokens");
                }
                if (defaultModel.ModelSpec.Pricing?.Output?.Usd.HasValue == true)
                {
                    Console.WriteLine($"Output Cost: ${defaultModel.ModelSpec.Pricing.Output.Usd.Value:F6} per 1K tokens");
                }

                Console.WriteLine($"Vision Support: {(defaultModel.ModelSpec.Capabilities?.SupportsVision == true ? "Yes" : "No")}");
                Console.WriteLine($"Function Calling: {(defaultModel.ModelSpec.Capabilities?.SupportsFunctionCalling == true ? "Yes" : "No")}");
            }
            else
            {
                Console.WriteLine("Default model not found in the list");
            }

            Console.WriteLine("\n✅ Quickstart completed successfully!");
            Console.WriteLine("Check out the full README.md for more examples and features.");
        }
        catch (VeniceAIException ex)
        {
            Console.WriteLine($"❌ Venice AI Error: {ex.Message}");
            Console.WriteLine($"   Status Code: {ex.StatusCode}");
            Console.WriteLine($"   Error Code: {ex.ErrorCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Unexpected error: {ex.Message}");
        }
    }
}
