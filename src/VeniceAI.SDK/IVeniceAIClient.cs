using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK;

/// <summary>
/// Interface for the Venice AI client.
/// </summary>
public interface IVeniceAIClient
{
    /// <summary>
    /// Gets the chat service.
    /// </summary>
    IChatService Chat { get; }

    /// <summary>
    /// Gets the image service.
    /// </summary>
    IImageService Images { get; }

    /// <summary>
    /// Gets the video service.
    /// </summary>
    IVideoService Video { get; }

    /// <summary>
    /// Gets the embedding service.
    /// </summary>
    IEmbeddingService Embeddings { get; }

    /// <summary>
    /// Gets the audio service.
    /// </summary>
    IAudioService Audio { get; }

    /// <summary>
    /// Gets the model service.
    /// </summary>
    IModelService Models { get; }

    /// <summary>
    /// Gets the billing service.
    /// </summary>
    IBillingService Billing { get; }

    /// <summary>
    /// Gets the character service.
    /// </summary>
    ICharacterService Characters { get; }
}
