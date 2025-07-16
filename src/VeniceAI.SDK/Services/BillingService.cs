using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Billing;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for billing operations using the Venice AI API.
/// </summary>
public class BillingService : BaseHttpService, IBillingService
{
    /// <summary>
    /// Initializes a new instance of the BillingService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    public BillingService(HttpClient httpClient, string apiKey) : base(httpClient, apiKey)
    {
    }

    /// <summary>
    /// Gets billing usage information.
    /// </summary>
    /// <param name="request">The billing usage request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The billing usage response.</returns>
    public async Task<BillingUsageResponse> GetBillingUsageAsync(BillingUsageRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            return await GetAsync<BillingUsageResponse>("billing/usage", cancellationToken);
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Error getting billing usage: {ex.Message}", ex);
        }
    }
}
