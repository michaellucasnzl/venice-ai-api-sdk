using VeniceAI.SDK.Models.Embeddings;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for embedding services.
/// </summary>
public interface IEmbeddingService
{
    /// <summary>
    /// Creates embeddings for the given input.
    /// </summary>
    /// <param name="request">The embedding request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The embedding response.</returns>
    Task<CreateEmbeddingResponse> CreateEmbeddingAsync(CreateEmbeddingRequest request, CancellationToken cancellationToken = default);
}
