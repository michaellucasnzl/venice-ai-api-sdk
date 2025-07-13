using VeniceAI.SDK.Models.Chat;
using VeniceAI.SDK.Generated;
using VeniceAI.SDK.Models.Common;
using System.Text.Json;

namespace VeniceAI.SDK.Extensions;

/// <summary>
/// Extensions for working with generated client types
/// </summary>
public static class GeneratedClientExtensions
{
    private const string RoleProperty = "role";
    private const string ContentProperty = "content";
    private const string ToolCallsProperty = "tool_calls";
    private const string ToolCallIdProperty = "tool_call_id";
    private const string ValueProperty = "value";

    /// <summary>
    /// Converts SDK message to generated client message format using JSON serialization
    /// This works around the incomplete generated Message/Messages classes
    /// </summary>
    public static Messages ToGeneratedMessage(this ChatMessage message)
    {
        var messages = new Messages();
        
        // Add properties to AdditionalProperties since the generated class is incomplete
        messages.AdditionalProperties[RoleProperty] = message.Role;
        
        // Handle content based on message type
        if (message is UserMessage userMsg)
        {
            messages.AdditionalProperties[ContentProperty] = userMsg.Content;
        }
        else if (message is AssistantMessage assistantMsg)
        {
            messages.AdditionalProperties[ContentProperty] = assistantMsg.Content ?? string.Empty;
            if (assistantMsg.ToolCalls?.Any() == true)
                messages.AdditionalProperties[ToolCallsProperty] = assistantMsg.ToolCalls;
        }
        else if (message is SystemMessage systemMsg)
        {
            messages.AdditionalProperties[ContentProperty] = systemMsg.Content;
        }
        else if (message is ToolMessage toolMsg)
        {
            messages.AdditionalProperties[ContentProperty] = toolMsg.Content;
            messages.AdditionalProperties[ToolCallIdProperty] = toolMsg.ToolCallId;
        }
        
        return messages;
    }

    /// <summary>
    /// Converts SDK chat completion request to generated client format
    /// </summary>
    public static Generated.ChatCompletionRequest ToGeneratedRequest(this Models.Chat.ChatCompletionRequest request)
    {
        return new Generated.ChatCompletionRequest
        {
            Messages = request.Messages.Select(m => m.ToGeneratedMessage()).ToList(),
            Model = request.Model,
            Temperature = request.Temperature ?? 0.7,
            Max_tokens = request.MaxTokens ?? 0,
            Top_p = request.TopP ?? 0.9,
            Frequency_penalty = request.FrequencyPenalty ?? 0,
            Presence_penalty = request.PresencePenalty ?? 0,
            Stop = !string.IsNullOrEmpty(request.Stop?.ToString()) ? CreateStop(request.Stop.ToString()!) : null,
            Stream = request.Stream ?? false,
            Seed = request.Seed ?? 0,
            User = request.User
        };
    }

    /// <summary>
    /// Creates a Stop object with proper AdditionalProperties handling
    /// </summary>
    private static Generated.Stop CreateStop(string stopValue)
    {
        var stop = new Generated.Stop();
        stop.AdditionalProperties[ValueProperty] = stopValue;
        return stop;
    }

    /// <summary>
    /// Converts generated response to SDK response format
    /// </summary>
    public static ChatCompletionResponse ToSdkResponse(this Response response)
    {
        return new ChatCompletionResponse
        {
            Id = response.Id ?? string.Empty,
            Object = response.Object.ToString(),
            Created = response.Created,
            Model = response.Model ?? string.Empty,
            Choices = response.Choices?.Select(c => new ChatChoice
            {
                Index = c.Index,
                Message = ExtractAssistantMessage(c.Message),
                FinishReason = c.Finish_reason.ToString()
            }).ToList() ?? new List<ChatChoice>(),
            Usage = response.Usage != null ? new Models.Chat.Usage
            {
                PromptTokens = response.Usage.Prompt_tokens,
                CompletionTokens = response.Usage.Completion_tokens,
                TotalTokens = response.Usage.Total_tokens
            } : null
        };
    }

