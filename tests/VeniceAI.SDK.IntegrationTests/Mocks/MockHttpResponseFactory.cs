using System.Net;
using System.Text;
using System.Text.Json;

namespace VeniceAI.SDK.IntegrationTests.Mocks;

/// <summary>
/// Factory for creating mock HTTP responses for Venice AI API endpoints.
/// Uses System.Text.Json for serialization.
/// </summary>
public static class MockHttpResponseFactory
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    #region Chat Completions

    public static HttpResponseMessage CreateChatCompletionResponse()
    {
        var response = new
        {
            id = "chatcmpl-mock123",
            @object = "chat.completion",
            created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            model = "gpt-4o",
            system_fingerprint = "fp_mock",
            choices = new[]
            {
                new
                {
                    index = 0,
                    message = new
                    {
                        role = "assistant",
                        content = "This is a mock response from the Venice AI API integration test."
                    },
                    finish_reason = "stop"
                }
            },
            usage = new
            {
                prompt_tokens = 10,
                completion_tokens = 15,
                total_tokens = 25
            }
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateChatCompletionStreamResponse()
    {
        // For streaming, return a simple chat completion response
        return CreateChatCompletionResponse();
    }

    #endregion

    #region Embeddings

    public static HttpResponseMessage CreateEmbeddingResponse()
    {
        var response = new
        {
            @object = "list",
            data = new[]
            {
                new
                {
                    @object = "embedding",
                    index = 0,
                    embedding = Enumerable.Range(0, 1536).Select(i => Math.Sin(i * 0.01)).ToArray()
                }
            },
            model = "text-embedding-3-small",
            usage = new
            {
                prompt_tokens = 8,
                total_tokens = 8
            }
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateEmbeddingBase64Response()
    {
        var response = new
        {
            @object = "list",
            data = new[]
            {
                new
                {
                    @object = "embedding",
                    index = 0,
                    embedding_base64 = Convert.ToBase64String(new byte[1536 * 4]) // Mock base64 embedding
                }
            },
            model = "text-embedding-3-small",
            usage = new
            {
                prompt_tokens = 8,
                total_tokens = 8
            }
        };

        return CreateJsonResponse(response);
    }

    #endregion

    #region Audio

    public static HttpResponseMessage CreateAudioResponse()
    {
        // Return mock audio binary data
        var mockAudioData = new byte[1024]; // Mock audio data
        new Random().NextBytes(mockAudioData);

        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(mockAudioData)
            {
                Headers = { { "Content-Type", "audio/mpeg" } }
            }
        };
    }

    #endregion

    #region Images

    public static HttpResponseMessage CreateImageGenerationResponse()
    {
        var response = new
        {
            created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            data = new[]
            {
                new
                {
                    url = "https://mock-image-url.com/generated-image.png",
                    revised_prompt = "A beautiful sunset over the ocean with sailboats in the distance"
                }
            }
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateImageStylesResponse()
    {
        var response = new
        {
            styles = new[] { "photographic", "digital-art", "comic-book", "fantasy-art", "analog-film" }
        };

        return CreateJsonResponse(response);
    }

    #endregion

    #region Models

    public static HttpResponseMessage CreateModelsResponse()
    {
        var response = new
        {
            @object = "list",
            data = new[]
            {
                new
                {
                    id = "gpt-4o",
                    @object = "model",
                    created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    owned_by = "venice_ai",  // Use snake_case for JsonStringEnumConverter 
                    type = "text"
                },
                new
                {
                    id = "flux-1-schnell",
                    @object = "model", 
                    created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    owned_by = "venice_ai",
                    type = "image"
                },
                new
                {
                    id = "text-embedding-3-small",
                    @object = "model",
                    created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    owned_by = "venice_ai",
                    type = "embedding"
                }
            }
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateModelResponse()
    {
        var response = new
        {
            id = "gpt-4o",
            @object = "model",
            created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            owned_by = "venice-ai",
            type = "text"
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateModelTraitsResponse()
    {
        var response = new
        {
            @object = "list",  // Added required object field
            traits = new Dictionary<string, string>
            {
                { "max_tokens", "4096" },
                { "supports_streaming", "true" },
                { "supports_functions", "true" },
                { "context_window", "128000" }
            }
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateModelCompatibilityResponse()
    {
        var response = new
        {
            @object = "list",  // Added required object field
            compatibility = new Dictionary<string, string>
            {
                { "gpt-4", "gpt-4o" },
                { "gpt-3.5-turbo", "gpt-4o-mini" },
                { "text-davinci-003", "gpt-4o" }
            }
        };

        return CreateJsonResponse(response);
    }

    #endregion

    #region Billing

    public static HttpResponseMessage CreateBillingUsageResponse()
    {
        var response = new
        {
            data = new[]
            {
                new
                {
                    timestamp = DateTime.UtcNow.AddDays(-1),
                    amount = 0.50,
                    currency = "USD",
                    sku = "chat-completion-gpt-4o"
                },
                new
                {
                    timestamp = DateTime.UtcNow.AddDays(-2),
                    amount = 0.25,
                    currency = "USD",
                    sku = "text-embedding-3-small"
                }
            },
            pagination = new
            {
                page = 1,
                limit = 100,
                total = 2,
                total_pages = 1
            }
        };

        return CreateJsonResponse(response);
    }

    #endregion

    #region Helper Methods

    private static HttpResponseMessage CreateJsonResponse<T>(T data)
    {
        var json = JsonSerializer.Serialize(data, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = content
        };
    }

    public static HttpResponseMessage CreateErrorResponse(HttpStatusCode statusCode, string message)
    {
        var errorResponse = new
        {
            error = new
            {
                message,
                type = "api_error",
                code = statusCode.ToString()
            }
        };

        var json = JsonSerializer.Serialize(errorResponse, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return new HttpResponseMessage(statusCode)
        {
            Content = content
        };
    }

    #endregion
}
