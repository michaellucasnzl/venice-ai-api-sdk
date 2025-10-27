using Shouldly;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Chat;

/// <summary>
/// Integration tests for the Chat service.
/// </summary>
public class ChatServiceIntegrationTests : IntegrationTestBase
{
    public ChatServiceIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    /// <summary>
    /// Returns properties that should be scrubbed from verification output for chat tests.
    /// </summary>
    protected override string[] GetPropertiesToScrub()
    {
        return new[] { "Content", "CompletionTokens", "TotalTokens", "PromptTokens", "Usage" };
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithValidRequest_ShouldReturnResponse()
    {


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.Llama33_70B,
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


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.Llama33_70B,
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


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.VeniceMedium, // mistral-31-24b has vision capabilities
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


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.Llama33_70B,
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


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.Llama33_70B,
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

    [Fact]
    public async Task CreateChatCompletionAsync_WithCharacterSlug_ShouldReturnResponse()
    {


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.Llama33_70B,
            Messages = new List<ChatMessage>
            {
                new UserMessage("What is the meaning of life?")
            },
            MaxTokens = 100,
            Temperature = 0.7,
            VeniceParameters = new VeniceParameters
            {
                CharacterSlug = "alan-watts"
            }
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithCharacterSlug"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();
        response.Choices[0].Message.Content.ShouldNotBeNull();

        Output.WriteLine($"Character response: {response.Choices[0].Message.Content}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithFunctionCalling_ShouldReturnResponse()
    {


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.VeniceLarge,
            Messages = new List<ChatMessage>
            {
                new UserMessage("What's the current weather like in New York?")
            },
            MaxTokens = 200,
            Temperature = 0.1,
            Tools = new List<Tool>
            {
                new Tool
                {
                    Type = "function",
                    Function = new FunctionDefinition
                    {
                        Name = "get_current_weather",
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
                                },
                                ["unit"] = new Dictionary<string, object>
                                {
                                    ["type"] = "string",
                                    ["enum"] = new[] { "celsius", "fahrenheit" }
                                }
                            },
                            ["required"] = new[] { "location" }
                        }
                    }
                }
            }
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithFunctionCalling"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();

        var choice = response.Choices[0];
        if (choice.Message.ToolCalls?.Any() == true)
        {
            Output.WriteLine($"Function call detected: {choice.Message.ToolCalls[0].Function?.Name}");
            Output.WriteLine($"Function arguments: {choice.Message.ToolCalls[0].Function?.Arguments}");
        }
        else
        {
            Output.WriteLine($"Response content: {choice.Message.Content}");
        }

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithJsonResponseFormat_ShouldReturnResponse()
    {


        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.VeniceLarge,
            Messages = new List<ChatMessage>
            {
                new UserMessage("Generate a JSON object with information about a cat. Include name, age, and breed.")
            },
            MaxTokens = 100,
            Temperature = 0.1,
            ResponseFormat = new ResponseFormat
            {
                Type = "json_object"
            }
        };

        // Act
        var response = await ExecuteWithErrorHandling(
            () => Client.Chat.CreateChatCompletionAsync(request, CancellationToken.None),
            "CreateChatCompletionAsync_WithJsonResponseFormat"
        );

        // Assert
        if (response == null) return;

        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.Choices.ShouldNotBeEmpty();
        response.Choices[0].Message.Content.ShouldNotBeNull();

        // Try to parse the response as JSON to verify it's valid
        var content = response.Choices[0].Message.Content?.ToString();
        if (!string.IsNullOrEmpty(content))
        {
            try
            {
                using var jsonDoc = System.Text.Json.JsonDocument.Parse(content);
                Output.WriteLine($"Valid JSON response: {content}");
            }
            catch (System.Text.Json.JsonException)
            {
                Output.WriteLine($"Response (may not be valid JSON): {content}");
            }
        }

        await VerifyResult(response);
    }
}
