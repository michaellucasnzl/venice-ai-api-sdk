using System.Text.Json.Serialization;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Models;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for model operations using the Venice AI API.
/// </summary>
public class ModelService : BaseHttpService, IModelService
{
    /// <summary>
    /// Initializes a new instance of the ModelService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    public ModelService(HttpClient httpClient, string apiKey) : base(httpClient, apiKey)
    {
    }

    /// <summary>
    /// Gets the list of available models.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The models response.</returns>
    public async Task<ModelsResponse> GetModelsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var apiResponse = await GetAsync<ModelsApiResponse>("models", cancellationToken);

            return new ModelsResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Object = apiResponse.Object ?? "list",
                Data = apiResponse.Data?.Select(m => new Model
                {
                    Id = m.Id ?? string.Empty,
                    Object = m.Object ?? "model",
                    Created = m.Created,
                    OwnedBy = m.OwnedBy ?? string.Empty,
                    Type = m.Type ?? string.Empty,
                    ModelSpec = m.ModelSpec ?? new ModelSpec()
                }).ToList() ?? new List<Model>()
            };
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting models: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets model traits.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model traits response.</returns>
    public async Task<ModelTraitsResponse> GetModelTraitsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await GetAsync<Models.Models.ModelTraitsApiResponse>("models/traits", cancellationToken);

            return new ModelTraitsResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Traits = response?.Data ?? new Dictionary<string, string>()
            };
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting model traits: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets model compatibility.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The model compatibility response.</returns>
    public async Task<ModelCompatibilityResponse> GetModelCompatibilityAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await GetAsync<Models.Models.ModelCompatibilityApiResponse>("models/compatibility_mapping", cancellationToken);

            return new ModelCompatibilityResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Compatibility = response?.Data ?? new Dictionary<string, string>()
            };
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting model compatibility: {ex.Message}", ex);
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
        if (string.IsNullOrEmpty(modelId))
            throw new ArgumentException("Model ID is required", nameof(modelId));

        try
        {
            var apiResponse = await GetAsync<ModelApiResponse>($"models/{modelId}", cancellationToken);

            return new Model
            {
                Id = apiResponse.Id ?? string.Empty,
                Object = apiResponse.Object ?? "model",
                Created = apiResponse.Created,
                OwnedBy = apiResponse.OwnedBy ?? string.Empty,
                Type = apiResponse.Type ?? string.Empty,
                ModelSpec = apiResponse.ModelSpec ?? new ModelSpec()
            };
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting model: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Internal API response classes for models.
/// </summary>
internal class ModelsApiResponse
{
    public string? Object { get; set; }
    public List<ModelApiResponse>? Data { get; set; }
}

internal class ModelApiResponse
{
    public string? Id { get; set; }
    public string? Object { get; set; }
    public long Created { get; set; }
    public string? OwnedBy { get; set; }
    public string? Type { get; set; }
    [JsonPropertyName("model_spec")]
    public ModelSpec? ModelSpec { get; set; }
}