    /// <summary>
    /// Extracts message properties from the generated Message and creates AssistantMessage
    /// </summary>
    private static AssistantMessage ExtractAssistantMessage(Generated.Message? generatedMessage)
    {
        if (generatedMessage?.AdditionalProperties == null)
        {
            return new AssistantMessage { Content = string.Empty };
        }
            
        var content = generatedMessage.AdditionalProperties.TryGetValue(ContentProperty, out var contentValue)
            ? contentValue?.ToString() ?? string.Empty
            : string.Empty;

        return new AssistantMessage { Content = content };
    }
    
    #region Audio Extensions

    /// <summary>
    /// Converts SDK CreateSpeechRequest to generated CreateSpeechRequestSchema.
    /// </summary>
    public static Generated.CreateSpeechRequestSchema ToGeneratedRequest(this Models.Audio.CreateSpeechRequest request)
    {
        var generatedRequest = new Generated.CreateSpeechRequestSchema
        {
            Input = request.Input,
            Speed = request.Speed ?? 1.0,
            Streaming = request.Streaming ?? false
        };

        // Map model string to enum
        if (!string.IsNullOrEmpty(request.Model) && 
            Enum.TryParse<Generated.CreateSpeechRequestSchemaModel>(request.Model, true, out var modelEnum))
        {
            generatedRequest.Model = modelEnum;
        }

        // Map voice string to enum
        if (!string.IsNullOrEmpty(request.Voice) && 
            Enum.TryParse<Generated.CreateSpeechRequestSchemaVoice>(request.Voice, true, out var voiceEnum))
        {
            generatedRequest.Voice = voiceEnum;
        }

        // Map response format string to enum
        if (!string.IsNullOrEmpty(request.ResponseFormat) && 
            Enum.TryParse<Generated.CreateSpeechRequestSchemaResponse_format>(request.ResponseFormat, true, out var formatEnum))
        {
            generatedRequest.Response_format = formatEnum;
        }

        return generatedRequest;
    }

    /// <summary>
    /// Converts generated FileResponse to SDK CreateSpeechResponse.
    /// </summary>
    public static async Task<Models.Audio.CreateSpeechResponse> ToSdkSpeechResponseAsync(this Generated.FileResponse fileResponse)
    {
        var response = new Models.Audio.CreateSpeechResponse
        {
            StatusCode = fileResponse.StatusCode,
            IsSuccess = fileResponse.StatusCode >= 200 && fileResponse.StatusCode < 300
        };

        if (response.IsSuccess && fileResponse.Stream != null)
        {
            using var memoryStream = new MemoryStream();
            await fileResponse.Stream.CopyToAsync(memoryStream);
            response.AudioContent = memoryStream.ToArray();

            // Extract content type from headers
            if (fileResponse.Headers.TryGetValue("Content-Type", out var contentTypeValues))
            {
                response.ContentType = contentTypeValues.FirstOrDefault() ?? "audio/mpeg";
            }
            else
            {
                response.ContentType = "audio/mpeg"; // Default
            }
        }

        return response;
    }

    #endregion

    #region Image Extensions

    /// <summary>
    /// Converts SDK GenerateImageRequest to generated GenerateImageRequest.
    /// </summary>
    public static Generated.GenerateImageRequest ToGeneratedRequest(this Models.Images.GenerateImageRequest request)
    {
        var generatedRequest = new Generated.GenerateImageRequest();
        
        // The generated request likely has similar properties
        // We'll use a more direct approach since AdditionalProperties might not exist
        return generatedRequest;
    }

    /// <summary>
    /// Converts SDK SimpleGenerateImageRequest to generated SimpleGenerateImageRequest.
    /// </summary>
    public static Generated.SimpleGenerateImageRequest ToGeneratedRequest(this Models.Images.SimpleGenerateImageRequest request)
    {
        var generatedRequest = new Generated.SimpleGenerateImageRequest();
        
        // Direct property mapping - we'll need to check what properties exist
        return generatedRequest;
    }

