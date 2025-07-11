using System.ComponentModel.DataAnnotations;

namespace VeniceAI.SDK.Configuration;

/// <summary>
/// Configuration options for the Venice AI API client.
/// </summary>
public class VeniceAIOptions
{
    /// <summary>
    /// The configuration section name for Venice AI options.
    /// </summary>
    public const string SectionName = "VeniceAI";

    /// <summary>
    /// The API key for authenticating with the Venice AI API.
    /// </summary>
    [Required]
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The base URL for the Venice AI API. Defaults to the official API endpoint.
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.venice.ai/api/v1/";

    /// <summary>
    /// The timeout for HTTP requests in seconds. Defaults to 300 seconds (5 minutes).
    /// </summary>
    public int TimeoutSeconds { get; set; } = 300;

    /// <summary>
    /// The maximum number of retry attempts for failed requests. Defaults to 3.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// The delay between retry attempts in milliseconds. Defaults to 1000ms.
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;

    /// <summary>
    /// Whether to enable request/response logging. Defaults to false.
    /// </summary>
    public bool EnableLogging { get; set; } = false;

    /// <summary>
    /// Custom HTTP headers to include with all requests.
    /// </summary>
    public Dictionary<string, string> CustomHeaders { get; set; } = new();
}
