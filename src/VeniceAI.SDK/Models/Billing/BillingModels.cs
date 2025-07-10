using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Billing;

/// <summary>
/// Request for billing usage information.
/// </summary>
public class BillingUsageRequest
{
    /// <summary>
    /// Start date for filtering records (ISO 8601).
    /// </summary>
    [JsonPropertyName("startDate")]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End date for filtering records (ISO 8601).
    /// </summary>
    [JsonPropertyName("endDate")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Filter by currency.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page number for pagination.
    /// </summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>
    /// Sort order for createdAt field.
    /// </summary>
    [JsonPropertyName("sortOrder")]
    public string? SortOrder { get; set; }
}

/// <summary>
/// Response from billing usage API.
/// </summary>
public class BillingUsageResponse : BaseResponse
{
    /// <summary>
    /// List of billing usage entries.
    /// </summary>
    [JsonPropertyName("data")]
    public List<BillingUsageEntry> Data { get; set; } = new();

    /// <summary>
    /// Pagination information.
    /// </summary>
    [JsonPropertyName("pagination")]
    public PaginationInfo Pagination { get; set; } = new();
}

/// <summary>
/// Billing usage entry.
/// </summary>
public class BillingUsageEntry
{
    /// <summary>
    /// The total amount charged for the billing usage entry.
    /// </summary>
    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    /// <summary>
    /// The currency charged for the billing usage entry.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Details about the related inference request, if applicable.
    /// </summary>
    [JsonPropertyName("inferenceDetails")]
    public InferenceDetails? InferenceDetails { get; set; }

    /// <summary>
    /// Notes about the billing usage entry.
    /// </summary>
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// The price per unit in USD.
    /// </summary>
    [JsonPropertyName("pricePerUnitUsd")]
    public double PricePerUnitUsd { get; set; }

    /// <summary>
    /// The product associated with the billing usage entry.
    /// </summary>
    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp the billing usage entry was created.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// The number of units consumed.
    /// </summary>
    [JsonPropertyName("units")]
    public double Units { get; set; }
}

/// <summary>
/// Details about the related inference request.
/// </summary>
public class InferenceDetails
{
    /// <summary>
    /// Number of tokens used in the completion.
    /// </summary>
    [JsonPropertyName("completionTokens")]
    public int? CompletionTokens { get; set; }

    /// <summary>
    /// Time taken for inference execution in milliseconds.
    /// </summary>
    [JsonPropertyName("inferenceExecutionTime")]
    public double? InferenceExecutionTime { get; set; }

    /// <summary>
    /// Number of tokens requested in the prompt.
    /// </summary>
    [JsonPropertyName("promptTokens")]
    public int? PromptTokens { get; set; }

    /// <summary>
    /// Unique identifier for the inference request.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }
}

/// <summary>
/// Pagination information.
/// </summary>
public class PaginationInfo
{
    /// <summary>
    /// Number of items per page.
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    /// <summary>
    /// Current page number.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    /// Total number of items.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}

/// <summary>
/// Currency options for billing.
/// </summary>
public static class Currency
{
    public const string USD = "USD";
    public const string VCU = "VCU";
    public const string DIEM = "DIEM";
}

/// <summary>
/// Sort order options.
/// </summary>
public static class SortOrder
{
    public const string Ascending = "asc";
    public const string Descending = "desc";
}
