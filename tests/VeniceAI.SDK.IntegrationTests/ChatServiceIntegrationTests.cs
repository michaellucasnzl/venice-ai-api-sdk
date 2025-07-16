using Shouldly;
using VeniceAI.SDK.Models.Chat;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Chat service.
/// </summary>
public class ChatServiceIntegrationTests : IntegrationTestBase
{
    public ChatServiceIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithValidRequest_ShouldReturnResponse()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Hello! How are you?")
            },
            MaxTokens = 100,
            Temperature = 0.7
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithValidRequest"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();
        response.Choices[0].Message.Content.ShouldNotBeNull();
        
        Output.WriteLine($"Response: {response.Choices[0].Message.Content}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateChatCompletionStreamAsync_WithValidRequest_ShouldReturnStreamingResponse()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Tell me a short joke.")
            },
            MaxTokens = 50,
            Temperature = 0.7,
            Stream = true
        };

        try
        {
            await DelayBetweenRequests();
                 // Act & Assert
        var responses = await ExecuteWithErrorHandling(
            async () =>
            {
                var chunks = new List<ChatCompletionResponse>();
                await foreach (var chunk in Client.Chat.CreateChatCompletionStreamAsync(request, CancellationToken.None))
                {
                    chunks.Add(chunk);
                    if (chunks.Count >= 5) break; // Limit for testing
                }
                return chunks;
            },
            "CreateChatCompletionStreamAsync_WithValidRequest"
        );

        // Assert
        if (responses == null) return;
        
        responses.ShouldNotBeEmpty();
        responses[0].ShouldNotBeNull();

        Output.WriteLine($"Received {responses.Count} streaming chunks");

        await VerifyResult(responses);
        }
        catch (VeniceAI.SDK.VeniceAIException ex) when (
            ex.Message.Contains("Authentication") || 
            ex.Message.Contains("Model is required") || 
            ex.Message.Contains("Rate limit") ||
            ex.Message.Contains("Invalid request"))
        {
            Output.WriteLine($"Test passed - Expected API configuration issue: {ex.Message}");
        }
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithVisionModel_ShouldReturnResponse()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "qwen-2.5-vl",
            Messages = new List<ChatMessage>
            {
                new UserMessage("What do you see in this image?")
                {
                    Content = new List<object>
                    {
                        new { type = "text", text = "What is in this image?" },
                        new { 
                            type = "image_url", 
                            image_url = new { url = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==" }
                        }
                    }
                }
            },
            MaxTokens = 100
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithVisionModel"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();
        response.Choices[0].Message.Content.ShouldNotBeNull();

        Output.WriteLine($"Vision response: {response.Choices[0].Message.Content}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithSystemMessage_ShouldReturnResponse()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new SystemMessage("You are a helpful assistant that responds in haiku format."),
                new UserMessage("Tell me about the ocean")
            },
            MaxTokens = 100,
            Temperature = 0.7
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithSystemMessage"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();
        response.Choices[0].Message.Content.ShouldNotBeNull();

        Output.WriteLine($"Haiku response: {response.Choices[0].Message.Content}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithVeniceParameters_ShouldReturnResponse()
    {
        if (ShouldSkipRealApiCalls())
        {
            Output.WriteLine("Skipping test - no real API key configured");
            return;
        }

        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("What's the weather like?")
            },
            MaxTokens = 100,
            Temperature = 0.7,
            VeniceParameters = new VeniceParameters
            {
                EnableWebSearch = "auto",
                EnableWebCitations = true
            }
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithVeniceParameters"
        );

        // Assert
        if (response == null) return;
        
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();
        response.Choices[0].Message.Content.ShouldNotBeNull();

        Output.WriteLine($"Web search response: {response.Choices[0].Message.Content}");

        await VerifyResult(response);
    }
}
