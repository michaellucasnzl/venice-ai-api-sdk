using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Billing;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for billing operations.
/// </summary>
public class BillingService : BaseService, IBillingService
{
    /// <summary>
    /// Initializes a new instance of the BillingService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    public BillingService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger<BillingService> logger)
        : base(httpClient, options, logger)
    {
    }

    /// <inheritdoc />
    public async Task<BillingUsageResponse> GetBillingUsageAsync(BillingUsageRequest request, CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();

        if (request.StartDate.HasValue)
            queryParams.Add($"startDate={request.StartDate.Value:yyyy-MM-ddTHH:mm:ssZ}");
        
        if (request.EndDate.HasValue)
            queryParams.Add($"endDate={request.EndDate.Value:yyyy-MM-ddTHH:mm:ssZ}");
        
        if (!string.IsNullOrEmpty(request.Currency))
            queryParams.Add($"currency={request.Currency}");
        
        if (request.Limit.HasValue)
            queryParams.Add($"limit={request.Limit.Value}");
        
        if (request.Page.HasValue)
            queryParams.Add($"page={request.Page.Value}");
        
        if (!string.IsNullOrEmpty(request.SortOrder))
            queryParams.Add($"sortOrder={request.SortOrder}");

        var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        var endpoint = $"/billing/usage{queryString}";

        return await SendGetRequestAsync<BillingUsageResponse>(endpoint, cancellationToken);
    }
}
