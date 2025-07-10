using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Services.Base;

/// <summary>
/// Base service class for Venice AI API services.
/// </summary>
public abstract class BaseService
{
    protected readonly HttpClient HttpClient;
    protected readonly VeniceAIOptions Options;
    protected readonly ILogger Logger;
    protected readonly JsonSerializerOptions JsonSerializerOptions;

    /// <summary>
    /// Initializes a new instance of the BaseService class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The Venice AI options.</param>
    /// <param name="logger">The logger.</param>
    protected BaseService(HttpClient httpClient, IOptions<VeniceAIOptions> options, ILogger logger)
    {
        HttpClient = httpClient;
        Options = options.Value;
        Logger = logger;
        JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    /// <summary>
    /// Sends a GET request to the specified endpoint.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response.</returns>
    protected async Task<T> SendGetRequestAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        where T : BaseResponse, new()
    {
        try
        {
            if (Options.EnableLogging)
            {
                Logger.LogInformation("Sending GET request to {Endpoint}", endpoint);
            }

            var response = await HttpClient.GetAsync(endpoint, cancellationToken);
            return await ProcessResponseAsync<T>(response, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error sending GET request to {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a POST request to the specified endpoint.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="request">The request object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response.</returns>
    protected async Task<T> SendPostRequestAsync<T>(string endpoint, object request, CancellationToken cancellationToken = default)
        where T : BaseResponse, new()
    {
        try
        {
            if (Options.EnableLogging)
            {
                Logger.LogInformation("Sending POST request to {Endpoint}", endpoint);
            }

            var json = JsonSerializer.Serialize(request, JsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(endpoint, content, cancellationToken);
            return await ProcessResponseAsync<T>(response, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error sending POST request to {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a streaming POST request to the specified endpoint.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="request">The request object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable of response chunks.</returns>
    protected async IAsyncEnumerable<T> SendStreamingPostRequestAsync<T>(
        string endpoint,
        object request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where T : BaseResponse, new()
    {
        HttpResponseMessage? response = null;
        Stream? stream = null;

        try
        {
            if (Options.EnableLogging)
            {
                Logger.LogInformation("Sending streaming POST request to {Endpoint}", endpoint);
            }

            var json = JsonSerializer.Serialize(request, JsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            response = await HttpClient.PostAsync(endpoint, content, cancellationToken);
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                Logger.LogError("Streaming request failed with status {StatusCode}: {Content}", response.StatusCode, errorContent);
                yield break;
            }

            stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var line = await reader.ReadLineAsync(cancellationToken);
                if (string.IsNullOrEmpty(line) || !line.StartsWith("data: "))
                    continue;

                var jsonData = line.Substring(6);
                if (jsonData == "[DONE]")
                    break;

                T? chunk = null;
                try
                {
                    chunk = JsonSerializer.Deserialize<T>(jsonData, JsonSerializerOptions);
                }
                catch (JsonException ex)
                {
                    Logger.LogWarning(ex, "Failed to deserialize streaming chunk: {JsonData}", jsonData);
                    continue;
                }

                if (chunk != null)
                {
                    chunk.IsSuccess = true;
                    chunk.StatusCode = (int)response.StatusCode;
                    yield return chunk;
                }
            }
        }
        finally
        {
            stream?.Dispose();
            response?.Dispose();
        }
    }

    /// <summary>
    /// Processes the HTTP response and returns the deserialized object.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    /// <param name="response">The HTTP response.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized response.</returns>
    protected async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
        where T : BaseResponse, new()
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (Options.EnableLogging)
        {
            Logger.LogInformation("Received response with status {StatusCode}: {Content}",
                response.StatusCode, content);
        }

        var result = new T
        {
            StatusCode = (int)response.StatusCode,
            RawContent = content
        };

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var deserializedResult = JsonSerializer.Deserialize<T>(content, JsonSerializerOptions);
                if (deserializedResult != null)
                {
                    result = deserializedResult;
                    result.StatusCode = (int)response.StatusCode;
                    result.RawContent = content;
                }
                result.IsSuccess = true;
            }
            catch (JsonException ex)
            {
                Logger.LogError(ex, "Failed to deserialize response: {Content}", content);
                result.IsSuccess = false;
                result.Error = new StandardError { Error = "Failed to deserialize response" };
            }
        }
        else
        {
            result.IsSuccess = false;
            try
            {
                var error = JsonSerializer.Deserialize<StandardError>(content, JsonSerializerOptions);
                result.Error = error ?? new StandardError { Error = "Unknown error" };
            }
            catch (JsonException)
            {
                result.Error = new StandardError { Error = content };
            }
        }

        return result;
    }
}
