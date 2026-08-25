using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Responses;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for the Responses API (Alpha) using the Venice AI API.
/// </summary>
public class ResponsesService : BaseHttpService, IResponsesService
{
    /// <summary>
    /// Initializes a new instance of the ResponsesService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public ResponsesService(HttpClient httpClient, string apiKey, ILogger<ResponsesService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Creates a response using the Responses API (Alpha).
    /// </summary>
    /// <param name="request">The responses request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The responses response.</returns>
    public async Task<ResponsesResponse> CreateResponseAsync(
        ResponsesRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var response = await PostAsync<ResponsesRequest, ResponsesResponse>(
                "responses",
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
            throw new VeniceAIException($"Unexpected error during response creation: {ex.Message}", ex);
        }
    }
}
