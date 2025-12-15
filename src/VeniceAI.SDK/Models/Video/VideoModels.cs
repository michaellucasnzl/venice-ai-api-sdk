using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Video;

/// <summary>
/// Request to queue a video generation job.
/// </summary>
public class QueueVideoRequest
{
    /// <summary>
    /// The model to use for video generation.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonConverter(typeof(VideoModelJsonConverter))]
    public VideoModel Model { get; set; }

    /// <summary>
    /// The prompt to use for video generation. Maximum 2500 characters.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The negative prompt to use for video generation. Maximum 2500 characters.
    /// </summary>
    [JsonPropertyName("negative_prompt")]
    public string? NegativePrompt { get; set; }

    /// <summary>
    /// The duration of the video to generate (e.g., "5s", "10s").
    /// </summary>
    [JsonPropertyName("duration")]
    public string Duration { get; set; } = "5s";

    /// <summary>
    /// The aspect ratio of the video to generate (e.g., "16:9", "9:16", "1:1").
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public string? AspectRatio { get; set; }

    /// <summary>
    /// The resolution of the video to generate (e.g., "1080p", "720p", "480p").
    /// </summary>
    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    /// <summary>
    /// For models which support audio generation, indicates if audio should be generated. Defaults to true.
    /// </summary>
    [JsonPropertyName("audio")]
    public bool? Audio { get; set; }

    /// <summary>
    /// For image to video models, the reference image URL or data URL.
    /// Must start with "http://", "https://", or "data:".
    /// </summary>
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}

/// <summary>
/// Response from queuing a video generation job.
/// </summary>
public class QueueVideoResponse : BaseResponse
{
    /// <summary>
    /// The unique identifier for the queued video generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// The model used for video generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The status of the video generation request.
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
/// Request to retrieve a video generation result.
/// </summary>
public class RetrieveVideoRequest
{
    /// <summary>
    /// The ID of the model used for video generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the video generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// If true, the video media will be deleted from storage after the request is completed.
    /// </summary>
    [JsonPropertyName("delete_media_on_completion")]
    public bool? DeleteMediaOnCompletion { get; set; }
}

/// <summary>
/// Response from retrieving a video generation result.
/// </summary>
public class RetrieveVideoResponse : BaseResponse
{
    /// <summary>
    /// The status of the video generation (e.g., "processing", "completed", "failed").
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The queue ID of the video generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;

    /// <summary>
    /// The model used for video generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The URL to download the generated video (when completed).
    /// </summary>
    [JsonPropertyName("video_url")]
    public string? VideoUrl { get; set; }

    /// <summary>
    /// The base64-encoded video data (when completed, if requested).
    /// </summary>
    [JsonPropertyName("video_base64")]
    public string? VideoBase64 { get; set; }

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
/// Request to mark a video generation as complete and delete media from storage.
/// </summary>
public class CompleteVideoRequest
{
    /// <summary>
    /// The ID of the model used for video generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the video generation request.
    /// </summary>
    [JsonPropertyName("queue_id")]
    public string QueueId { get; set; } = string.Empty;
}

/// <summary>
/// Response from completing a video generation request.
/// </summary>
public class CompleteVideoResponse : BaseResponse
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
/// Response from quoting a video generation request.
/// </summary>
public class QuoteVideoResponse : BaseResponse
{
    /// <summary>
    /// The estimated price in USD for the video generation.
    /// </summary>
    [JsonPropertyName("price_usd")]
    public decimal PriceUsd { get; set; }

    /// <summary>
    /// The estimated price in DIEM for the video generation.
    /// </summary>
    [JsonPropertyName("price_diem")]
    public decimal? PriceDiem { get; set; }

    /// <summary>
    /// The model that would be used.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The duration of the video.
    /// </summary>
    [JsonPropertyName("duration")]
    public string Duration { get; set; } = string.Empty;

    /// <summary>
    /// The resolution of the video.
    /// </summary>
    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }
}
