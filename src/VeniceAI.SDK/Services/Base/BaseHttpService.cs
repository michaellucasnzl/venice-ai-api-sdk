using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using VeniceAI.SDK.Models.Chat;

namespace VeniceAI.SDK.Services.Base;

/// <summary>
/// Base HTTP client for Venice AI API services.
/// </summary>
public class BaseHttpService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Initializes a new instance of the BaseHttpService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiKey">The API key.</param>
    public BaseHttpService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;

        // Set up authentication
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        // Set base address
        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("https://api.venice.ai/api/v1/"); //todo: getthis from appsettings.
        }

        // Configure JSON serialization
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };
    }

    /// <summary>
    /// Makes a POST request with JSON payload.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="endpoint">The API endpoint.</param>
    /// <param name="request">The request object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response object.</returns>
    protected async Task<TResponse> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);

        // Debug logging
        Console.WriteLine($"POST {endpoint}");
        Console.WriteLine($"Request: {json}");

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Explicitly set Content-Type to avoid any issues
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        using var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        // Debug logging
        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"Response: {responseContent}");

        if (!response.IsSuccessStatusCode)
        {
            throw new VeniceAIException($"API Error (Status: {(int)response.StatusCode}): {responseContent}");
        }

        return JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions)!;
    }

    /// <summary>
    /// Makes a GET request.
    /// </summary>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="endpoint">The API endpoint.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response object.</returns>
    protected async Task<TResponse> GetAsync<TResponse>(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"GET {endpoint}");

        using var response = await _httpClient.GetAsync(endpoint, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"Response: {responseContent}");

        if (!response.IsSuccessStatusCode)
        {
            throw new VeniceAIException($"API Error (Status: {(int)response.StatusCode}): {responseContent}");
        }

        return JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions)!;
    }

    /// <summary>
    /// Makes a POST request that returns binary data (like audio or images).
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <param name="endpoint">The API endpoint.</param>
    /// <param name="request">The request object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The binary response data and content type.</returns>
    protected async Task<(byte[] Data, string ContentType)> PostForBinaryAsync<TRequest>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);

        Console.WriteLine($"POST {endpoint} (Binary)");
        Console.WriteLine($"Request: {json}");

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Explicitly set Content-Type to avoid any issues
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        using var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"Response Content-Type: {response.Content.Headers.ContentType}");

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new VeniceAIException($"API Error (Status: {(int)response.StatusCode}): {errorContent}");
        }

        var data = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";

        return (data, contentType);
    }

    /// <summary>
    /// Makes a POST request that returns Server-Sent Events (SSE) stream.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <param name="endpoint">The API endpoint.</param>
    /// <param name="request">The request object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of streaming responses.</returns>
    protected async IAsyncEnumerable<ChatCompletionStreamResponse> PostStreamAsync<TRequest>(
        string endpoint,
        TRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);

        Console.WriteLine($"POST {endpoint} (Stream)");
        Console.WriteLine($"Request: {json}");

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        using var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

        Console.WriteLine($"Response Status: {response.StatusCode}");

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new VeniceAIException($"API Error (Status: {(int)response.StatusCode}): {errorContent}");
        }

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var reader = new StreamReader(stream);

        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            if (line.StartsWith("data: "))
            {
                var jsonData = line.Substring(6); // Remove "data: " prefix

                if (jsonData.Trim() == "[DONE]")
                {
                    yield break;
                }

                var streamResponse = TryDeserializeStreamResponse(jsonData);
                if (streamResponse != null)
                {
                    yield return streamResponse;
                }
            }
        }
    }

    private ChatCompletionStreamResponse? TryDeserializeStreamResponse(string jsonData)
    {
        try
        {
            return JsonSerializer.Deserialize<ChatCompletionStreamResponse>(jsonData, _jsonOptions);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse SSE chunk: {jsonData}");
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
