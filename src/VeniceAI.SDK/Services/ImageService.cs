using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Images;
using VeniceAI.SDK.Models.Common;
using VeniceAI.SDK.Extensions;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for image operations using the Venice AI API.
/// </summary>
public class ImageService : BaseHttpService, IImageService
{
    /// <summary>
    /// Initializes a new instance of the ImageService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public ImageService(HttpClient httpClient, string apiKey, ILogger<ImageService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Generates an image from a prompt.
    /// </summary>
    /// <param name="request">The image generation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The image generation response.</returns>
    public async Task<ImageGenerationResponse> GenerateImageAsync(
        GenerateImageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        try
        {
            var response = await PostAsync<GenerateImageRequest, ImageGenerationResponse>(
                "image/generate",
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
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
    /// <returns>The image generation response.</returns>
    public async Task<ImageGenerationResponse> GenerateImageSimpleAsync(
        SimpleGenerateImageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        try
        {
            var response = await PostAsync<SimpleGenerateImageRequest, ImageGenerationResponse>(
                "images/generations",
                request,
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
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
    public async Task<ImageGenerationResponse> UpscaleImageAsync(
        UpscaleImageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Image))
            throw new ArgumentException("Image is required", nameof(request));

        try
        {
            var (data, _) = await PostForBinaryAsync(
                "image/upscale",
                request,
                cancellationToken);

            // Convert binary data to base64 string
            var base64Image = Convert.ToBase64String(data);

            var response = new ImageGenerationResponse
            {
                Images = new List<string> { base64Image },
                IsSuccess = true,
                StatusCode = 200
            };

            return response;
        }
        catch (VeniceAIException)
        {
            throw;
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
    public async Task<ImageGenerationResponse> EditImageAsync(
        EditImageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Prompt))
            throw new ArgumentException("Prompt is required", nameof(request));

        if (string.IsNullOrEmpty(request.Image))
            throw new ArgumentException("Image is required", nameof(request));

        try
        {
            var imageData = await PostForBinaryAsync(
                "image/edit",
                request,
                cancellationToken);

            var response = new ImageGenerationResponse
            {
                Data = new List<ImageData>
                {
                    new ImageData
                    {
                        B64Json = Convert.ToBase64String(imageData.Data)
                    }
                },
                IsSuccess = true,
                StatusCode = 200
            };

            return response;
        }
        catch (VeniceAIException)
        {
            throw;
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
    /// <returns>The image styles response.</returns>
    public async Task<ImageStylesResponse> GetImageStylesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await GetAsync<ImageStylesResponse>(
                "image/styles",
                cancellationToken);

            response.IsSuccess = true;
            response.StatusCode = 200;
            return response;
        }
        catch (VeniceAIException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new VeniceAIException($"Unexpected error getting image styles: {ex.Message}", ex);
        }
    }
}
