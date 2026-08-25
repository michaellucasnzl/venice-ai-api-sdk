using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Audio;

/// <summary>
/// Request for creating speech from text.
/// </summary>
public class CreateSpeechRequest
{
    /// <summary>
    /// The text to generate audio for.
    /// </summary>
    [JsonPropertyName("input")]
    public string Input { get; set; } = string.Empty;

    /// <summary>
    /// The TTS model to use for speech generation.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(TextToSpeechModelJsonConverter))]
    public TextToSpeechModel? Model { get; set; }

    /// <summary>
    /// The voice to use when generating the audio.
    /// </summary>
    [JsonPropertyName("voice")]
    public string? Voice { get; set; }

    /// <summary>
    /// The format to audio in.
    /// </summary>
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }

    /// <summary>
    /// The speed of the generated audio.
    /// </summary>
    [JsonPropertyName("speed")]
    public double? Speed { get; set; }

    /// <summary>
    /// Should the content stream back sentence by sentence.
    /// </summary>
    [JsonPropertyName("streaming")]
    public bool? Streaming { get; set; }
}

/// <summary>
/// Response from speech creation API.
/// </summary>
public class CreateSpeechResponse : BaseResponse
{
    /// <summary>
    /// The audio content as a byte array.
    /// </summary>
    public byte[] AudioContent { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// The content type of the audio.
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
}

/// <summary>
/// Available voices for text-to-speech.
/// </summary>
public static class VoiceOptions
{
    /// <summary>
    /// Female voices with alloy characteristics.
    /// </summary>
    public static class Female
    {
        public const string Alloy = "af_alloy";
        public const string Aoede = "af_aoede";
        public const string Bella = "af_bella";
        public const string Heart = "af_heart";
        public const string Jadzia = "af_jadzia";
        public const string Jessica = "af_jessica";
        public const string Kore = "af_kore";
        public const string Nicole = "af_nicole";
        public const string Nova = "af_nova";
        public const string River = "af_river";
        public const string Sarah = "af_sarah";
        public const string Sky = "af_sky";
    }

    /// <summary>
    /// Male voices with alloy characteristics.
    /// </summary>
    public static class Male
    {
        public const string Adam = "am_adam";
        public const string Echo = "am_echo";
        public const string Eric = "am_eric";
        public const string Fenrir = "am_fenrir";
        public const string Liam = "am_liam";
        public const string Michael = "am_michael";
        public const string Onyx = "am_onyx";
        public const string Puck = "am_puck";
        public const string Santa = "am_santa";
    }

    /// <summary>
    /// Chinese voices.
    /// </summary>
    public static class Chinese
    {
        public const string XiaoBei = "zf_xiaobei";
        public const string XiaoNi = "zf_xiaoni";
        public const string XiaoXiao = "zf_xiaoxiao";
        public const string XiaoYi = "zf_xiaoyi";
        public const string YunJian = "zm_yunjian";
        public const string YunXi = "zm_yunxi";
        public const string YunXia = "zm_yunxia";
        public const string YunYang = "zm_yunyang";
    }

    /// <summary>
    /// Other international voices.
    /// </summary>
    public static class International
    {
        public const string Siwis = "ff_siwis"; // French
        public const string Sara = "if_sara"; // Italian
        public const string Nicola = "im_nicola"; // Italian
        public const string Alpha = "jf_alpha"; // Japanese
        public const string Gongitsune = "jf_gongitsune"; // Japanese
        public const string Nezumi = "jf_nezumi"; // Japanese
        public const string Tebukuro = "jf_tebukuro"; // Japanese
        public const string Kumo = "jm_kumo"; // Japanese
        public const string Dora = "pf_dora"; // Portuguese
        public const string Alex = "pm_alex"; // Portuguese
        public const string DoraSpanish = "ef_dora"; // Spanish
        public const string AlexSpanish = "em_alex"; // Spanish
    }
}

/// <summary>
/// Audio response formats.
/// </summary>
public static class AudioFormat
{
    public const string Mp3 = "mp3";
    public const string Opus = "opus";
    public const string Aac = "aac";
    public const string Flac = "flac";
    public const string Wav = "wav";
    public const string Pcm = "pcm";
}

/// <summary>
/// Request to queue an audio generation job.
/// </summary>
public class QueueAudioRequest
{
    /// <summary>
    /// The model to use for audio generation.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(MusicModelJsonConverter))]
    public MusicModel Model { get; set; }

    /// <summary>
    /// The prompt describing the audio to generate.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// Optional lyrics/text for lyric-capable models.
    /// </summary>
    [JsonPropertyName("lyrics_prompt")]
    public string? LyricsPrompt { get; set; }

    /// <summary>
    /// Optional duration hint in seconds.
    /// </summary>
    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }

    /// <summary>
    /// Optional instrumental toggle.
    /// </summary>
    [JsonPropertyName("force_instrumental")]
    public bool? ForceInstrumental { get; set; }

    /// <summary>
    /// When enabled, auto-generates lyrics from the prompt.
    /// </summary>
    [JsonPropertyName("lyrics_optimizer")]
    public bool? LyricsOptimizer { get; set; }

    /// <summary>
    /// Render the clip so its end splices back into its start without an audible seam.
    /// </summary>
    [JsonPropertyName("loop")]
    public bool? Loop { get; set; }

    /// <summary>
    /// Optional voice selection for voice-enabled models.
    /// </summary>
    [JsonPropertyName("voice")]
    public string? Voice { get; set; }

    /// <summary>
    /// Optional ISO 639-1 language code.
    /// </summary>
    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Optional audio speed multiplier (0.25-4).
    /// </summary>
    [JsonPropertyName("speed")]
    public double? Speed { get; set; }
}

