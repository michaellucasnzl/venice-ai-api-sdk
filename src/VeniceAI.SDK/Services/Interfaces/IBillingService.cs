using VeniceAI.SDK.Models.Billing;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for billing services.
/// </summary>
public interface IBillingService
{
    /// <summary>
    /// Gets billing usage information.
    /// </summary>
    /// <param name="request">The billing usage request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The billing usage response.</returns>
    Task<BillingUsageResponse> GetBillingUsageAsync(BillingUsageRequest request, CancellationToken cancellationToken = default);
}
