using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Images;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for image generation operations.
/// </summary>
public class ImageService : BaseService, IImageService
{
    /// <summary>
    /// Initializes a new instance of the ImageService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    public ImageService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger<ImageService> logger)
        : base(httpClient, options, logger)
    {
    }

    /// <inheritdoc />
    public async Task<ImageGenerationResponse> GenerateImageAsync(GenerateImageRequest request, CancellationToken cancellationToken = default)
    {
        return await SendPostRequestAsync<ImageGenerationResponse>("/image/generate", request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ImageGenerationResponse> GenerateImageSimpleAsync(SimpleGenerateImageRequest request, CancellationToken cancellationToken = default)
    {
        return await SendPostRequestAsync<ImageGenerationResponse>("/images/generations", request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ImageGenerationResponse> UpscaleImageAsync(UpscaleImageRequest request, CancellationToken cancellationToken = default)
    {
        return await SendPostRequestAsync<ImageGenerationResponse>("/image/upscale", request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ImageGenerationResponse> EditImageAsync(EditImageRequest request, CancellationToken cancellationToken = default)
    {
        return await SendPostRequestAsync<ImageGenerationResponse>("/image/edit", request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ImageStylesResponse> GetImageStylesAsync(CancellationToken cancellationToken = default)
    {
        return await SendGetRequestAsync<ImageStylesResponse>("/image/styles", cancellationToken);
    }
}
