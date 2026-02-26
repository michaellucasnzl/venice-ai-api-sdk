using System.Text.Json.Serialization;

namespace VeniceAI.SDK.Models.Common;

/// <summary>
/// Standard error response from the Venice AI API.
/// </summary>
public class StandardError
{
    /// <summary>
    /// A description of the error.
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
}

/// <summary>
/// Detailed error response from the Venice AI API.
/// </summary>
public class DetailedError
{
    /// <summary>
    /// Details about the incorrect input.
    /// </summary>
    [JsonPropertyName("details")]
    public Dictionary<string, object> Details { get; set; } = new();

    /// <summary>
    /// A description of the error.
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
}

/// <summary>
/// Base response class for API responses.
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Indicates if the request was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Error information if the request failed.
    /// </summary>
    public StandardError? Error { get; set; }

    /// <summary>
    /// HTTP status code of the response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Raw response content.
    /// </summary>
    public string? RawContent { get; set; }
}
