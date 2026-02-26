using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Quickstart;

internal class ProgramLogger { }

static class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("🚀 Venice AI SDK Quickstart");
            Console.WriteLine("===========================");

            // Load configuration
            var config = GetConfiguration();

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
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddVeniceAI(apiKey);
                })
                .Build();

            // Get the Venice AI client from DI
            var client = host.Services.GetRequiredService<IVeniceAIClient>();
            var logger = host.Services.GetRequiredService<ILogger<ProgramLogger>>();

            try
            {
                // Example 1: List available models
                logger.LogInformation("📋 Available Models:");
                logger.LogInformation("===================");

                var modelsResponse = await client.Models.GetModelsAsync();
                var topModels = modelsResponse.Data.Take(5);

                foreach (var model in topModels)
                {
                    logger.LogInformation("• {ModelId}", model.Id);
                    logger.LogInformation("  Type: {ModelType}", model.Type);
                    logger.LogInformation("  Context: {ContextTokens:N0} tokens", model.ModelSpec.AvailableContextTokens);
                    logger.LogInformation("  Vision: {VisionSupport}", model.ModelSpec.Capabilities?.SupportsVision == true ? "✓" : "✗");
                    logger.LogInformation("  Functions: {FunctionSupport}", model.ModelSpec.Capabilities?.SupportsFunctionCalling == true ? "✓" : "✗");
                    logger.LogInformation("");
                }

                // Example 2: Simple chat completion
                logger.LogInformation("💬 Chat Completion Example:");
                logger.LogInformation("============================");

                var chatRequest = new ChatCompletionRequest
                {
                    Model = TextModel.Llama33_70B,
                    Messages = new List<ChatMessage>
                    {
                        new SystemMessage("You are a helpful assistant that provides concise, friendly responses."),
                        new UserMessage("Hello! Can you tell me what Venice AI is in 2-3 sentences?")
                    },
                    MaxTokens = 150,
                    Temperature = 0.7
                };

                logger.LogInformation("🤖 AI Response:");
                var chatResponse = await client.Chat.CreateChatCompletionAsync(chatRequest);

                if (chatResponse.IsSuccess)
                {
                    var message = chatResponse.Choices[0].Message;
                    logger.LogInformation("{MessageContent}", message.Content);
                    logger.LogInformation("Tokens used: {TokenCount}", chatResponse.Usage?.TotalTokens ?? 0);
                }
                else
                {
                    logger.LogError("❌ Error: {ErrorMessage}", chatResponse.Error?.Error);
                }

                // Example 3: Streaming chat completion
                logger.LogInformation("📡 Streaming Chat Example:");
                logger.LogInformation("==========================");

                var streamingRequest = new ChatCompletionRequest
                {
                    Model = TextModel.Llama33_70B,
                    Messages = new List<ChatMessage>
                    {
                        new SystemMessage("You are a creative storyteller."),
                        new UserMessage("Tell me a very short story about a robot discovering emotions. Keep it under 100 words.")
                    },
                    MaxTokens = 120,
                    Temperature = 0.8,
                    Stream = true
                };

                logger.LogInformation("🤖 AI Response (streaming):");
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

                logger.LogInformation("");

                // Example 4: Get model information (using the default model from the list)
                logger.LogInformation("🔍 Model Details Example:");
                logger.LogInformation("=========================");

                var defaultModel = modelsResponse.Data.FirstOrDefault(m => m.Id == "llama-3.3-70b");
                if (defaultModel != null)
                {
                    logger.LogInformation("Model: {ModelName}", defaultModel.ModelSpec.Name);
                    logger.LogInformation("Context Length: {ContextLength:N0} tokens", defaultModel.ModelSpec.AvailableContextTokens);

                    // Check if pricing information is available
                    if (defaultModel.ModelSpec.Pricing?.Input?.Usd.HasValue == true)
                    {
                        logger.LogInformation("Input Cost: ${InputCost:F6} per 1K tokens", defaultModel.ModelSpec.Pricing.Input.Usd.Value);
                    }
                    if (defaultModel.ModelSpec.Pricing?.Output?.Usd.HasValue == true)
                    {
                        logger.LogInformation("Output Cost: ${OutputCost:F6} per 1K tokens", defaultModel.ModelSpec.Pricing.Output.Usd.Value);
                    }

                    logger.LogInformation("Vision Support: {VisionSupport}", defaultModel.ModelSpec.Capabilities?.SupportsVision == true ? "Yes" : "No");
                    logger.LogInformation("Function Calling: {FunctionSupport}", defaultModel.ModelSpec.Capabilities?.SupportsFunctionCalling == true ? "Yes" : "No");
                }
                else
                {
                    logger.LogWarning("Default model not found in the list");
                }

                logger.LogInformation("✅ Quickstart completed successfully!");
                logger.LogInformation("Check out the full README.md for more examples and features.");
            }
            catch (VeniceAIException ex)
            {
                logger.LogError(ex, "❌ Venice AI Error: {ErrorMessage}", ex.Message);
                logger.LogError("   Status Code: {StatusCode}", ex.StatusCode);
                logger.LogError("   Error Code: {ErrorCode}", ex.ErrorCode);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Unexpected error: {ErrorMessage}", ex.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Application terminated unexpectedly: {ex.Message}");
        }
    }

    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<ProgramLogger>()
            .AddEnvironmentVariables()
            .Build();
    }
}
