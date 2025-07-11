using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Models;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for model operations.
/// </summary>
public class ModelService : BaseService, IModelService
{
    /// <summary>
    /// Initializes a new instance of the ModelService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    public ModelService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger<ModelService> logger)
        : base(httpClient, options, logger)
    {
    }

    /// <inheritdoc />
    public async Task<ModelsResponse> GetModelsAsync(CancellationToken cancellationToken = default)
    {
        return await SendGetRequestAsync<ModelsResponse>("models", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Model> GetModelAsync(string modelId, CancellationToken cancellationToken = default)
    {
        var response = await SendGetRequestAsync<ModelsResponse>($"models/{modelId}", cancellationToken);
        return response.Data.FirstOrDefault() ?? new Model { Id = modelId };
    }

    /// <inheritdoc />
    public async Task<ModelTraitsResponse> GetModelTraitsAsync(CancellationToken cancellationToken = default)
    {
        return await SendGetRequestAsync<ModelTraitsResponse>("models/traits", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelCompatibilityResponse> GetModelCompatibilityAsync(CancellationToken cancellationToken = default)
    {
        return await SendGetRequestAsync<ModelCompatibilityResponse>("models/compatibility_mapping", cancellationToken);
    }
}
