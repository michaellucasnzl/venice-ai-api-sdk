using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Embeddings;
using VeniceAI.SDK.Models.Images;

namespace VeniceAI.SDK.Samples;

/// <summary>
/// Sample application demonstrating the Venice AI SDK usage.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        // Setup configuration and dependency injection
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<Program>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                });

                // Add Venice AI SDK
                services.AddVeniceAI(context.Configuration);
            })
            .Build();

        var client = host.Services.GetRequiredService<IVeniceAIClient>();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        try
        {
            Console.WriteLine("=== Venice AI SDK Sample Application ===\n");

            // Run various samples
            await RunChatSamples(client, logger);
            await RunImageSamples(client, logger);
            await RunEmbeddingSamples(client, logger);
            await RunAudioSamples(client, logger);
            await RunModelSamples(client, logger);
            await RunBillingSamples(client, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred running the samples");
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            host.Dispose();
        }
    }

    private static async Task RunChatSamples(IVeniceAIClient client, ILogger logger)
    {
        Console.WriteLine("=== Chat Completion Samples ===");

        // Basic chat completion
        var chatRequest = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Hello! Can you explain what Venice AI is?")
            },
            MaxTokens = 150,
            Temperature = 0.7
        };

        var chatResponse = await client.Chat.CreateChatCompletionAsync(chatRequest);
        if (chatResponse.IsSuccess)
        {
            Console.WriteLine($"Assistant: {chatResponse.Choices[0].Message.Content}\n");
        }
        else
        {
            Console.WriteLine($"Chat error: {chatResponse.Error?.Error}\n");
        }

        // Streaming chat completion
        Console.WriteLine("=== Streaming Chat ===");
        var streamRequest = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Write a short poem about artificial intelligence.")
            },
            MaxTokens = 100,
            Temperature = 0.8
        };

        Console.Write("AI Poem: ");
        await foreach (var chunk in client.Chat.CreateChatCompletionStreamAsync(streamRequest))
        {
            if (chunk.IsSuccess && chunk.Choices?.Any() == true)
            {
                Console.Write(chunk.Choices[0].Message.Content);
            }
        }
        Console.WriteLine("\n");
    }

    private static async Task RunImageSamples(IVeniceAIClient client, ILogger logger)
    {
        Console.WriteLine("=== Image Generation Samples ===");

        // Basic image generation
        var imageRequest = new GenerateImageRequest
        {
            Model = "flux-dev",
            Prompt = "A beautiful landscape with mountains and a lake at sunset",
            Width = 1024,
            Height = 1024,
            Steps = 20,
            Format = "webp"
        };

        var imageResponse = await client.Images.GenerateImageAsync(imageRequest);
        if (imageResponse.IsSuccess && imageResponse.Data.Any())
        {
            Console.WriteLine($"Generated image - Base64 length: {imageResponse.Data[0].B64Json?.Length} characters");
        }
        else
        {
            Console.WriteLine($"Image generation error: {imageResponse.Error?.Error}");
        }

        // Simple image generation (OpenAI-compatible)
        var simpleRequest = new SimpleGenerateImageRequest
        {
            Model = "flux-dev",
            Prompt = "A serene mountain landscape",
            N = 1,
            Size = "1024x1024"
        };

        var simpleResponse = await client.Images.GenerateImageSimpleAsync(simpleRequest);
        if (simpleResponse.IsSuccess && simpleResponse.Data.Any())
        {
            Console.WriteLine($"Simple image generated - Base64 length: {simpleResponse.Data[0].B64Json?.Length} characters");
        }
        else
        {
            Console.WriteLine($"Simple image generation error: {simpleResponse.Error?.Error}");
        }

        // Get image styles
        var stylesResponse = await client.Images.GetImageStylesAsync();
        if (stylesResponse.IsSuccess)
        {
            Console.WriteLine($"Available image styles: {stylesResponse.Styles.Count}");
            foreach (var style in stylesResponse.Styles.Take(3))
            {
                Console.WriteLine($"- {style.Name}");
            }
        }
        else
        {
            Console.WriteLine($"Image styles error: {stylesResponse.Error?.Error}");
        }

        // Note: Upscale and Edit operations require existing images as input
        // These are not included in this basic sample but are available in the SDK

        Console.WriteLine();
    }

    private static async Task RunEmbeddingSamples(IVeniceAIClient client, ILogger logger)
    {
        Console.WriteLine("=== Embedding Samples ===");

        var embeddingRequest = new CreateEmbeddingRequest
        {
            Model = "text-embedding-bge-m3",
            Input = "Venice AI provides powerful artificial intelligence capabilities through its API.",
            EncodingFormat = "float"
        };

        var embeddingResponse = await client.Embeddings.CreateEmbeddingAsync(embeddingRequest);
        if (embeddingResponse.IsSuccess)
        {
            Console.WriteLine($"Generated embedding with {embeddingResponse.Data[0].Embedding.Count} dimensions");
            Console.WriteLine($"Used {embeddingResponse.Usage.PromptTokens} prompt tokens\n");
        }
        else
        {
            Console.WriteLine($"Embedding error: {embeddingResponse.Error?.Error}\n");
        }
    }

    private static async Task RunAudioSamples(IVeniceAIClient client, ILogger logger)
    {
        Console.WriteLine("=== Audio/TTS Samples ===");

        var audioRequest = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Hello! This is a demonstration of Venice AI's text-to-speech capabilities.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0
        };

        var audioResponse = await client.Audio.CreateSpeechAsync(audioRequest);
        if (audioResponse.IsSuccess)
        {
            Console.WriteLine($"Generated audio with {audioResponse.AudioContent.Length} bytes");
            Console.WriteLine($"Content type: {audioResponse.ContentType}\n");
        }
        else
        {
            Console.WriteLine($"Audio generation error: {audioResponse.Error?.Error}\n");
        }
    }

    private static async Task RunModelSamples(IVeniceAIClient client, ILogger logger)
    {
        Console.WriteLine("=== Model Information Samples ===");

        // Get all models
        var modelsResponse = await client.Models.GetModelsAsync();
        if (modelsResponse.IsSuccess)
        {
            var textModels = modelsResponse.Data.Where(m => m.Type == "text").Count();
            var imageModels = modelsResponse.Data.Where(m => m.Type == "image").Count();
            var embeddingModels = modelsResponse.Data.Where(m => m.Type == "embedding").Count();

            Console.WriteLine($"Available models: {modelsResponse.Data.Count}");
            Console.WriteLine($"- Text models: {textModels}");
            Console.WriteLine($"- Image models: {imageModels}");
            Console.WriteLine($"- Embedding models: {embeddingModels}");
        }
        else
        {
            Console.WriteLine($"Models error: {modelsResponse.Error?.Error}");
        }

        // Get a specific model (Note: Individual model details endpoint returns 404 for all models)
        if (modelsResponse.IsSuccess && modelsResponse.Data.Any())
        {
            var firstModel = modelsResponse.Data.First();
            try
            {
                var modelResponse = await client.Models.GetModelAsync(firstModel.Id);
                if (modelResponse != null)
                {
                    Console.WriteLine($"Model details - ID: {firstModel.Id}, Owner: {firstModel.OwnedBy}");
                }
            }
            catch (Exception ex)
            {
                if (ex is VeniceAI.SDK.VeniceAIException veniceEx && veniceEx.StatusCode == 404)
                {
                    Console.WriteLine($"Individual model details endpoint not supported");
                    Console.WriteLine($"Model info from list - ID: {firstModel.Id}, Owner: {firstModel.OwnedBy}, Type: {firstModel.Type}");
                }
                else
                {
                    Console.WriteLine($"Error getting model details: {ex.Message}");
                }
            }
        }

        // Get model traits
        var traitsResponse = await client.Models.GetModelTraitsAsync();
        if (traitsResponse.IsSuccess)
        {
            Console.WriteLine($"Model traits: {traitsResponse.Traits.Count}");
            foreach (var trait in traitsResponse.Traits.Take(3))
            {
                Console.WriteLine($"- {trait.Key}: {trait.Value}");
            }
        }
        else
        {
            Console.WriteLine($"Model traits error: {traitsResponse.Error?.Error}");
        }

        // Get model compatibility mapping
        var compatibilityResponse = await client.Models.GetModelCompatibilityAsync();
        if (compatibilityResponse.IsSuccess)
        {
            Console.WriteLine($"Model compatibility mappings: {compatibilityResponse.Compatibility.Count}");
            foreach (var mapping in compatibilityResponse.Compatibility.Take(3))
            {
                Console.WriteLine($"- {mapping.Key}: {mapping.Value}");
            }
        }
        else
        {
            Console.WriteLine($"Model compatibility error: {compatibilityResponse.Error?.Error}");
        }

        Console.WriteLine();
    }

    private static async Task RunBillingSamples(IVeniceAIClient client, ILogger logger)
    {
        Console.WriteLine("=== Billing Information Samples ===");

        var billingRequest = new VeniceAI.SDK.Models.Billing.BillingUsageRequest
        {
            Limit = 5,
            Page = 1,
            SortOrder = VeniceAI.SDK.Models.Billing.SortOrder.Descending
        };

        var billingResponse = await client.Billing.GetBillingUsageAsync(billingRequest);
        if (billingResponse.IsSuccess)
        {
            Console.WriteLine($"Recent billing entries: {billingResponse.Data.Count}");
            Console.WriteLine($"Total entries: {billingResponse.Pagination.Total}");

            foreach (var entry in billingResponse.Data.Take(3))
            {
                Console.WriteLine($"- {entry.Timestamp:yyyy-MM-dd HH:mm}: {entry.Sku} - {entry.Amount} {entry.Currency}");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Billing error: {billingResponse.Error?.Error}\n");
        }
    }
}