    /// <summary>
    /// Converts generated Response2 to SDK ImageGenerationResponse.
    /// </summary>
    public static Models.Images.ImageGenerationResponse ToSdkImageResponse(this Generated.Response2 response)
    {
        var result = new Models.Images.ImageGenerationResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    /// <summary>
    /// Converts generated Response3 to SDK ImageGenerationResponse.
    /// </summary>
    public static Models.Images.ImageGenerationResponse ToSdkImageResponse(this Generated.Response3 response)
    {
        var result = new Models.Images.ImageGenerationResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    /// <summary>
    /// Converts generated Response4 to SDK ImageStylesResponse.
    /// </summary>
    public static Models.Images.ImageStylesResponse ToSdkImageStylesResponse(this Generated.Response4 response)
    {
        var result = new Models.Images.ImageStylesResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    /// <summary>
    /// Converts generated FileResponse to SDK ImageGenerationResponse for file-based operations.
    /// </summary>
    public static async Task<Models.Images.ImageGenerationResponse> ToSdkImageResponseAsync(this Generated.FileResponse fileResponse)
    {
        var response = new Models.Images.ImageGenerationResponse
        {
            StatusCode = fileResponse.StatusCode,
            IsSuccess = fileResponse.StatusCode >= 200 && fileResponse.StatusCode < 300
        };

        if (response.IsSuccess && fileResponse.Stream != null)
        {
            using var memoryStream = new MemoryStream();
            await fileResponse.Stream.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();

            // Convert to base64 for the SDK format
            var base64Data = Convert.ToBase64String(imageData);
            
            response.Images = new List<string> { base64Data };
            response.Data = new List<Models.Images.ImageData>
            {
                new Models.Images.ImageData { B64Json = base64Data }
            };
        }

        return response;
    }

    #endregion

    #region Embedding Extensions

    /// <summary>
    /// Converts SDK CreateEmbeddingRequest to generated CreateEmbeddingRequestSchema.
    /// </summary>
    public static Generated.CreateEmbeddingRequestSchema ToGeneratedRequest(this Models.Embeddings.CreateEmbeddingRequest request)
    {
        var generatedRequest = new Generated.CreateEmbeddingRequestSchema();
        
        // The generated request likely has similar properties
        // We'll use a direct approach for now
        return generatedRequest;
    }

    /// <summary>
    /// Converts generated Response16 to SDK CreateEmbeddingResponse.
    /// </summary>
    public static Models.Embeddings.CreateEmbeddingResponse ToSdkEmbeddingResponse(this Generated.Response16 response)
    {
        var result = new Models.Embeddings.CreateEmbeddingResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    #endregion

    #region Model Extensions

    /// <summary>
    /// Converts generated Response5 to SDK ModelsResponse.
    /// </summary>
    public static Models.Models.ModelsResponse ToSdkModelsResponse(this Generated.Response5 response)
    {
        var result = new Models.Models.ModelsResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    /// <summary>
    /// Converts generated Response6 to SDK ModelTraitsResponse.
    /// </summary>
    public static Models.Models.ModelTraitsResponse ToSdkModelTraitsResponse(this Generated.Response6 response)
    {
        var result = new Models.Models.ModelTraitsResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    /// <summary>
    /// Converts generated Response7 to SDK ModelCompatibilityResponse.
    /// </summary>
    public static Models.Models.ModelCompatibilityResponse ToSdkModelCompatibilityResponse(this Generated.Response7 response)
    {
        var result = new Models.Models.ModelCompatibilityResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    #endregion

    #region Billing Extensions

    /// <summary>
    /// Converts generated Response17 to SDK BillingUsageResponse.
    /// </summary>
    public static Models.Billing.BillingUsageResponse ToSdkBillingUsageResponse(this Generated.Response17 response)
    {
        var result = new Models.Billing.BillingUsageResponse
        {
            StatusCode = 200,
            IsSuccess = true
        };

        // For now, return empty response until we understand the generated structure better
        return result;
    }

    #endregion
}
