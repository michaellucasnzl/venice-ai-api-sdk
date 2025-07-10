using System.Text.Json;
using VeniceAI.SDK.Models.Chat;
using Xunit;

namespace VeniceAI.SDK.Tests.Models.Chat;

public class ChatCompletionRequestTests
{
    [Fact]
    public void Constructor_SetsDefaults()
    {
        // Arrange & Act
        var request = new ChatCompletionRequest();

        // Assert
        Assert.NotNull(request.Messages);
        Assert.Empty(request.Messages);
        Assert.Equal(string.Empty, request.Model);
        Assert.Null(request.MaxTokens);
        Assert.Null(request.Temperature);
        Assert.Null(request.TopP);
        Assert.Null(request.TopK);
        Assert.Null(request.Stream);
        Assert.Null(request.Stop);
        Assert.Null(request.PresencePenalty);
        Assert.Null(request.FrequencyPenalty);
        Assert.Null(request.User);
        Assert.Null(request.Tools);
        Assert.Null(request.ToolChoice);
        Assert.Null(request.ResponseFormat);
        Assert.Null(request.Seed);
    }

    [Fact]
    public void JsonSerialization_WorksCorrectly()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = "gpt-4",
            Messages = new List<ChatMessage>
            {
                new UserMessage("Hello")
            },
            MaxTokens = 100,
            Temperature = 0.7
        };

        // Act
        var json = JsonSerializer.Serialize(request);
        
        // Assert - Just verify it serializes without throwing
        Assert.NotNull(json);
        Assert.NotEmpty(json);
        Assert.Contains("gpt-4", json);
        Assert.Contains("Hello", json);
    }

    [Fact]
    public void Messages_CanBeModified()
    {
        // Arrange
        var request = new ChatCompletionRequest();
        var message = new UserMessage("Test message");

        // Act
        request.Messages.Add(message);

        // Assert
        Assert.Single(request.Messages);
        Assert.Equal(message.Role, request.Messages[0].Role);
        Assert.Equal(message.Content, request.Messages[0].Content);
    }

    [Fact]
    public void SetTemperature_AcceptsValidValues()
    {
        // Arrange
        var request = new ChatCompletionRequest();

        // Act & Assert - Valid values
        request.Temperature = 0.0;
        Assert.Equal(0.0, request.Temperature);
        
        request.Temperature = 1.0;
        Assert.Equal(1.0, request.Temperature);
        
        request.Temperature = 0.5;
        Assert.Equal(0.5, request.Temperature);
        
        request.Temperature = null;
        Assert.Null(request.Temperature);
    }

    [Fact]
    public void SetTopP_AcceptsValidValues()
    {
        // Arrange
        var request = new ChatCompletionRequest();

        // Act & Assert - Valid values
        request.TopP = 0.0;
        Assert.Equal(0.0, request.TopP);
        
        request.TopP = 1.0;
        Assert.Equal(1.0, request.TopP);
        
        request.TopP = 0.5;
        Assert.Equal(0.5, request.TopP);
        
        request.TopP = null;
        Assert.Null(request.TopP);
    }
}

public class ChatMessageTests
{
    [Fact]
    public void UserMessage_Constructor_SetsDefaults()
    {
        // Arrange & Act
        var message = new UserMessage();

        // Assert
        Assert.Equal("user", message.Role);
        Assert.Null(message.Content);
        Assert.Null(message.Name);
    }

    [Fact]
    public void UserMessage_WithContent_SetsProperties()
    {
        // Arrange
        var content = "Hello! How can I help you today?";

        // Act
        var message = new UserMessage(content);

        // Assert
        Assert.Equal("user", message.Role);
        Assert.Equal(content, message.Content);
    }

    [Fact]
    public void AssistantMessage_Constructor_SetsDefaults()
    {
        // Arrange & Act
        var message = new AssistantMessage();

        // Assert
        Assert.Equal("assistant", message.Role);
        Assert.Null(message.Content);
        Assert.Null(message.Name);
    }

    [Fact]
    public void AssistantMessage_WithContent_SetsProperties()
    {
        // Arrange
        var content = "I'm here to help you!";

        // Act
        var message = new AssistantMessage();
        message.Content = content;

        // Assert
        Assert.Equal("assistant", message.Role);
        Assert.Equal(content, message.Content);
    }

    [Fact]
    public void SystemMessage_Constructor_SetsDefaults()
    {
        // Arrange & Act
        var message = new SystemMessage();

        // Assert
        Assert.Equal("system", message.Role);
        Assert.Null(message.Content);
        Assert.Null(message.Name);
    }

    [Fact]
    public void SystemMessage_WithContent_SetsProperties()
    {
        // Arrange
        var content = "You are a helpful assistant.";

        // Act
        var message = new SystemMessage(content);

        // Assert
        Assert.Equal("system", message.Role);
        Assert.Equal(content, message.Content);
    }

    [Fact]
    public void JsonSerialization_WorksCorrectly()
    {
        // Arrange
        var message = new UserMessage("Hello world!");

        // Act
        var json = JsonSerializer.Serialize(message);
        
        // Assert - Just verify it serializes without throwing
        Assert.NotNull(json);
        Assert.NotEmpty(json);
        Assert.Contains("Hello world!", json);
        Assert.Contains("user", json);
    }

    [Fact]
    public void MessageContent_SetsProperties()
    {
        // Arrange
        var content = new List<MessageContent>
        {
            new MessageContent { Type = "text", Text = "What's in this image?" },
            new MessageContent { Type = "image_url", ImageUrl = new ImageUrl { Url = "https://example.com/image.jpg" } }
        };

        // Act
        var message = new UserMessage(content);

        // Assert
        Assert.Equal("user", message.Role);
        Assert.Equal(content, message.Content);
    }
}
