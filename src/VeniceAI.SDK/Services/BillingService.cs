using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Services.Interfaces;
using BillingUsageRequest = VeniceAI.SDK.Models.Billing.BillingUsageRequest;
using BillingUsageResponse = VeniceAI.SDK.Models.Billing.BillingUsageResponse;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for billing operations using the Venice AI API.
/// </summary>
public class BillingService : IBillingService
{
    private readonly IVeniceAIGeneratedClient _generatedClient;

    /// <summary>
    /// Initializes a new instance of the BillingService class.
    /// </summary>
    /// <param name="generatedClient">The generated Venice AI client.</param>
    public BillingService(IVeniceAIGeneratedClient generatedClient)
    {
        _generatedClient = generatedClient ?? throw new ArgumentNullException(nameof(generatedClient));
    }

    /// <summary>
    /// Gets billing usage data for the authenticated user.
    /// </summary>
    /// <param name="request">The billing usage request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The billing usage response.</returns>
    public async Task<BillingUsageResponse> GetBillingUsageAsync(BillingUsageRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            // Map SDK parameters to generated client parameters
            Generated.Currency? currency = null;
            if (!string.IsNullOrEmpty(request.Currency) && 
                Enum.TryParse<Generated.Currency>(request.Currency, true, out var currencyEnum))
            {
                currency = currencyEnum;
            }

            Generated.SortOrder? sortOrder = null;
            if (!string.IsNullOrEmpty(request.SortOrder) && 
                Enum.TryParse<Generated.SortOrder>(request.SortOrder, true, out var sortOrderEnum))
            {
                sortOrder = sortOrderEnum;
            }

            // Call the generated client
            var response = await _generatedClient.GetBillingUsageAsync(
                "application/json", // Accept header
                currency,
                request.EndDate,
                request.Limit,
                request.Page,
                sortOrder,
                request.StartDate,
                cancellationToken);

            // Convert back to SDK format
            return response.ToSdkBillingUsageResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Getting billing usage failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting billing usage: {ex.Message}", ex);
        }
    }
}
