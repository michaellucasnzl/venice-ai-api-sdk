using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Characters;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for character operations using the Venice AI API.
/// </summary>
public class CharacterService : BaseHttpService, ICharacterService
{
    /// <summary>
    /// Initializes a new instance of the CharacterService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public CharacterService(HttpClient httpClient, string apiKey, ILogger<CharacterService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Lists all available characters.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The list of available characters.</returns>
    public async Task<CharactersResponse> ListCharactersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await GetAsync<CharactersResponse>("characters", cancellationToken);

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
            throw new VeniceAIException($"Unexpected error during character listing: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a character by its slug identifier.
    /// </summary>
    /// <param name="slug">The character's slug identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The character details.</returns>
    public async Task<CharacterResponse> GetCharacterAsync(string slug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(slug))
            throw new ArgumentException("Slug is required", nameof(slug));

        try
        {
            var response = await GetAsync<CharacterResponse>($"characters/{Uri.EscapeDataString(slug)}", cancellationToken);

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
            throw new VeniceAIException($"Unexpected error during character retrieval: {ex.Message}", ex);
        }
    }
}
