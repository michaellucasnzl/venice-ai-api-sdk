using VeniceAI.SDK.Models.Augment;

namespace VeniceAI.SDK.Services.Interfaces;

/// <summary>
/// Interface for augment services (web search, web scraping, and text parsing).
/// </summary>
public interface IAugmentService
{
    /// <summary>
    /// Performs a web search.
    /// </summary>
    /// <param name="request">The web search request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The web search response.</returns>
    Task<WebSearchResponse> SearchWebAsync(WebSearchRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Scrapes a web page and returns its content as markdown.
    /// </summary>
    /// <param name="request">The web scrape request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The web scrape response.</returns>
    Task<WebScrapeResponse> ScrapeWebAsync(WebScrapeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Parses a document file and extracts its text content.
    /// </summary>
    /// <param name="request">The text parser request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The parsed text response.</returns>
    Task<TextParserResponse> ParseTextAsync(TextParserRequest request, CancellationToken cancellationToken = default);
}
