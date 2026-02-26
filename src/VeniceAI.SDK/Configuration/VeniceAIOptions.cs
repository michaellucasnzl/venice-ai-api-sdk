using System.ComponentModel.DataAnnotations;

namespace VeniceAI.SDK.Configuration;

/// <summary>
/// Configuration options for the Venice AI API client.
/// Only the API key is required - all other settings are managed internally by the SDK.
/// </summary>
public class VeniceAIOptions
{
    /// <summary>
    /// The configuration section name for Venice AI options.
    /// </summary>
    public const string SectionName = "VeniceAI";

    /// <summary>
    /// The official Venice AI API base URL.
    /// This is managed internally by the SDK and cannot be overridden.
    /// </summary>
    internal const string OfficialApiBaseUrl = "https://api.venice.ai/api/v1/";

    /// <summary>
    /// The API key for authenticating with the Venice AI API.
    /// This is the only required user configuration.
    /// </summary>
    [Required]
    public string ApiKey { get; set; } = string.Empty;

    // All other settings are internal and managed by the SDK

    /// <summary>
    /// Gets the Venice AI API base URL. This cannot be overridden.
    /// </summary>
    internal static string BaseUrl => OfficialApiBaseUrl;

    /// <summary>
    /// Gets the default timeout for requests in seconds.
    /// </summary>
    internal static int DefaultTimeoutSeconds => 600; // 10 minutes

    /// <summary>
    /// Gets the maximum retry attempts for failed requests.
    /// </summary>
    internal static int MaxRetryAttempts => 3;

    /// <summary>
    /// Gets the delay between retry attempts in milliseconds.
    /// </summary>
    internal static int RetryDelayMs => 1000;
}
