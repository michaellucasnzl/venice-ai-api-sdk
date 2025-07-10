using VeniceAI.SDK.Models.Models;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for model services.
/// </summary>
public interface IModelService
{
    /// <summary>
    /// Gets a list of available models.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The list of available models.</returns>
    Task<ModelsResponse> GetModelsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific model by ID.
    /// </summary>
    /// <param name="modelId">The model ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model information.</returns>
    Task<Model> GetModelAsync(string modelId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets model traits mapping.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model traits mapping.</returns>
    Task<ModelTraitsResponse> GetModelTraitsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets model compatibility mapping.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model compatibility mapping.</returns>
    Task<ModelCompatibilityResponse> GetModelCompatibilityAsync(CancellationToken cancellationToken = default);
}
