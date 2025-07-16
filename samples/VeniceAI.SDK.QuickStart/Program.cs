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
            const string ModelName = "llama-3.3-70b";
            
            Console.WriteLine("1. Basic Chat Completion");
            Console.WriteLine("========================");
            
            var chatRequest = new ChatCompletionRequest
            {
                Model = ModelName,
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
            
            // Test Venice-specific features
            Console.WriteLine("\n2. Venice AI Features - Web Search");
            Console.WriteLine("==================================");
            
            var webSearchRequest = new ChatCompletionRequest
            {
                Model = ModelName,
                Messages = new List<ChatMessage>
                {
                    new UserMessage("What are the latest developments in AI technology this week?")
                },
                MaxTokens = 200,
                VeniceParameters = new VeniceParameters
                {
                    EnableWebSearch = "on",
                    EnableWebCitations = true
                }
            };

            Console.WriteLine("Sending web search request...");
            var webResponse = await client.Chat.CreateChatCompletionAsync(webSearchRequest);
            
            Console.WriteLine($"Response: {webResponse.Choices[0].Message.Content}");
            Console.WriteLine($"Tokens used: {webResponse.Usage?.TotalTokens ?? 0}");
            
            // Test function calling
            Console.WriteLine("\n3. Function Calling");
            Console.WriteLine("===================");
            
            var functionRequest = new ChatCompletionRequest
            {
                Model = ModelName,
                Messages = new List<ChatMessage>
                {
                    new UserMessage("What's the weather like in San Francisco?")
                },
                MaxTokens = 150,
                Tools = new List<Tool>
                {
                    new Tool
                    {
                        Type = "function",
                        Function = new FunctionDefinition
                        {
                            Name = "get_weather",
                            Description = "Get the current weather in a given location",
                            Parameters = new Dictionary<string, object>
                            {
                                ["type"] = "object",
                                ["properties"] = new Dictionary<string, object>
                                {
                                    ["location"] = new Dictionary<string, object>
                                    {
                                        ["type"] = "string",
                                        ["description"] = "The city and state, e.g. San Francisco, CA"
                                    }
                                },
                                ["required"] = new[] { "location" }
                            }
                        }
                    }
                }
            };

            Console.WriteLine("Sending function calling request...");
            var functionResponse = await client.Chat.CreateChatCompletionAsync(functionRequest);
            
            Console.WriteLine($"Response: {functionResponse.Choices[0].Message.Content}");
            var toolCalls = functionResponse.Choices[0].Message.ToolCalls;
            if (toolCalls?.Any() == true)
            {
                Console.WriteLine("Function called:");
                foreach (var toolCall in toolCalls)
                {
                    Console.WriteLine($"  - {toolCall.Function?.Name}: {toolCall.Function?.Arguments}");
                }
            }
            Console.WriteLine($"Tokens used: {functionResponse.Usage?.TotalTokens ?? 0}");

            // Test streaming
            Console.WriteLine("\n4. Streaming Response");
            Console.WriteLine("====================");
            
            var streamRequest = new ChatCompletionRequest
            {
                Model = ModelName,
                Messages = new List<ChatMessage>
                {
                    new UserMessage("Tell me a creative story about a robot discovering emotions.")
                },
                MaxTokens = 200,
                Stream = true
            };

            Console.WriteLine("Streaming response...");
            Console.Write("Story: ");
            
            await foreach (var chunk in client.Chat.CreateChatCompletionStreamAsync(streamRequest))
            {
                if (chunk.Choices?.Count > 0 && chunk.Choices[0].Message?.Content != null)
                {
                    Console.Write(chunk.Choices[0].Message.Content);
                }
            }
            
            Console.WriteLine("\n");
            
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
