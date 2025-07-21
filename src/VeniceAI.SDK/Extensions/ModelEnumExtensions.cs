using System.ComponentModel;
using System.Reflection;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Extensions;

/// <summary>
/// Extensions for model enums.
/// </summary>
public static class ModelEnumExtensions
{
    private const string ModelCannotBeNullOrEmpty = "Model cannot be null or empty.";
    private const string StyleCannotBeNullOrEmpty = "Style cannot be null or empty.";

    /// <summary>
    /// Gets the string representation of a model enum value.
    /// </summary>
    /// <param name="model">The model enum value.</param>
    /// <returns>The string representation.</returns>
    public static string ToModelString(this TextModel model)
    {
        return GetEnumDescription(model);
    }

    /// <summary>
    /// Gets the string representation of an image model enum value.
    /// </summary>
    /// <param name="model">The image model enum value.</param>
    /// <returns>The string representation.</returns>
    public static string ToModelString(this ImageModel model)
    {
        return GetEnumDescription(model);
    }

    /// <summary>
    /// Gets the string representation of an embedding model enum value.
    /// </summary>
    /// <param name="model">The embedding model enum value.</param>
    /// <returns>The string representation.</returns>
    public static string ToModelString(this EmbeddingModel model)
    {
        return GetEnumDescription(model);
    }

    /// <summary>
    /// Gets the string representation of a TTS model enum value.
    /// </summary>
    /// <param name="model">The TTS model enum value.</param>
    /// <returns>The string representation.</returns>
    public static string ToModelString(this TextToSpeechModel model)
    {
        return GetEnumDescription(model);
    }

    /// <summary>
    /// Gets the string representation of an upscale model enum value.
    /// </summary>
    /// <param name="model">The upscale model enum value.</param>
    /// <returns>The string representation.</returns>
    public static string ToModelString(this UpscaleModel model)
    {
        return GetEnumDescription(model);
    }

    /// <summary>
    /// Gets the string representation of an image style enum value.
    /// </summary>
    /// <param name="style">The image style enum value.</param>
    /// <returns>The string representation.</returns>
    public static string ToStyleString(this ImageStyle style)
    {
        return GetEnumDescription(style);
    }

    /// <summary>
    /// Parses a string to a TextModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <returns>The TextModel enum value.</returns>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static TextModel ParseTextModel(string modelString)
    {
        return ParseEnum<TextModel>(modelString, "text model");
    }

    /// <summary>
    /// Parses a string to an ImageModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <returns>The ImageModel enum value.</returns>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static ImageModel ParseImageModel(string modelString)
    {
        return ParseEnum<ImageModel>(modelString, "image model");
    }

    /// <summary>
    /// Parses a string to an EmbeddingModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <returns>The EmbeddingModel enum value.</returns>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static EmbeddingModel ParseEmbeddingModel(string modelString)
    {
        return ParseEnum<EmbeddingModel>(modelString, "embedding model");
    }

    /// <summary>
    /// Parses a string to a TextToSpeechModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <returns>The TextToSpeechModel enum value.</returns>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static TextToSpeechModel ParseTextToSpeechModel(string modelString)
    {
        return ParseEnum<TextToSpeechModel>(modelString, "text-to-speech model");
    }

    /// <summary>
    /// Parses a string to an UpscaleModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <returns>The UpscaleModel enum value.</returns>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static UpscaleModel ParseUpscaleModel(string modelString)
    {
        return ParseEnum<UpscaleModel>(modelString, "upscale model");
    }

    /// <summary>
    /// Parses a string to an ImageStyle enum value.
    /// </summary>
    /// <param name="styleString">The style string.</param>
    /// <returns>The ImageStyle enum value.</returns>
    /// <exception cref="VeniceAIException">Thrown when the style string is not valid.</exception>
    public static ImageStyle ParseImageStyle(string styleString)
    {
        return ParseEnum<ImageStyle>(styleString, "image style");
    }

    /// <summary>
    /// Tries to parse a string to a TextModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <param name="model">The parsed TextModel enum value.</param>
    /// <returns>True if parsing was successful, false otherwise.</returns>
    public static bool TryParseTextModel(string modelString, out TextModel model)
    {
        return TryParseEnum(modelString, out model);
    }

    /// <summary>
    /// Tries to parse a string to an ImageModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <param name="model">The parsed ImageModel enum value.</param>
    /// <returns>True if parsing was successful, false otherwise.</returns>
    public static bool TryParseImageModel(string modelString, out ImageModel model)
    {
        return TryParseEnum(modelString, out model);
    }

    /// <summary>
    /// Tries to parse a string to an EmbeddingModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <param name="model">The parsed EmbeddingModel enum value.</param>
    /// <returns>True if parsing was successful, false otherwise.</returns>
    public static bool TryParseEmbeddingModel(string modelString, out EmbeddingModel model)
    {
        return TryParseEnum(modelString, out model);
    }

    /// <summary>
    /// Tries to parse a string to a TextToSpeechModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <param name="model">The parsed TextToSpeechModel enum value.</param>
    /// <returns>True if parsing was successful, false otherwise.</returns>
    public static bool TryParseTextToSpeechModel(string modelString, out TextToSpeechModel model)
    {
        return TryParseEnum(modelString, out model);
    }

    /// <summary>
    /// Tries to parse a string to an UpscaleModel enum value.
    /// </summary>
    /// <param name="modelString">The model string.</param>
    /// <param name="model">The parsed UpscaleModel enum value.</param>
    /// <returns>True if parsing was successful, false otherwise.</returns>
    public static bool TryParseUpscaleModel(string modelString, out UpscaleModel model)
    {
        return TryParseEnum(modelString, out model);
    }

