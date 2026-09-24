using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Audio;

/// <summary>
/// Request to queue a speech-to-speech voice conversion.
/// Supply the source recording either as a URL (<see cref="AudioUrl"/>, JSON requests) or
/// as an uploaded file (<see cref="File"/>, multipart requests), but not both.
/// </summary>
public class QueueVoiceChangerRequest
{
    /// <summary>
    /// The voice-changer model to run (e.g. "elevenlabs-voice-changer").
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The source recording bytes (multipart/form-data, field name "file").
    /// Mutually exclusive with <see cref="AudioUrl"/>.
    /// </summary>
    [JsonPropertyName("file")]
    public byte[]? File { get; set; }

    /// <summary>
    /// The filename sent with <see cref="File"/> when uploading.
    /// </summary>
    [JsonIgnore]
    public string Filename { get; set; } = "source.mp3";

    /// <summary>
    /// Publicly reachable http(s) URL of the source recording.
    /// Mutually exclusive with <see cref="File"/>.
    /// </summary>
    [JsonPropertyName("audio_url")]
    public string? AudioUrl { get; set; }

    /// <summary>
    /// The target voice: one of the model's voices, or a provider Voice ID when supported.
    /// Defaults to the model's default voice.
    /// </summary>
    [JsonPropertyName("voice")]
    public string? Voice { get; set; }

    /// <summary>
    /// Strip background noise from the source recording before conversion.
    /// </summary>
    [JsonPropertyName("remove_background_noise")]
    public bool? RemoveBackgroundNoise { get; set; }

    /// <summary>
    /// Seed for reproducible output. Omit for a non-deterministic result.
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }
}

/// <summary>
/// Response from queuing a voice conversion.
/// </summary>
public class QueueVoiceChangerResponse : BaseResponse
{
    /// <summary>
    /// The model that is running the conversion.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Pass this to retrieve and complete the conversion.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// Always "QUEUED": the provider has accepted the job.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Length of the source recording in seconds, measured server-side (the billed quantity).
    /// </summary>
    [JsonPropertyName("duration_seconds")]
    public double? DurationSeconds { get; set; }
}

/// <summary>
/// Request to poll a queued voice conversion.
/// </summary>
public class RetrieveVoiceChangerRequest
{
    /// <summary>
    /// The model that is running the conversion.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The queue ID returned by the queue request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// Release the provider-held media as soon as this call returns the audio,
    /// making a separate complete call unnecessary.
    /// </summary>
    [JsonPropertyName("delete_media_on_completion")]
    public bool? DeleteMediaOnCompletion { get; set; }
}

/// <summary>
/// Response from polling a voice conversion. While processing, <see cref="Status"/> is
/// "PROCESSING"; once finished, <see cref="AudioContent"/> carries the converted audio bytes.
/// </summary>
public class RetrieveVoiceChangerResponse : BaseResponse
{
    /// <summary>
    /// "PROCESSING" while the conversion is still running; null when audio is returned.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Recent average end-to-end time for this model in milliseconds (processing only).
    /// </summary>
    [JsonPropertyName("average_execution_time")]
    public double? AverageExecutionTime { get; set; }

    /// <summary>
    /// Milliseconds elapsed since the conversion was queued (processing only).
    /// </summary>
    [JsonPropertyName("execution_duration")]
    public double? ExecutionDuration { get; set; }

    /// <summary>
    /// The converted audio bytes (completed conversions only).
    /// </summary>
    [JsonIgnore]
    public byte[]? AudioContent { get; set; }

    /// <summary>
    /// The content type of <see cref="AudioContent"/> (completed conversions only).
    /// </summary>
    [JsonIgnore]
    public string? ContentType { get; set; }
}

/// <summary>
/// Request to release the provider-held media for a finished voice conversion.
/// </summary>
public class CompleteVoiceChangerRequest
{
    /// <summary>
    /// The model that is running the conversion.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The queue ID returned by the queue request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;
}

/// <summary>
/// Response from completing a voice conversion.
/// </summary>
public class CompleteVoiceChangerResponse : BaseResponse
{
    /// <summary>
    /// Whether the provider-held media for this conversion was released.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}
