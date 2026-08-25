using VeniceAI.SDK.Models.Responses;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for the Responses API service.
/// </summary>
public interface IResponsesService
{
    /// <summary>
    /// Creates a response using the Responses API (Alpha).
    /// </summary>
    /// <param name="request">The responses request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The responses response.</returns>
    Task<ResponsesResponse> CreateResponseAsync(ResponsesRequest request, CancellationToken cancellationToken = default);
}
