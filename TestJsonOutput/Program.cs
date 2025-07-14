using System.Text.Json;
using System.Text.Json.Serialization;

var JsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

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
            owned_by = "venice.ai",
            type = "text"
        }
    }
};

var json = JsonSerializer.Serialize(response, JsonOptions);
Console.WriteLine("Generated JSON:");
Console.WriteLine(json);
