using System.Text.Json.Serialization;

namespace VeniceAI.SDK.Models.Augment;

/// <summary>
/// Request to perform a web search.
/// </summary>
public class WebSearchRequest
{
    /// <summary>
    /// The search query (1-400 characters).
    /// </summary>
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// Maximum number of results to return (default: 10, max: 20).
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Search provider to use ("brave" or "google"). Defaults to "brave".
    /// </summary>
    [JsonPropertyName("search_provider")]
    public string? SearchProvider { get; set; }
}

/// <summary>
/// Response from a web search.
/// </summary>
public class WebSearchResponse : VeniceAI.SDK.Models.Common.BaseResponse
{
    /// <summary>
    /// The search query that was executed.
    /// </summary>
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// The search results.
    /// </summary>
    [JsonPropertyName("results")]
    public List<WebSearchResult> Results { get; set; } = new();
}

/// <summary>
/// A single web search result.
/// </summary>
public class WebSearchResult
{
    /// <summary>
    /// The title of the search result.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the search result.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// A snippet or description of the search result.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The date of the search result, if available.
    /// </summary>
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;
}

/// <summary>
/// Request to scrape a web page.
/// </summary>
public class WebScrapeRequest
{
    /// <summary>
    /// The URL to scrape.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

/// <summary>
/// Response from scraping a web page.
/// </summary>
public class WebScrapeResponse : VeniceAI.SDK.Models.Common.BaseResponse
{
    /// <summary>
    /// The URL that was scraped.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The scraped content in markdown format.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The format of the scraped content.
    /// </summary>
    [JsonPropertyName("format")]
    public string Format { get; set; } = string.Empty;
}

/// <summary>
/// Request to parse and extract text from a document file.
/// </summary>
public class TextParserRequest
{
    /// <summary>
    /// The document file data (binary). Supported formats: PDF, EPUB, DOCX, PPTX, XLSX,
    /// plain text, Markdown, CSV, JSON, and most source-code files. Maximum size: 25MB.
    /// </summary>
    [JsonPropertyName("file")]
    public byte[] File { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// The filename of the document.
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; set; } = "document.pdf";

    /// <summary>
    /// The format of the response output (json, text).
    /// </summary>
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }
}

/// <summary>
/// Response from parsing a document file.
/// </summary>
public class TextParserResponse : VeniceAI.SDK.Models.Common.BaseResponse
{
    /// <summary>
    /// The extracted text content from the document.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The token count of the extracted text.
    /// </summary>
    [JsonPropertyName("tokens")]
    public double Tokens { get; set; }
}
