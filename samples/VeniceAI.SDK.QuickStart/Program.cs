using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Images;

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

            // First, let's check what models are available
            Console.WriteLine("Available Models:");
            Console.WriteLine("=================");

            var models = await client.Models.GetModelsAsync();

            foreach (var model in models.Data)
            {
                Console.WriteLine($"- {model.Id} (Type: {model.Type})");
            }

            Console.WriteLine("\n");

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

            // Test image generation
            Console.WriteLine("\n5. Image Generation");
            Console.WriteLine("===================");

            var imageRequest = new GenerateImageRequest
            {
                Model = "flux-dev", // FLUX Standard - highest quality
                Prompt = "A beautiful sunset over mountains with a lake in the foreground",
                Width = 1024,
                Height = 1024,
                Steps = 25,
                CfgScale = 1.0,
                Format = "png"
            };

            Console.WriteLine("Generating image...");
            var imageResponse = await client.Images.GenerateImageAsync(imageRequest);

            if (imageResponse.IsSuccess && imageResponse.Images?.Any() == true)
            {
                Console.WriteLine($"Image generated successfully!");
                Console.WriteLine($"Number of images: {imageResponse.Images.Count}");
                Console.WriteLine($"Image format: {imageRequest.Format}");
                Console.WriteLine($"Image size: {imageRequest.Width}x{imageRequest.Height}");

                // Note: In a real application, you would save the base64 image data to a file
            }
            else
            {
                Console.WriteLine("Image generation failed or returned no images.");
            }

            // Test simple image generation (OpenAI compatible)
            Console.WriteLine("\n6. Simple Image Generation");
            Console.WriteLine("==========================");

            var simpleImageRequest = new SimpleGenerateImageRequest
            {
                Prompt = "A cute robot playing with a butterfly in a garden",
                Model = "flux-1.1-pro",
                Size = "1024x1024",
                ResponseFormat = "b64_json",
                Quality = "standard"
            };

            Console.WriteLine("Generating image with simple API...");
            var simpleImageResponse = await client.Images.GenerateImageSimpleAsync(simpleImageRequest);

            if (simpleImageResponse.IsSuccess && simpleImageResponse.Data?.Any() == true)
            {
                Console.WriteLine($"Simple image generated successfully!");
                Console.WriteLine($"Number of images: {simpleImageResponse.Data.Count}");
                Console.WriteLine($"Response format: {simpleImageRequest.ResponseFormat}");
            }
            else
            {
                Console.WriteLine("Simple image generation failed or returned no images.");
            }

            // Test getting image styles
            Console.WriteLine("\n7. Available Image Styles");
            Console.WriteLine("=========================");

            Console.WriteLine("Fetching available image styles...");
            var stylesResponse = await client.Images.GetImageStylesAsync();

            if (stylesResponse.IsSuccess && stylesResponse.Styles?.Any() == true)
            {
                Console.WriteLine($"Found {stylesResponse.Styles.Count} available styles:");
                foreach (var style in stylesResponse.Styles.Take(5)) // Show first 5 styles
                {
                    Console.WriteLine($"  - {style.Name}: {style.Description}");
                }

                if (stylesResponse.Styles.Count > 5)
                {
                    Console.WriteLine($"  ... and {stylesResponse.Styles.Count - 5} more styles");
                }
            }
            else
            {
                Console.WriteLine("Failed to fetch image styles or no styles available.");
            }

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
