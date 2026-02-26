using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Characters;

/// <summary>
/// Response from listing characters.
/// </summary>
public class CharactersResponse : BaseResponse
{
    /// <summary>
    /// The object type, which is always "list".
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "list";

    /// <summary>
    /// List of available characters.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Character> Data { get; set; } = new();
}

/// <summary>
/// Character information.
/// </summary>
public class Character
{
    /// <summary>
    /// The unique slug identifier for the character.
    /// </summary>
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// The display name of the character.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A brief description of the character.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The character's avatar image URL.
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// The character's system prompt or personality definition.
    /// </summary>
    [JsonPropertyName("system_prompt")]
    public string? SystemPrompt { get; set; }

    /// <summary>
    /// Categories or tags associated with the character.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Whether the character is publicly available.
    /// </summary>
    [JsonPropertyName("is_public")]
    public bool IsPublic { get; set; }

    /// <summary>
    /// The username of the character's creator.
    /// </summary>
    [JsonPropertyName("creator")]
    public string? Creator { get; set; }

    /// <summary>
    /// The number of conversations with this character.
    /// </summary>
    [JsonPropertyName("conversation_count")]
    public int? ConversationCount { get; set; }

    /// <summary>
    /// When the character was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// When the character was last updated.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Response from getting a single character.
/// </summary>
public class CharacterResponse : BaseResponse
{
    /// <summary>
    /// The character data.
    /// </summary>
    [JsonPropertyName("data")]
    public Character? Data { get; set; }
}
