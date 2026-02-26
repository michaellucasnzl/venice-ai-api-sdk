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
    /// <param name="videoService">The video service.</param>
    /// <param name="embeddingService">The embedding service.</param>
    /// <param name="audioService">The audio service.</param>
    /// <param name="modelService">The model service.</param>
    /// <param name="billingService">The billing service.</param>
    /// <param name="characterService">The character service.</param>
    public VeniceAIClient(
        IChatService chatService,
        IImageService imageService,
        IVideoService videoService,
        IEmbeddingService embeddingService,
        IAudioService audioService,
        IModelService modelService,
        IBillingService billingService,
        ICharacterService characterService)
    {
        Chat = chatService;
        Images = imageService;
        Video = videoService;
        Embeddings = embeddingService;
        Audio = audioService;
        Models = modelService;
        Billing = billingService;
        Characters = characterService;
    }

    /// <inheritdoc />
    public IChatService Chat { get; }

    /// <inheritdoc />
    public IImageService Images { get; }

    /// <inheritdoc />
    public IVideoService Video { get; }

    /// <inheritdoc />
    public IEmbeddingService Embeddings { get; }

    /// <inheritdoc />
    public IAudioService Audio { get; }

    /// <inheritdoc />
    public IModelService Models { get; }

    /// <inheritdoc />
    public IBillingService Billing { get; }

    /// <inheritdoc />
    public ICharacterService Characters { get; }
}
