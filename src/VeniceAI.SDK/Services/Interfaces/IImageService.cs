using VeniceAI.SDK.Models.Images;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for image generation services.
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Generates an image based on the given prompt.
    /// </summary>
    /// <param name="request">The image generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The generated image response.</returns>
    Task<ImageGenerationResponse> GenerateImageAsync(GenerateImageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates an image using the simple (OpenAI-compatible) endpoint.
    /// </summary>
    /// <param name="request">The simple image generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The generated image response.</returns>
    Task<ImageGenerationResponse> GenerateImageSimpleAsync(SimpleGenerateImageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Upscales an image.
    /// </summary>
    /// <param name="request">The upscale image request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The upscaled image response.</returns>
    Task<ImageGenerationResponse> UpscaleImageAsync(UpscaleImageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Edits an image based on the given prompt.
    /// </summary>
    /// <param name="request">The edit image request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The edited image response.</returns>
    Task<ImageGenerationResponse> EditImageAsync(EditImageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets available image styles.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The available image styles.</returns>
    Task<ImageStylesResponse> GetImageStylesAsync(CancellationToken cancellationToken = default);
}
