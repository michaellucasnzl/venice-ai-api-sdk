using Shouldly;
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
        response.ShouldNotBeNull();
        response.IsSuccess.ShouldBeTrue();
        response.AudioContent.ShouldNotBeEmpty();
        response.ContentType.ShouldContain("audio");

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
                ResponseFormat = AudioFormat.Mp3
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

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
            AudioFormat.Flac
        };

        foreach (var format in formats)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = $"Testing format {format}",
                Voice = VoiceOptions.Female.Sky,
                ResponseFormat = format
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"Format {format}: Generated {response.AudioContent.Length} bytes, ContentType: {response.ContentType}");

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
                Input = $"Testing speed {speed}",
                Voice = VoiceOptions.Female.Sky,
                ResponseFormat = AudioFormat.Mp3,
                Speed = speed
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"Speed {speed}: Generated {response.AudioContent.Length} bytes");

            await VerifyResult(response, $"Speed{speed.ToString("0_0")}");
        }
    }

    [Fact]
    public async Task CreateSpeechStreamAsync_WithValidRequest_ShouldReturnStreamingAudio()
    {
        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "This is a streaming test of the Venice AI text-to-speech system. The audio should be delivered in chunks for real-time playback.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3
        };

        // Act & Assert
        var chunks = 0;
        var totalBytes = 0;

        await foreach (var chunk in Client.Audio.CreateSpeechStreamAsync(request, TestContext.Current.CancellationToken))
        {
            chunk.ShouldNotBeEmpty();
            chunks++;
            totalBytes += chunk.Length;
            Output.WriteLine($"Received chunk {chunks}: {chunk.Length} bytes");
        }

        chunks.ShouldBeGreaterThan(0);
        totalBytes.ShouldBeGreaterThan(0);

        Output.WriteLine($"Total: {chunks} chunks, {totalBytes} bytes");

        await VerifyResult(new { Chunks = chunks, TotalBytes = totalBytes });
    }

    [Fact]
    public async Task CreateSpeechAsync_WithInternationalVoices_ShouldReturnAudio()
    {
        // Test different international voice options
        var voices = new[]
        {
            VoiceOptions.Female.Sky,      // English
            VoiceOptions.Male.Adam,       // English
            VoiceOptions.Female.Bella,    // English
            VoiceOptions.Male.Onyx        // English
        };

        foreach (var voice in voices)
        {
            // Arrange - Use different languages/accents
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = "Hello world! This is a test of international voices.",
                Voice = voice,
                ResponseFormat = AudioFormat.Mp3
            };

            // Act
            var response = await Client.Audio.CreateSpeechAsync(request, TestContext.Current.CancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"International Voice {voice}: Generated {response.AudioContent.Length} bytes");

            await VerifyResult(response);
        }
    }
}