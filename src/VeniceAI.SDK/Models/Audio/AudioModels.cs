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
