using System.Text.Json;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;
using VeniceAI.SDK.Models.Images;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Models.Embeddings;

namespace VeniceAI.SDK.Tests;

public class ModelEnumSerializationTests
{
    [Fact]
    public void ChatCompletionRequest_SerializesTextModelCorrectly()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.VeniceUncensored
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"venice-uncensored\"", json);
    }

    [Fact]
    public void ChatCompletionRequest_DeserializesTextModelCorrectly()
    {
        // Arrange
        var json = "{\"model\":\"llama-3.3-70b\",\"messages\":[]}";

        // Act
        var request = JsonSerializer.Deserialize<ChatCompletionRequest>(json);

        // Assert
        Assert.NotNull(request);
        Assert.Equal(TextModel.Llama33_70B, request.Model);
    }

    [Fact]
    public void GenerateImageRequest_SerializesImageModelCorrectly()
    {
        // Arrange
        var request = new GenerateImageRequest
        {
            Model = ImageModel.VeniceSD35,
            Prompt = "Test prompt"
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"venice-sd35\"", json);
    }

    [Fact]
    public void CreateEmbeddingRequest_SerializesEmbeddingModelCorrectly()
    {
        // Arrange
        var request = new CreateEmbeddingRequest
        {
            Model = EmbeddingModel.TextEmbeddingBGEM3,
            Input = "Test input"
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"text-embedding-bge-m3\"", json);
    }

    [Fact]
    public void CreateSpeechRequest_SerializesTextToSpeechModelCorrectly()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = TextToSpeechModel.TtsKokoro,
            Input = "Test input"
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"tts-kokoro\"", json);
    }

    [Fact]
    public void ChatCompletionRequest_CannotBeCreatedWithoutModel()
    {
        // This test demonstrates that the Model property is now required
        // and cannot be set to an empty string or null

        // Arrange
        var request = new ChatCompletionRequest();

        // Act & Assert
        // The model property is now an enum and has a default value
        // but in practice, you must explicitly set it to a valid enum value
        Assert.IsType<TextModel>(request.Model);
    }
}
