using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Services.Interfaces;
using GenerateImageRequest = VeniceAI.SDK.Models.Images.GenerateImageRequest;
using SimpleGenerateImageRequest = VeniceAI.SDK.Models.Images.SimpleGenerateImageRequest;
using UpscaleImageRequest = VeniceAI.SDK.Models.Images.UpscaleImageRequest;
using EditImageRequest = VeniceAI.SDK.Models.Images.EditImageRequest;
using ImageGenerationResponse = VeniceAI.SDK.Models.Images.ImageGenerationResponse;
using ImageStylesResponse = VeniceAI.SDK.Models.Images.ImageStylesResponse;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for image generation operations using the Venice AI API.
/// </summary>
public class ImageService : IImageService
{
    private readonly IVeniceAIGeneratedClient _generatedClient;

    /// <summary>
    /// Initializes a new instance of the ImageService class.
    /// </summary>
    /// <param name="generatedClient">The generated Venice AI client.</param>
    public ImageService(IVeniceAIGeneratedClient generatedClient)
    {
        _generatedClient = generatedClient ?? throw new ArgumentNullException(nameof(generatedClient));
    }

    /// <summary>
    /// Generates an image based on the given prompt.
    /// </summary>
    /// <param name="request">The image generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The generated image response.</returns>
    public async Task<ImageGenerationResponse> GenerateImageAsync(GenerateImageRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        try
        {
            // Convert SDK request to generated client format
            var generatedRequest = request.ToGeneratedRequest();
            
            // Call the generated client
            var response = await _generatedClient.GenerateImageAsync(
                "gzip, br", // Accept compression
                generatedRequest,
                cancellationToken);

            // Convert back to SDK format
            return response.ToSdkImageResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Image generation failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during image generation: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Generates an image using the simple (OpenAI-compatible) endpoint.
    /// </summary>
    /// <param name="request">The simple image generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The generated image response.</returns>
    public async Task<ImageGenerationResponse> GenerateImageSimpleAsync(SimpleGenerateImageRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        try
        {
            // Convert SDK request to generated client format
            var generatedRequest = request.ToGeneratedRequest();
            
            // Call the generated client
            var response = await _generatedClient.SimpleGenerateImageAsync(
                "gzip, br", // Accept compression
                generatedRequest,
                cancellationToken);

            // Convert back to SDK format
            return response.ToSdkImageResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Simple image generation failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during simple image generation: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Upscales an image.
    /// </summary>
    /// <param name="request">The upscale image request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The upscaled image response.</returns>
    public async Task<ImageGenerationResponse> UpscaleImageAsync(UpscaleImageRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Image))
            throw new ArgumentException("Image is required", nameof(request));

        try
        {
            // Note: Generated client has complex signature for upscale
            // We'll need to map the SDK request properties to the generated client parameters
            // For now, using a simplified approach
            await Task.Delay(0, cancellationToken); // Suppress warning
            throw new NotImplementedException("Upscale image functionality needs proper parameter mapping to generated client");
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Image upscaling failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during image upscaling: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Edits an image based on the given prompt.
    /// </summary>
    /// <param name="request">The edit image request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The edited image response.</returns>
    public async Task<ImageGenerationResponse> EditImageAsync(EditImageRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        try
        {
            // Note: Generated client has complex signature for edit
            // We'll need to map the SDK request properties to the generated client parameters
            // For now, using a simplified approach
            await Task.Delay(0, cancellationToken); // Suppress warning
            throw new NotImplementedException("Edit image functionality needs proper parameter mapping to generated client");
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Image editing failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error during image editing: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets available image styles.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The available image styles.</returns>
    public async Task<ImageStylesResponse> GetImageStylesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Call the generated client
            var response = await _generatedClient.StylesAsync(cancellationToken);

            // Convert back to SDK format
            return response.ToSdkImageStylesResponse();
        }
        catch (ApiException ex)
        {
            throw new VeniceAIException($"Getting image styles failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting image styles: {ex.Message}", ex);
        }
    }
}
