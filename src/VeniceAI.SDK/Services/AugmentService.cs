using Microsoft.Extensions.Logging;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;
using VeniceAI.SDK.Models.Augment;

namespace VeniceAI.SDK.Services;

/// <summary>
/// Service for augment operations (web search, web scraping, and text parsing) using the Venice AI API.
/// </summary>
public class AugmentService : BaseHttpService, IAugmentService
{
    /// <summary>
    /// Initializes a new instance of the AugmentService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public AugmentService(HttpClient httpClient, string apiKey, ILogger<AugmentService> logger) : base(httpClient, apiKey, logger)
    {
    }

    /// <summary>
    /// Performs a web search.
    /// </summary>
    /// <param name="request">The web search request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The web search response.</returns>
    public async Task<WebSearchResponse> SearchWebAsync(
        WebSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Query))
            throw new ArgumentException("Query is required", nameof(request));

        try
        {
            var response = await PostAsync<WebSearchRequest, WebSearchResponse>(
                "augment/search",
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
            throw new VeniceAIException($"Unexpected error during web search: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Scrapes a web page and returns its content as markdown.
    /// </summary>
    /// <param name="request">The web scrape request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The web scrape response.</returns>
    public async Task<WebScrapeResponse> ScrapeWebAsync(
        WebScrapeRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Url))
            throw new ArgumentException("Url is required", nameof(request));

        try
        {
            var response = await PostAsync<WebScrapeRequest, WebScrapeResponse>(
                "augment/scrape",
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
            throw new VeniceAIException($"Unexpected error during web scraping: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Parses a document file and extracts its text content.
    /// </summary>
    /// <param name="request">The text parser request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The parsed text response.</returns>
    public async Task<TextParserResponse> ParseTextAsync(
        TextParserRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("File is required", nameof(request));

        try
        {
            var fields = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(request.ResponseFormat))
                fields["response_format"] = request.ResponseFormat;

            var response = await PostMultipartAsync<TextParserResponse>(
                "augment/text-parser",
                request.File,
                request.Filename,
                "file",
                fields,
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
            throw new VeniceAIException($"Unexpected error during text parsing: {ex.Message}", ex);
        }
    }
}
