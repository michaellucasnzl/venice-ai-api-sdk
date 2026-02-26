using VeniceAI.SDK.Models.Characters;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for character services.
/// </summary>
public interface ICharacterService
{
    /// <summary>
    /// Lists all available characters.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The list of available characters.</returns>
    Task<CharactersResponse> ListCharactersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a character by its slug identifier.
    /// </summary>
    /// <param name="slug">The character's slug identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The character details.</returns>
    Task<CharacterResponse> GetCharacterAsync(string slug, CancellationToken cancellationToken = default);
}
