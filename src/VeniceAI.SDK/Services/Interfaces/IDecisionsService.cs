using VeniceAI.SDK.Models.Decisions;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for the Decisions API (Beta).
/// </summary>
public interface IDecisionsService
{
    /// <summary>
    /// Evaluates a state against a map of typed questions and returns structured answers.
    /// </summary>
    /// <param name="request">The decision request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The structured answers.</returns>
    Task<DecisionResponse> CreateDecisionAsync(DecisionRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Alias of <see cref="CreateDecisionAsync"/> mirroring the upstream TypeSafe path (/api/v1/systemone).
    /// </summary>
    /// <param name="request">The decision request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The structured answers.</returns>
    Task<DecisionResponse> CreateDecisionSystemOneAsync(DecisionRequest request, CancellationToken cancellationToken = default);
}
