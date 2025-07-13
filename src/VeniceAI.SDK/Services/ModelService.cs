using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Services.Interfaces;
using ModelsResponse = VeniceAI.SDK.Models.Models.ModelsResponse;
using Model = VeniceAI.SDK.Models.Models.Model;
using ModelTraitsResponse = VeniceAI.SDK.Models.Models.ModelTraitsResponse;
using ModelCompatibilityResponse = VeniceAI.SDK.Models.Models.ModelCompatibilityResponse;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for model operations using the Venice AI API.
/// </summary>
public class ModelService : IModelService
{
    private readonly IVeniceAIGeneratedClient _generatedClient;

    /// <summary>
    /// Initializes a new instance of the ModelService class.
    /// </summary>
    /// <param name="generatedClient">The generated Venice AI client.</param>
    public ModelService(IVeniceAIGeneratedClient generatedClient)
    {
        _generatedClient = generatedClient ?? throw new ArgumentNullException(nameof(generatedClient));
    }

    /// <summary>
    /// Gets a list of available models.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The list of available models.</returns>
    public async Task<ModelsResponse> GetModelsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Call the generated client - using null for Type parameter to get all models
            var response = await _generatedClient.ListModelsAsync(null, cancellationToken);

            // Convert back to SDK format
            return response.ToSdkModelsResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Getting models failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting models: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a specific model by ID.
    /// </summary>
    /// <param name="modelId">The model ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model information.</returns>
    public async Task<Model> GetModelAsync(string modelId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(modelId);

        try
        {
            // For individual model, we'll get all models and filter
            // This is a limitation of the current generated client structure
            var response = await GetModelsAsync(cancellationToken);
            return response.Data.FirstOrDefault(m => m.Id == modelId) ?? new Model { Id = modelId };
        }
        catch (Exception ex) when (ex is not VeniceAIException)
        {
            throw new VeniceAIException($"Unexpected error getting model {modelId}: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets model traits mapping.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model traits mapping.</returns>
    public async Task<ModelTraitsResponse> GetModelTraitsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Call the generated client - using null for Type2 parameter to get all traits
            var response = await _generatedClient.ListModelTraitsAsync(null, cancellationToken);

            // Convert back to SDK format
            return response.ToSdkModelTraitsResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Getting model traits failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting model traits: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets model compatibility mapping.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model compatibility mapping.</returns>
    public async Task<ModelCompatibilityResponse> GetModelCompatibilityAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Call the generated client - using null for Type3 parameter to get all compatibility mappings
            var response = await _generatedClient.ListModelCompatibilityMappingAsync(null, cancellationToken);

            // Convert back to SDK format
            return response.ToSdkModelCompatibilityResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Getting model compatibility failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting model compatibility: {ex.Message}", ex);
        }
    }
}
