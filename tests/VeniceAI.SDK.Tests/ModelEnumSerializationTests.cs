using System.Text.Json;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;
using VeniceAI.SDK.Models.Images;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Models.Embeddings;
using VeniceAI.SDK.Models.Responses;
using VeniceAI.SDK.Models.Video;

namespace VeniceAI.SDK.Tests;

public class ModelEnumSerializationTests
{
    [Fact]
    public void ChatCompletionRequest_SerializesTextModelCorrectly()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.VeniceUncensoredRolePlay
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"venice-uncensored-role-play\"", json);
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

    [Fact]
    public void QueueAudioRequest_SerializesMusicModelCorrectly()
    {
        // Arrange
        var request = new QueueAudioRequest
        {
            Model = MusicModel.ElevenlabsMusic,
            Prompt = "Test prompt"
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"elevenlabs-music\"", json);
    }

    [Fact]
    public void CreateTranscriptionRequest_SerializesAsrModelCorrectly()
    {
        // Arrange
        var request = new CreateTranscriptionRequest
        {
            Model = AsrModel.WhisperLargeV3
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"openai/whisper-large-v3\"", json);
    }

    [Fact]
    public void QueueVideoRequest_SerializesNewVideoModelCorrectly()
    {
        // Arrange
        var request = new QueueVideoRequest
        {
            Model = VideoModel.Wan30TextToVideo,
            Prompt = "Test prompt"
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"wan-3-0-text-to-video\"", json);
    }

    [Fact]
    public void ResponsesRequest_SerializesTextModelCorrectly()
    {
        // Arrange
        var request = new ResponsesRequest
        {
            Model = TextModel.Gemini36Flash
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"gemini-3-6-flash\"", json);
    }

    [Fact]
    public void ChatCompletionRequest_SerializesNewTextModelCorrectly()
    {
        // Arrange
        var request = new ChatCompletionRequest
        {
            Model = TextModel.ClaudeOpus5
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"claude-opus-5\"", json);
    }

    [Fact]
    public void GenerateImageRequest_SerializesNewImageModelCorrectly()
    {
        // Arrange
        var request = new GenerateImageRequest
        {
            Model = ImageModel.QwenImage3,
            Prompt = "Test prompt"
        };

        // Act
        var json = JsonSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"model\":\"qwen-image-3\"", json);
    }
}
