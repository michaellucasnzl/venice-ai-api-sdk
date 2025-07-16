using VeniceAI.SDK.Models.Audio;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Audio;

public class AudioServiceIntegrationTests : IntegrationTestBase
{
    public AudioServiceIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateSpeechAsync_WithBasicRequest_ShouldReturnAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Hello! This is a demonstration of Venice AI's text-to-speech capabilities.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0
        };

        // Act
        var result = await ExecuteWithErrorHandling(
            () => Client.Audio.CreateSpeechAsync(request),
            "CreateSpeechAsync_WithBasicRequest");

        // Assert
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.AudioContent);
            Assert.True(result.AudioContent.Length > 0);
            Assert.NotNull(result.ContentType);
        }

        await VerifyResult(result ?? new object());
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentVoices_ShouldReturnAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Testing different voice options in Venice AI.",
            Voice = VoiceOptions.Male.Adam,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0
        };

        // Act
        var result = await ExecuteWithErrorHandling(
            () => Client.Audio.CreateSpeechAsync(request),
            "CreateSpeechAsync_WithDifferentVoices");

        // Assert
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.AudioContent);
            Assert.True(result.AudioContent.Length > 0);
        }

        await VerifyResult(result ?? new object());
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentFormats_ShouldReturnAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Testing different audio formats.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Wav,
            Speed = 1.0
        };

        // Act
        var result = await ExecuteWithErrorHandling(
            () => Client.Audio.CreateSpeechAsync(request),
            "CreateSpeechAsync_WithDifferentFormats");

        // Assert
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.AudioContent);
            Assert.True(result.AudioContent.Length > 0);
        }

        await VerifyResult(result ?? new object());
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentSpeeds_ShouldReturnAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Testing different speech speeds.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.5
        };

        // Act
        var result = await ExecuteWithErrorHandling(
            () => Client.Audio.CreateSpeechAsync(request),
            "CreateSpeechAsync_WithDifferentSpeeds");

        // Assert
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.AudioContent);
            Assert.True(result.AudioContent.Length > 0);
        }

        await VerifyResult(result ?? new object());
    }

    [Fact]
    public async Task CreateSpeechAsync_WithInternationalVoices_ShouldReturnAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Testing international voice capabilities.",
            Voice = VoiceOptions.Female.Nova,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0
        };

        // Act
        var result = await ExecuteWithErrorHandling(
            () => Client.Audio.CreateSpeechAsync(request),
            "CreateSpeechAsync_WithInternationalVoices");

        // Assert
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.AudioContent);
            Assert.True(result.AudioContent.Length > 0);
        }

        await VerifyResult(result ?? new object());
    }

    [Fact]
    public async Task CreateSpeechStreamAsync_WithBasicRequest_ShouldReturnStreamingAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Testing streaming audio generation.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0
        };

        // Act
        var chunks = new List<byte[]>();
        await foreach (var chunk in Client.Audio.CreateSpeechStreamAsync(request))
        {
            if (chunk != null && chunk.Length > 0)
            {
                chunks.Add(chunk);
            }
        }

        // Assert
        Assert.NotEmpty(chunks);
        var totalBytes = chunks.Sum(c => c.Length);
        Assert.True(totalBytes > 0);

        await VerifyResult(new { ChunkCount = chunks.Count, TotalBytes = totalBytes });
    }
}
