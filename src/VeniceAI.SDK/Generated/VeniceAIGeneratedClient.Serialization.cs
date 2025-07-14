using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace VeniceAI.SDK.Generated
{
    public partial class VeniceAIGeneratedClient
    {
        /// <summary>
        /// Configures the JsonSerializerOptions to properly handle System.Runtime.Serialization attributes
        /// and snake_case naming policy used by the Venice AI API.
        /// </summary>
        static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
            // Configure naming policy to match API expectations
            settings.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            
            // Handle null values appropriately  
            settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            
            // Add enum converter that respects EnumMember attributes
            settings.Converters.Add(new EnumMemberConverter());
        }
    }

    /// <summary>
    /// Custom JsonConverter that respects System.Runtime.Serialization.EnumMember attributes
    /// for System.Text.Json serialization.
    /// </summary>
    public class EnumMemberConverter : JsonConverterFactory
    {
        public override bool CanConvert(System.Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter? CreateConverter(System.Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(EnumMemberConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter?)Activator.CreateInstance(converterType);
        }
    }

    /// <summary>
    /// Generic enum converter that reads EnumMember attribute values.
    /// </summary>
    public class EnumMemberConverter<T> : JsonConverter<T> where T : struct, System.Enum
    {
        private readonly Dictionary<string, T> _nameToValue = new();
        private readonly Dictionary<T, string> _valueToName = new();

        public EnumMemberConverter()
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                if (field.IsLiteral)
                {
                    var enumValue = (T)field.GetValue(null)!;
                    var memberAttr = field.GetCustomAttributes(typeof(System.Runtime.Serialization.EnumMemberAttribute), false)
                                          .FirstOrDefault() as System.Runtime.Serialization.EnumMemberAttribute;
                    
                    var name = memberAttr?.Value ?? field.Name;
                    _nameToValue[name] = enumValue;
                    _valueToName[enumValue] = name;
                }
            }
        }

        public override T Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value != null && _nameToValue.TryGetValue(value, out var enumValue))
            {
                return enumValue;
            }
            
            throw new JsonException($"Unable to convert \"{value}\" to enum {typeof(T).Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (_valueToName.TryGetValue(value, out var name))
            {
                writer.WriteStringValue(name);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
