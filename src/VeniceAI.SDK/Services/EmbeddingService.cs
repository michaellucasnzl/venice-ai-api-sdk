using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Services.Interfaces;
using CreateEmbeddingRequest = VeniceAI.SDK.Models.Embeddings.CreateEmbeddingRequest;
using CreateEmbeddingResponse = VeniceAI.SDK.Models.Embeddings.CreateEmbeddingResponse;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for embedding operations using the Venice AI API.
/// </summary>
public class EmbeddingService : IEmbeddingService
{
    private readonly IVeniceAIGeneratedClient _generatedClient;

    /// <summary>
    /// Initializes a new instance of the EmbeddingService class.
    /// </summary>
    /// <param name="generatedClient">The generated Venice AI client.</param>
    public EmbeddingService(IVeniceAIGeneratedClient generatedClient)
    {
        _generatedClient = generatedClient ?? throw new ArgumentNullException(nameof(generatedClient));
    }

    /// <summary>
    /// Creates embeddings for the given input.
    /// </summary>
    /// <param name="request">The embedding request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The embedding response.</returns>
    public async Task<CreateEmbeddingResponse> CreateEmbeddingAsync(CreateEmbeddingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Model))
            throw new ArgumentException("Model is required", nameof(request));

        if (request.Input == null)
            throw new ArgumentException("Input is required", nameof(request));

        try
        {
            // Convert SDK request to generated client format
            var generatedRequest = request.ToGeneratedRequest();
            
            // Call the generated client
            var response = await _generatedClient.CreateEmbeddingAsync(
                "gzip, br", // Accept compression
                generatedRequest,
                cancellationToken);

            // Convert back to SDK format
            return response.ToSdkEmbeddingResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Embedding creation failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during embedding creation: {ex.Message}", ex);
        }
    }
}