/// <summary>
/// Response from queuing an audio generation job.
/// </summary>
public class QueueAudioResponse : BaseResponse
{
    /// <summary>
    /// The unique identifier for the queued audio generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// The model used for audio generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The status of the audio generation request.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Estimated wait time in seconds.
    /// </summary>
    [JsonPropertyName("estimated_wait_seconds")]
    public int? EstimatedWaitSeconds { get; set; }
}

/// <summary>
/// Request to retrieve an audio generation result.
/// </summary>
public class RetrieveAudioRequest
{
    /// <summary>
    /// The ID of the model used for audio generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the audio generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// If true, the audio media will be deleted from storage after the request is completed.
    /// </summary>
    [JsonPropertyName("delete_media_on_completion")]
    public bool? DeleteMediaOnCompletion { get; set; }
}

/// <summary>
/// Response from retrieving an audio generation result.
/// </summary>
public class RetrieveAudioResponse : BaseResponse
{
    /// <summary>
    /// The status of the audio generation (e.g., "processing", "completed", "failed").
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The queue ID of the audio generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// The model used for audio generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The URL to download the generated audio (when completed).
    /// </summary>
    [JsonPropertyName("audio_url")]
    public string? AudioUrl { get; set; }

    /// <summary>
    /// The base64-encoded audio data (when completed, if requested).
    /// </summary>
    [JsonPropertyName("audio_base64")]
    public string? AudioBase64 { get; set; }

    /// <summary>
    /// Progress percentage (0-100) when processing.
    /// </summary>
    [JsonPropertyName("progress")]
    public int? Progress { get; set; }

    /// <summary>
    /// Estimated remaining time in seconds.
    /// </summary>
    [JsonPropertyName("estimated_remaining_seconds")]
    public int? EstimatedRemainingSeconds { get; set; }

    /// <summary>
    /// Error message if the generation failed.
    /// </summary>
    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Request to mark an audio generation as complete and delete media from storage.
/// </summary>
public class CompleteAudioRequest
{
    /// <summary>
    /// The ID of the model used for audio generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the audio generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;
}

/// <summary>
/// Response from completing an audio generation request.
/// </summary>
public class CompleteAudioResponse : BaseResponse
{
    /// <summary>
    /// Whether the completion was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Message describing the result.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

/// <summary>
/// Request to get a price quote for audio generation.
/// </summary>
public class QuoteAudioRequest
{
    /// <summary>
    /// The model to get a price quote for.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(MusicModelJsonConverter))]
    public MusicModel Model { get; set; }

    /// <summary>
    /// Optional duration hint in seconds.
    /// </summary>
    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }

    /// <summary>
    /// Optional character count for character-based pricing models.
    /// </summary>
    [JsonPropertyName("character_count")]
    public int? CharacterCount { get; set; }
}

/// <summary>
/// Response from quoting an audio generation request.
/// </summary>
public class QuoteAudioResponse : BaseResponse
{
    /// <summary>
    /// The estimated price in USD for the audio generation.
    /// </summary>
    [JsonPropertyName("price_usd")]
    public decimal PriceUsd { get; set; }

    /// <summary>
    /// The estimated price in DIEM for the audio generation.
    /// </summary>
    [JsonPropertyName("price_diem")]
    public decimal? PriceDiem { get; set; }

    /// <summary>
    /// The model that would be used.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The duration of the audio.
    /// </summary>
    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }
}

/// <summary>
/// Request to transcribe audio to text.
/// </summary>
public class CreateTranscriptionRequest
{
    /// <summary>
    /// The audio file data (binary). Supported formats: WAV, WAVE, FLAC, M4A, AAC, MP4, MP3, OGG, OGA, WEBM.
    /// </summary>
    [JsonPropertyName("file")]
    public byte[] File { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// The filename of the audio file.
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; set; } = "audio.mp3";

    /// <summary>
    /// The model to use for transcription.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(AsrModelJsonConverter))]
    public AsrModel Model { get; set; } = AsrModel.ParakeetTdt06bV3;

    /// <summary>
    /// The format of the transcript output (json, text).
    /// </summary>
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }

    /// <summary>
    /// Whether to include timestamps in the response.
    /// </summary>
    [JsonPropertyName("timestamps")]
    public bool? Timestamps { get; set; }

    /// <summary>
    /// ISO 639-1 language code (e.g., "en"). Optional - auto-detected if not provided.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }
}

/// <summary>
/// Response from transcribing audio to text.
/// </summary>
public class CreateTranscriptionResponse : BaseResponse
{
    /// <summary>
    /// The transcribed text.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The detected language of the audio.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// The duration of the audio in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public double? Duration { get; set; }
}

/// <summary>
/// Request to create a cloned voice from an audio sample.
/// </summary>
public class CreateClonedVoiceRequest
{
    /// <summary>
    /// The voice sample audio file (binary). Recommended: a clean speech recording of at least 5-10 seconds.
    /// </summary>
    [JsonPropertyName("file")]
    public byte[] File { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// The filename of the voice sample.
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; set; } = "voice.mp3";

    /// <summary>
    /// The Venice TTS model the cloned voice will be paired with.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(TextToSpeechModelJsonConverter))]
    public TextToSpeechModel Model { get; set; } = TextToSpeechModel.TtsChatterboxHd;
}

/// <summary>
/// Response from creating a cloned voice.
/// </summary>
public class CreateClonedVoiceResponse : BaseResponse
{
    /// <summary>
    /// The cloned voice handle. Pass this to POST /api/v1/audio/speech as the voice parameter.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The model the cloned voice is paired with.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
}
