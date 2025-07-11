using FluentAssertions;
using VeniceAI.SDK.Models.Audio;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Audio service.
/// </summary>
public class AudioServiceIntegrationTests : IntegrationTestBase
{
    public AudioServiceIntegrationTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateSpeechAsync_WithValidRequest_ShouldReturnAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "Hello, this is a test of the Venice AI text-to-speech system.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0
        };

        // Act
        var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.AudioContent.Should().NotBeEmpty();
        response.ContentType.Should().Contain("audio");

        Output.WriteLine($"Generated audio with {response.AudioContent.Length} bytes");
        Output.WriteLine($"Content type: {response.ContentType}");

        await VerifyResult(response);
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentVoices_ShouldReturnAudio()
    {
        // Test different voice options
        var voices = new[]
        {
            VoiceOptions.Female.Nova,
            VoiceOptions.Male.Adam,
            VoiceOptions.Female.Bella
        };

        foreach (var voice in voices)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = $"Testing voice {voice}",
                Voice = voice,
                ResponseFormat = AudioFormat.Mp3,
                Speed = 1.0
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();

            Output.WriteLine($"Voice {voice}: Generated {response.AudioContent.Length} bytes");
            await VerifyResult(response, voice.Replace(".", "_"));
        }
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentFormats_ShouldReturnAudio()
    {
        // Test different audio formats
        var formats = new[]
        {
            AudioFormat.Mp3,
            AudioFormat.Wav,
            AudioFormat.Opus
        };

        foreach (var format in formats)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = $"Testing audio format {format}",
                Voice = VoiceOptions.Female.Sky,
                ResponseFormat = format,
                Speed = 1.0
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();

            Output.WriteLine($"Format {format}: Generated {response.AudioContent.Length} bytes");
            await VerifyResult(response, format.ToString());
        }
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentSpeeds_ShouldReturnAudio()
    {
        // Test different speech speeds
        var speeds = new[] { 0.5, 1.0, 1.5, 2.0 };

        foreach (var speed in speeds)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = "Testing different speech speeds for text-to-speech conversion.",
                Voice = VoiceOptions.Female.Sky,
                ResponseFormat = AudioFormat.Mp3,
                Speed = speed
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();

            Output.WriteLine($"Speed {speed}: Generated {response.AudioContent.Length} bytes");
            await VerifyResult(response, $"Speed{speed.ToString("0_0")}");
        }
    }

    [Fact]
    public async Task CreateSpeechStreamAsync_WithValidRequest_ShouldReturnAudioStream()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "This is a test of streaming text-to-speech functionality.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0,
            Streaming = true
        };

        // Act
        var totalBytes = 0;
        var chunks = 0;

        await foreach (var chunk in Client.Audio.CreateSpeechStreamAsync(request,
                           TestContext.Current.CancellationToken))
        {
            chunk.Should().NotBeEmpty();
            totalBytes += chunk.Length;
            chunks++;
        }

        // Assert
        chunks.Should().BeGreaterThan(0);
        totalBytes.Should().BeGreaterThan(0);

        Output.WriteLine($"Received {chunks} chunks with total {totalBytes} bytes");
    }

    [Fact]
    public async Task CreateSpeechAsync_WithInternationalVoices_ShouldReturnAudio()
    {
        // Test some international voices
        var internationalVoices = new[]
        {
            VoiceOptions.Chinese.XiaoXiao,
            VoiceOptions.International.Siwis, // French
            VoiceOptions.International.Sara // Italian
        };

        var responses = new List<CreateSpeechResponse>();
        foreach (var voice in internationalVoices)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = "Testing international voice capabilities.",
                Voice = voice,
                ResponseFormat = AudioFormat.Mp3,
                Speed = 1.0
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();

            Output.WriteLine($"International voice {voice}: Generated {response.AudioContent.Length} bytes");
            responses.Add(response);
        }

        await VerifyResult(responses);
    }
}