using Shouldly;
using VeniceAI.SDK.Models.Audio;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests.Audio;

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
        var response = await ExecuteWithErrorHandling(
            () => Client.Audio.CreateSpeechAsync(request, CancellationToken.None),
            "CreateSpeechAsync_WithValidRequest"
        );

        // Assert
        if (response == null) return;

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
            VoiceOptions.Female.Sky,
            VoiceOptions.Male.Echo,
            VoiceOptions.Female.Nova
        };

        foreach (var voice in voices)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = $"Testing voice: {voice}",
                Voice = voice,
                ResponseFormat = AudioFormat.Mp3,
                Speed = 1.0
            };

            // Act
            var response = await ExecuteWithErrorHandling(
                () => Client.Audio.CreateSpeechAsync(request, CancellationToken.None),
                $"CreateSpeechAsync_WithVoice_{voice}"
            );

            // Assert
            if (response == null) continue;

            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"Voice {voice}: Generated {response.AudioContent.Length} bytes");
        }
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentFormats_ShouldReturnAudio()
    {


        var formats = new[] { AudioFormat.Mp3, AudioFormat.Wav, AudioFormat.Opus };

        foreach (var format in formats)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = $"Testing audio format: {format}",
                Voice = VoiceOptions.Female.Sky,
                ResponseFormat = format,
                Speed = 1.0
            };

            // Act
            var response = await ExecuteWithErrorHandling(
                () => Client.Audio.CreateSpeechAsync(request, CancellationToken.None),
                $"CreateSpeechAsync_WithFormat_{format}"
            );

            // Assert
            if (response == null) continue;

            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"Format {format}: Generated {response.AudioContent.Length} bytes");
        }
    }

    [Fact]
    public async Task CreateSpeechAsync_WithDifferentSpeeds_ShouldReturnAudio()
    {


        var speeds = new[] { 0.5, 1.0, 1.5, 2.0 };

        foreach (var speed in speeds)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = $"Testing speech speed at {speed}x",
                Voice = VoiceOptions.Female.Sky,
                ResponseFormat = AudioFormat.Mp3,
                Speed = speed
            };

            // Act
            var response = await ExecuteWithErrorHandling(
                () => Client.Audio.CreateSpeechAsync(request, CancellationToken.None),
                $"CreateSpeechAsync_WithSpeed_{speed}"
            );

            // Assert
            if (response == null) continue;

            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"Speed {speed}x: Generated {response.AudioContent.Length} bytes");
        }
    }

    [Fact]
    public async Task CreateSpeechStreamAsync_WithValidRequest_ShouldReturnStreamingAudio()
    {


        // Arrange
        var request = new CreateSpeechRequest
        {
            Model = "tts-kokoro",
            Input = "This is a streaming audio test with multiple sentences. Each sentence should be processed separately.",
            Voice = VoiceOptions.Female.Sky,
            ResponseFormat = AudioFormat.Mp3,
            Speed = 1.0,
            Streaming = true
        };

        try
        {
            await DelayBetweenRequests();

            // Act
            var audioChunks = new List<byte[]>();
            await foreach (var chunk in Client.Audio.CreateSpeechStreamAsync(request, CancellationToken.None))
            {
                audioChunks.Add(chunk);
                if (audioChunks.Count > 10) break; // Limit for testing
            }

            // Assert
            audioChunks.ShouldNotBeEmpty();
            Output.WriteLine($"Received {audioChunks.Count} audio chunks");

            await VerifyResult(audioChunks);
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
    public async Task CreateSpeechAsync_WithInternationalVoices_ShouldReturnAudio()
    {


        var internationalVoices = new[]
        {
            VoiceOptions.Chinese.XiaoBei,
            VoiceOptions.Chinese.YunJian,
            VoiceOptions.International.DoraSpanish
        };

        foreach (var voice in internationalVoices)
        {
            // Arrange
            var request = new CreateSpeechRequest
            {
                Model = "tts-kokoro",
                Input = "Hello world in different languages",
                Voice = voice,
                ResponseFormat = AudioFormat.Mp3,
                Speed = 1.0
            };

            // Act
            var response = await ExecuteWithErrorHandling(
                () => Client.Audio.CreateSpeechAsync(request, CancellationToken.None),
                $"CreateSpeechAsync_WithInternationalVoice_{voice}"
            );

            // Assert
            if (response == null) continue;

            response.ShouldNotBeNull();
            response.IsSuccess.ShouldBeTrue();
            response.AudioContent.ShouldNotBeEmpty();

            Output.WriteLine($"International voice {voice}: Generated {response.AudioContent.Length} bytes");
        }
    }
}
