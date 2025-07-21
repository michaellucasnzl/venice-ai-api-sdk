using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Embeddings;
using VeniceAI.SDK.Extensions;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for embedding operations using the Venice AI API.
/// </summary>
public class EmbeddingService : BaseHttpService, IEmbeddingService
{
    /// <summary>
    /// Initializes a new instance of the EmbeddingService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public EmbeddingService(HttpClient httpClient, string apiKey, ILogger<EmbeddingService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Creates embeddings for the given input.
    /// </summary>
    /// <param name="request">The embedding creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The embedding creation response.</returns>
    public async Task<CreateEmbeddingResponse> CreateEmbeddingAsync(
        CreateEmbeddingRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Validate the model if provided
        if (!string.IsNullOrEmpty(request.Model))
        {
            ModelEnumExtensions.ValidateEmbeddingModel(request.Model);
        }

        try
        {
            var response = await PostAsync<CreateEmbeddingRequest, CreateEmbeddingResponse>("embeddings", request, cancellationToken);
            response.IsSuccess = true;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Error creating embeddings: {ex.Message}", ex);
        }
    }
}
