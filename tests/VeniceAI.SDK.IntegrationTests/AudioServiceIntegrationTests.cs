using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Audio;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Audio service.
/// </summary>
public class AudioServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly IVeniceAIClient _client;
    private readonly ITestOutputHelper _output;

    public AudioServiceIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<AudioServiceIntegrationTests>();
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
        var response = await _client.Audio.CreateSpeechAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.AudioContent.Should().NotBeEmpty();
        response.ContentType.Should().Contain("audio");
        
        _output.WriteLine($"Generated audio with {response.AudioContent.Length} bytes");
        _output.WriteLine($"Content type: {response.ContentType}");
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
            var response = await _client.Audio.CreateSpeechAsync(request);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();
            
            _output.WriteLine($"Voice {voice}: Generated {response.AudioContent.Length} bytes");
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
            var response = await _client.Audio.CreateSpeechAsync(request);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();
            
            _output.WriteLine($"Format {format}: Generated {response.AudioContent.Length} bytes");
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
            var response = await _client.Audio.CreateSpeechAsync(request);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();
            
            _output.WriteLine($"Speed {speed}: Generated {response.AudioContent.Length} bytes");
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
        
        await foreach (var chunk in _client.Audio.CreateSpeechStreamAsync(request))
        {
            chunk.Should().NotBeEmpty();
            totalBytes += chunk.Length;
            chunks++;
        }

        // Assert
        chunks.Should().BeGreaterThan(0);
        totalBytes.Should().BeGreaterThan(0);
        
        _output.WriteLine($"Received {chunks} chunks with total {totalBytes} bytes");
    }

    [Fact]
    public async Task CreateSpeechAsync_WithInternationalVoices_ShouldReturnAudio()
    {
        // Test some international voices
        var internationalVoices = new[]
        {
            VoiceOptions.Chinese.XiaoXiao,
            VoiceOptions.International.Siwis, // French
            VoiceOptions.International.Sara   // Italian
        };

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
            var response = await _client.Audio.CreateSpeechAsync(request);

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.AudioContent.Should().NotBeEmpty();
            
            _output.WriteLine($"International voice {voice}: Generated {response.AudioContent.Length} bytes");
        }
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
