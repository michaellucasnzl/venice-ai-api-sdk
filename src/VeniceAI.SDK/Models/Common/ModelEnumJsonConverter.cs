using System.Text.Json;
using System.Text.Json.Serialization;
using VeniceAI.SDK.Extensions;

namespace VeniceAI.SDK.Models.Common;

/// <summary>
/// JSON converter for TextModel enum.
/// </summary>
public class TextModelJsonConverter : JsonConverter<TextModel>
{
    public override TextModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for {nameof(TextModel)}.");
        }

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException($"{nameof(TextModel)} cannot be null or empty.");
        }

        return ModelEnumExtensions.ParseTextModel(value);
    }

    public override void Write(Utf8JsonWriter writer, TextModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToModelString());
    }
}

/// <summary>
/// JSON converter for ImageModel enum.
/// </summary>
public class ImageModelJsonConverter : JsonConverter<ImageModel>
{
    public override ImageModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for {nameof(ImageModel)}.");
        }

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException($"{nameof(ImageModel)} cannot be null or empty.");
        }

        return ModelEnumExtensions.ParseImageModel(value);
    }

    public override void Write(Utf8JsonWriter writer, ImageModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToModelString());
    }
}

/// <summary>
/// JSON converter for EmbeddingModel enum.
/// </summary>
public class EmbeddingModelJsonConverter : JsonConverter<EmbeddingModel>
{
    public override EmbeddingModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for {nameof(EmbeddingModel)}.");
        }

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException($"{nameof(EmbeddingModel)} cannot be null or empty.");
        }

        return ModelEnumExtensions.ParseEmbeddingModel(value);
    }

    public override void Write(Utf8JsonWriter writer, EmbeddingModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToModelString());
    }
}

/// <summary>
/// JSON converter for TextToSpeechModel enum.
/// </summary>
public class TextToSpeechModelJsonConverter : JsonConverter<TextToSpeechModel>
{
    public override TextToSpeechModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for {nameof(TextToSpeechModel)}.");
        }

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException($"{nameof(TextToSpeechModel)} cannot be null or empty.");
        }

        return ModelEnumExtensions.ParseTextToSpeechModel(value);
    }

    public override void Write(Utf8JsonWriter writer, TextToSpeechModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToModelString());
    }
}

/// <summary>
/// JSON converter for UpscaleModel enum.
/// </summary>
public class UpscaleModelJsonConverter : JsonConverter<UpscaleModel>
{
    public override UpscaleModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for {nameof(UpscaleModel)}.");
        }

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException($"{nameof(UpscaleModel)} cannot be null or empty.");
        }

        return ModelEnumExtensions.ParseUpscaleModel(value);
    }

    public override void Write(Utf8JsonWriter writer, UpscaleModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToModelString());
    }
}

/// <summary>
/// JSON converter for InpaintModel enum.
/// </summary>
public class InpaintModelJsonConverter : JsonConverter<InpaintModel>
{
    public override InpaintModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for {nameof(InpaintModel)}.");
        }

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException($"{nameof(InpaintModel)} cannot be null or empty.");
        }

        return ModelEnumExtensions.ParseInpaintModel(value);
    }

    public override void Write(Utf8JsonWriter writer, InpaintModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToModelString());
    }
}