    /// <summary>
    /// Tries to parse a string to an ImageStyle enum value.
    /// </summary>
    /// <param name="styleString">The style string.</param>
    /// <param name="style">The parsed ImageStyle enum value.</param>
    /// <returns>True if parsing was successful, false otherwise.</returns>
    public static bool TryParseImageStyle(string styleString, out ImageStyle style)
    {
        return TryParseEnum(styleString, out style);
    }

    /// <summary>
    /// Validates that a model string is a valid text model.
    /// </summary>
    /// <param name="modelString">The model string to validate.</param>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static void ValidateTextModel(string modelString)
    {
        if (string.IsNullOrWhiteSpace(modelString))
        {
            throw new VeniceAIException(ModelCannotBeNullOrEmpty, 400);
        }

        if (!TryParseTextModel(modelString, out _))
        {
            var validModels = string.Join(", ", Enum.GetValues<TextModel>().Select(m => m.ToModelString()));
            throw new VeniceAIException($"Invalid text model '{modelString}'. Valid models are: {validModels}", 400);
        }
    }

    /// <summary>
    /// Validates that a model string is a valid image model.
    /// </summary>
    /// <param name="modelString">The model string to validate.</param>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static void ValidateImageModel(string modelString)
    {
        if (string.IsNullOrWhiteSpace(modelString))
        {
            throw new VeniceAIException(ModelCannotBeNullOrEmpty, 400);
        }

        if (!TryParseImageModel(modelString, out _))
        {
            var validModels = string.Join(", ", Enum.GetValues<ImageModel>().Select(m => m.ToModelString()));
            throw new VeniceAIException($"Invalid image model '{modelString}'. Valid models are: {validModels}", 400);
        }
    }

    /// <summary>
    /// Validates that a model string is a valid embedding model.
    /// </summary>
    /// <param name="modelString">The model string to validate.</param>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static void ValidateEmbeddingModel(string modelString)
    {
        if (string.IsNullOrWhiteSpace(modelString))
        {
            throw new VeniceAIException(ModelCannotBeNullOrEmpty, 400);
        }

        if (!TryParseEmbeddingModel(modelString, out _))
        {
            var validModels = string.Join(", ", Enum.GetValues<EmbeddingModel>().Select(m => m.ToModelString()));
            throw new VeniceAIException($"Invalid embedding model '{modelString}'. Valid models are: {validModels}", 400);
        }
    }

    /// <summary>
    /// Validates that a model string is a valid text-to-speech model.
    /// </summary>
    /// <param name="modelString">The model string to validate.</param>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static void ValidateTextToSpeechModel(string modelString)
    {
        if (string.IsNullOrWhiteSpace(modelString))
        {
            throw new VeniceAIException(ModelCannotBeNullOrEmpty, 400);
        }

        if (!TryParseTextToSpeechModel(modelString, out _))
        {
            var validModels = string.Join(", ", Enum.GetValues<TextToSpeechModel>().Select(m => m.ToModelString()));
            throw new VeniceAIException($"Invalid text-to-speech model '{modelString}'. Valid models are: {validModels}", 400);
        }
    }

    /// <summary>
    /// Validates that a model string is a valid upscale model.
    /// </summary>
    /// <param name="modelString">The model string to validate.</param>
    /// <exception cref="VeniceAIException">Thrown when the model string is not valid.</exception>
    public static void ValidateUpscaleModel(string modelString)
    {
        if (string.IsNullOrWhiteSpace(modelString))
        {
            throw new VeniceAIException(ModelCannotBeNullOrEmpty, 400);
        }

        if (!TryParseUpscaleModel(modelString, out _))
        {
            var validModels = string.Join(", ", Enum.GetValues<UpscaleModel>().Select(m => m.ToModelString()));
            throw new VeniceAIException($"Invalid upscale model '{modelString}'. Valid models are: {validModels}", 400);
        }
    }

    /// <summary>
    /// Validates that a style string is a valid image style.
    /// </summary>
    /// <param name="styleString">The style string to validate.</param>
    /// <exception cref="VeniceAIException">Thrown when the style string is not valid.</exception>
    public static void ValidateImageStyle(string styleString)
    {
        if (string.IsNullOrWhiteSpace(styleString))
        {
            throw new VeniceAIException(StyleCannotBeNullOrEmpty, 400);
        }

        if (!TryParseImageStyle(styleString, out _))
        {
            var validStyles = string.Join(", ", Enum.GetValues<ImageStyle>().Select(s => s.ToStyleString()));
            throw new VeniceAIException($"Invalid image style '{styleString}'. Valid styles are: {validStyles}", 400);
        }
    }

    private static string GetEnumDescription<T>(T enumValue) where T : struct, Enum
    {
        var field = typeof(T).GetField(enumValue.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? enumValue.ToString();
    }

    private static T ParseEnum<T>(string value, string entityType) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new VeniceAIException($"The {entityType} cannot be null or empty.", 400);
        }

        if (TryParseEnum<T>(value, out var result))
        {
            return result;
        }

        var validValues = string.Join(", ", Enum.GetValues<T>().Select(GetEnumDescription));
        throw new VeniceAIException($"Invalid {entityType} '{value}'. Valid values are: {validValues}", 400);
    }

    private static bool TryParseEnum<T>(string value, out T result) where T : struct, Enum
    {
        result = default;

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        // Try to find enum value by description
        foreach (var enumValue in Enum.GetValues<T>())
        {
            if (GetEnumDescription(enumValue).Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = enumValue;
                return true;
            }
        }

        // Try to parse by enum name
        return Enum.TryParse<T>(value, true, out result);
    }
}
