using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Embeddings;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for embedding operations.
/// </summary>
public class EmbeddingService : BaseService, IEmbeddingService
{
    /// <summary>
    /// Initializes a new instance of the EmbeddingService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    public EmbeddingService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger<EmbeddingService> logger)
        : base(httpClient, options, logger)
    {
    }

    /// <inheritdoc />
    public async Task<CreateEmbeddingResponse> CreateEmbeddingAsync(CreateEmbeddingRequest request, CancellationToken cancellationToken = default)
    {
        return await SendPostRequestAsync<CreateEmbeddingResponse>("/embeddings", request, cancellationToken);
    }
}
