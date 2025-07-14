using System.Net;
using System.Text;
using System.Text.Json;
using VeniceAI.SDK.Models.Audio;
using VeniceAI.SDK.Models.Billing;
using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Models.Common;
using VeniceAI.SDK.Models.Embeddings;
using VeniceAI.SDK.Models.Images;
using VeniceAI.SDK.Models.Models;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Factory for creating mock HTTP responses for Venice AI API endpoints.
/// Uses System.Text.Json for serialization instead of Newtonsoft.Json.
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
        var response = new ChatCompletionResponse
        {
            Id = "chatcmpl-mock123",
            Object = "chat.completion",
            Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Model = "gpt-4o",
            SystemFingerprint = "fp_mock",
            Choices = new List<ChatChoice>
            {
                new ChatChoice
                {
                    Index = 0,
                    Message = new AssistantMessage
                    {
                        Role = "assistant",
                        Content = "This is a mock response from the Venice AI API integration test."
                    },
                    FinishReason = "stop"
                }
            },
            Usage = new Usage
            {
                PromptTokens = 10,
                CompletionTokens = 15,
                TotalTokens = 25
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateChatCompletionStreamResponse()
    {
        // For streaming, we'll return a simple chat completion response
        // In a real mock, you might want to implement Server-Sent Events format
        return CreateChatCompletionResponse();
    }

    #endregion

    #region Embeddings

    public static HttpResponseMessage CreateEmbeddingResponse()
    {
        var response = new CreateEmbeddingResponse
        {
            Object = "list",
            Data = new List<EmbeddingData>
            {
                new EmbeddingData
                {
                    Object = "embedding",
                    Index = 0,
                    Embedding = Enumerable.Range(0, 1536).Select(i => Math.Sin(i * 0.01)).ToList()
                }
            },
            Model = "text-embedding-3-small",
            Usage = new EmbeddingUsage
            {
                PromptTokens = 8,
                TotalTokens = 8
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateEmbeddingBase64Response()
    {
        var response = new CreateEmbeddingResponse
        {
            Object = "list",
            Data = new List<EmbeddingData>
            {
                new EmbeddingData
                {
                    Object = "embedding",
                    Index = 0,
                    EmbeddingBase64 = Convert.ToBase64String(new byte[1536 * 4]) // Mock base64 embedding
                }
            },
            Model = "text-embedding-3-small",
            Usage = new EmbeddingUsage
            {
                PromptTokens = 8,
                TotalTokens = 8
            },
            IsSuccess = true
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
        var response = new ImageGenerationResponse
        {
            Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Data = new List<ImageData>
            {
                new ImageData
                {
                    Url = "https://mock-image-url.com/generated-image.png",
                    RevisedPrompt = "A beautiful sunset over the ocean with sailboats in the distance"
                }
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateImageStylesResponse()
    {
        var response = new ImageStylesResponse
        {
            Styles = new List<ImageStyle>
            {
                new ImageStyle { Name = "photographic" },
                new ImageStyle { Name = "digital-art" },
                new ImageStyle { Name = "comic-book" },
                new ImageStyle { Name = "fantasy-art" },
                new ImageStyle { Name = "analog-film" }
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    #endregion

    #region Models

    public static HttpResponseMessage CreateModelsResponse()
    {
        var response = new ModelsResponse
        {
            Object = "list",
            Data = new List<Model>
            {
                new Model
                {
                    Id = "gpt-4o",
                    Object = "model",
                    Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    OwnedBy = "venice-ai",
                    Type = "text"
                },
                new Model
                {
                    Id = "flux-1-schnell",
                    Object = "model", 
                    Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    OwnedBy = "venice-ai",
                    Type = "image"
                },
                new Model
                {
                    Id = "text-embedding-3-small",
                    Object = "model",
                    Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    OwnedBy = "venice-ai",
                    Type = "embedding"
                }
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateModelResponse()
    {
        var response = new Model
        {
            Id = "gpt-4o",
            Object = "model",
            Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            OwnedBy = "venice-ai",
            Type = "text"
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateModelTraitsResponse()
    {
        var response = new ModelTraitsResponse
        {
            Traits = new Dictionary<string, string>
            {
                { "max_tokens", "4096" },
                { "supports_streaming", "true" },
                { "supports_functions", "true" },
                { "context_window", "128000" }
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    public static HttpResponseMessage CreateModelCompatibilityResponse()
    {
        var response = new ModelCompatibilityResponse
        {
            Compatibility = new Dictionary<string, string>
            {
                { "gpt-4", "gpt-4o" },
                { "gpt-3.5-turbo", "gpt-4o-mini" },
                { "text-davinci-003", "gpt-4o" }
            },
            IsSuccess = true
        };

        return CreateJsonResponse(response);
    }

    #endregion

    #region Billing

    public static HttpResponseMessage CreateBillingUsageResponse()
    {
        var response = new BillingUsageResponse
        {
            Data = new List<BillingUsageEntry>
            {
                new BillingUsageEntry
                {
                    Timestamp = DateTime.UtcNow.AddDays(-1),
                    Amount = 0.50,
                    Currency = Currency.USD,
                    Sku = "chat-completion-gpt-4o",
                    Notes = "Chat completion tokens",
                    Units = 1000,
                    PricePerUnitUsd = 0.0005
                },
                new BillingUsageEntry
                {
                    Timestamp = DateTime.UtcNow.AddDays(-2),
                    Amount = 0.25,
                    Currency = Currency.USD,
                    Sku = "text-embedding-3-small", 
                    Notes = "Text embedding tokens",
                    Units = 500,
                    PricePerUnitUsd = 0.0005
                }
            },
            Pagination = new PaginationInfo
            {
                Page = 1,
                Limit = 100,
                Total = 2,
                TotalPages = 1
            },
            IsSuccess = true
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
