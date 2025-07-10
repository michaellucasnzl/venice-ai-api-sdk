using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK;

/// <summary>
/// Main client for the Venice AI API.
/// </summary>
public class VeniceAIClient : IVeniceAIClient
{
    /// <summary>
    /// Initializes a new instance of the VeniceAIClient class.
    /// </summary>
    /// <param name="chatService">The chat service.</param>
    /// <param name="imageService">The image service.</param>
    /// <param name="embeddingService">The embedding service.</param>
    /// <param name="audioService">The audio service.</param>
    /// <param name="modelService">The model service.</param>
    /// <param name="billingService">The billing service.</param>
    public VeniceAIClient(
        IChatService chatService,
        IImageService imageService,
        IEmbeddingService embeddingService,
        IAudioService audioService,
        IModelService modelService,
        IBillingService billingService)
    {
        Chat = chatService;
        Images = imageService;
        Embeddings = embeddingService;
        Audio = audioService;
        Models = modelService;
        Billing = billingService;
    }

    /// <inheritdoc />
    public IChatService Chat { get; }

    /// <inheritdoc />
    public IImageService Images { get; }

    /// <inheritdoc />
    public IEmbeddingService Embeddings { get; }

    /// <inheritdoc />
    public IAudioService Audio { get; }

    /// <inheritdoc />
    public IModelService Models { get; }

    /// <inheritdoc />
    public IBillingService Billing { get; }
}
