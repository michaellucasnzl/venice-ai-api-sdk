using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Chat;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Chat service.
/// </summary>
public class ChatServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly IVeniceAIClient _client;
    private readonly ITestOutputHelper _output;

    public ChatServiceIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<ChatServiceIntegrationTests>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);
                });
                
                services.AddVeniceAI(context.Configuration);
            });

        _host = hostBuilder.Build();
        _client = _host.Services.GetRequiredService<IVeniceAIClient>();
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithValidRequest_ShouldReturnResponse()
    {
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
        var response = await _client.Chat.CreateChatCompletionAsync(request);

        // Assert
        response.Should().NotBeNull();
        
        // Debug information if request failed
        if (!response.IsSuccess)
        {
            _output.WriteLine($"Request failed with status code: {response.StatusCode}");
            _output.WriteLine($"Error: {response.Error?.Error ?? "No error details"}");
            _output.WriteLine($"Raw content: {response.RawContent}");
        }
        
        response.IsSuccess.Should().BeTrue();
        response.Choices.Should().NotBeEmpty();
        response.Choices[0].Message.Content.Should().NotBeNull();
        
        _output.WriteLine($"Response: {response.Choices[0].Message.Content}");
    }

    [Fact]
    public async Task CreateChatCompletionStreamAsync_WithValidRequest_ShouldReturnStreamingResponse()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Write a short poem about artificial intelligence.")
            },
            MaxTokens = 150,
            Temperature = 0.8
        };

        // Act & Assert
        var chunks = new List<ChatCompletionResponse>();
        
        await foreach (var chunk in _client.Chat.CreateChatCompletionStreamAsync(request))
        {
            chunks.Add(chunk);
            _output.WriteLine($"Chunk: {chunk.Choices?.FirstOrDefault()?.Message?.Content}");
        }

        chunks.Should().NotBeEmpty();
        chunks.Any(c => c.Choices?.Any() == true).Should().BeTrue();
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithVisionModel_ShouldReturnResponse()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.2-11b-vision", // Using a vision model
            Messages = new List<ChatMessage>
            {
                new UserMessage(new List<MessageContent>
                {
                    new MessageContent
                    {
                        Type = "text",
                        Text = "What do you see in this image?"
                    },
                    new MessageContent
                    {
                        Type = "image_url",
                        ImageUrl = new ImageUrl
                        {
                            Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Gfp-wisconsin-madison-the-nature-boardwalk.jpg/2560px-Gfp-wisconsin-madison-the-nature-boardwalk.jpg"
                        }
                    }
                })
            },
            MaxTokens = 200
        };

        // Act
        var response = await _client.Chat.CreateChatCompletionAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Choices.Should().NotBeEmpty();
        response.Choices[0].Message.Content.Should().NotBeNull();
        
        _output.WriteLine($"Vision Response: {response.Choices[0].Message.Content}");
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithSystemMessage_ShouldReturnResponse()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new SystemMessage("You are a helpful assistant that responds in a formal, professional manner."),
                new UserMessage("What is the capital of France?")
            },
            MaxTokens = 100
        };

        // Act
        var response = await _client.Chat.CreateChatCompletionAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Choices.Should().NotBeEmpty();
        response.Choices[0].Message.Content.Should().NotBeNull();
        
        _output.WriteLine($"System Message Response: {response.Choices[0].Message.Content}");
    }

    [Fact]
    public async Task CreateChatCompletionAsync_WithVeniceParameters_ShouldReturnResponse()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "llama-3.3-70b",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Tell me about web search capabilities.")
            },
            MaxTokens = 200,
            VeniceParameters = new VeniceParameters
            {
                EnableWebSearch = "auto",
                EnableWebCitations = true
            }
        };

        // Act
        var response = await _client.Chat.CreateChatCompletionAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Choices.Should().NotBeEmpty();
        response.Choices[0].Message.Content.Should().NotBeNull();
        
        _output.WriteLine($"Web Search Response: {response.Choices[0].Message.Content}");
        
        if (response.Citations != null)
        {
            _output.WriteLine($"Citations: {response.Citations.Count}");
            foreach (var citation in response.Citations)
            {
                _output.WriteLine($"- {citation.Title}: {citation.Url}");
            }
        }
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
