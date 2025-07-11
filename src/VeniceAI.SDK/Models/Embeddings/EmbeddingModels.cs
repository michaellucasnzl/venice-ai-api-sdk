using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Embeddings;

/// <summary>
/// Request for creating embeddings.
/// </summary>
public class CreateEmbeddingRequest
{
    /// <summary>
    /// Input text to embed.
    /// </summary>
    [JsonPropertyName("input")]
    public object Input { get; set; } = string.Empty;

    /// <summary>
    /// ID of the model to use.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The format to return the embeddings in.
    /// </summary>
    [JsonPropertyName("encoding_format")]
    public string? EncodingFormat { get; set; }

    /// <summary>
    /// The number of dimensions the resulting output embeddings should have.
    /// </summary>
    [JsonPropertyName("dimensions")]
    public int? Dimensions { get; set; }

    /// <summary>
    /// This parameter is unused and is discarded by Venice.
    /// </summary>
    [JsonPropertyName("user")]
    public string? User { get; set; }
}

/// <summary>
/// Response from embeddings API.
/// </summary>
public class CreateEmbeddingResponse : BaseResponse
{
    /// <summary>
    /// The object type, which is always "list".
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "list";

    /// <summary>
    /// The list of embedding objects.
    /// </summary>
    [JsonPropertyName("data")]
    public List<EmbeddingData> Data { get; set; } = new();

    /// <summary>
    /// The name of the model used to generate the embedding.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Usage statistics for the embedding request.
    /// </summary>
    [JsonPropertyName("usage")]
    public EmbeddingUsage Usage { get; set; } = new();
}

/// <summary>
/// Embedding data object.
/// </summary>
[JsonConverter(typeof(EmbeddingDataConverter))]
public class EmbeddingData
{
    /// <summary>
    /// The object type, which is always "embedding".
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "embedding";

    /// <summary>
    /// The embedding vector.
    /// </summary>
    [JsonPropertyName("embedding")]
    public List<double> Embedding { get; set; } = new();

    /// <summary>
    /// The base64 encoded embedding (when using base64 encoding format).
    /// </summary>
    [JsonIgnore]
    public string? EmbeddingBase64 { get; set; }

    /// <summary>
    /// The encoding format used for the embedding.
    /// </summary>
    [JsonIgnore]
    public string? EncodingFormat { get; set; }

    /// <summary>
    /// The index of the embedding in the list of embeddings.
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }
}

/// <summary>
/// Usage statistics for embedding requests.
/// </summary>
public class EmbeddingUsage
{
    /// <summary>
    /// The number of tokens used by the prompt.
    /// </summary>
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }

    /// <summary>
    /// The total number of tokens used by the request.
    /// </summary>
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}
