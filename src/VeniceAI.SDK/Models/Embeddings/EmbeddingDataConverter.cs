using System.Text.Json;
using System.Text.Json.Serialization;

namespace VeniceAI.SDK.Models.Embeddings;

/// <summary>
/// Custom JSON converter for EmbeddingData to handle both base64 and array formats.
/// </summary>
public class EmbeddingDataConverter : JsonConverter<EmbeddingData>
{
    private const string EmbeddingObjectType = "embedding";
    
    public override EmbeddingData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        
        var embeddingData = new EmbeddingData();
        
        if (root.TryGetProperty("object", out var objectElement))
        {
            embeddingData.Object = objectElement.GetString() ?? EmbeddingObjectType;
        }
        
        if (root.TryGetProperty("index", out var indexElement))
        {
            embeddingData.Index = indexElement.GetInt32();
        }
        
        if (root.TryGetProperty("encoding_format", out var encodingElement))
        {
            embeddingData.EncodingFormat = encodingElement.GetString();
        }
        
        if (root.TryGetProperty("embedding", out var embeddingElement))
        {
            if (embeddingElement.ValueKind == JsonValueKind.String)
            {
                // Handle base64 encoded embedding - store as base64 string
                embeddingData.EmbeddingBase64 = embeddingElement.GetString();
                embeddingData.Embedding = new List<double>(); // Keep empty for base64 format
            }
            else if (embeddingElement.ValueKind == JsonValueKind.Array)
            {
                // Handle array of doubles
                var doubles = new List<double>();
                foreach (var element in embeddingElement.EnumerateArray())
                {
                    if (element.ValueKind == JsonValueKind.Number)
                    {
                        doubles.Add(element.GetDouble());
                    }
                }
                embeddingData.Embedding = doubles;
            }
        }
        
        return embeddingData;
    }

    public override void Write(Utf8JsonWriter writer, EmbeddingData value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        writer.WriteString("object", value.Object);
        writer.WriteNumber("index", value.Index);
        
        if (!string.IsNullOrEmpty(value.EmbeddingBase64))
        {
            writer.WriteString(EmbeddingObjectType, value.EmbeddingBase64);
            if (!string.IsNullOrEmpty(value.EncodingFormat))
            {
                writer.WriteString("encoding_format", value.EncodingFormat);
            }
        }
        else
        {
            writer.WriteStartArray(EmbeddingObjectType);
            foreach (var embedding in value.Embedding)
            {
                writer.WriteNumberValue(embedding);
            }
            writer.WriteEndArray();
        }
        
        writer.WriteEndObject();
    }
}
