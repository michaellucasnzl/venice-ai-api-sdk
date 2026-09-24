using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Decisions;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for the Decisions API (Beta) using the Venice AI API.
/// </summary>
public class DecisionsService : BaseHttpService, IDecisionsService
{
    /// <summary>
    /// Initializes a new instance of the DecisionsService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public DecisionsService(HttpClient httpClient, string apiKey, ILogger<DecisionsService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Evaluates a state against a map of typed questions and returns structured answers.
    /// </summary>
    /// <param name="request">The decision request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The structured answers.</returns>
    public Task<DecisionResponse> CreateDecisionAsync(
        DecisionRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendAsync("decisions", request, cancellationToken);
    }

    /// <summary>
    /// Alias of <see cref="CreateDecisionAsync"/> mirroring the upstream TypeSafe path (/api/v1/systemone).
    /// </summary>
    /// <param name="request">The decision request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The structured answers.</returns>
    public Task<DecisionResponse> CreateDecisionSystemOneAsync(
        DecisionRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendAsync("systemone", request, cancellationToken);
    }

    private async Task<DecisionResponse> SendAsync(
        string endpoint,
        DecisionRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (request.State is null)
            throw new ArgumentException("State is required", nameof(request));

        if (request.Questions.Count == 0)
            throw new ArgumentException("At least one question is required", nameof(request));

        try
        {
            var response = await PostAsync<DecisionRequest, DecisionResponse>(
                endpoint,
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during decision evaluation: {ex.Message}", ex);
        }
    }
}
