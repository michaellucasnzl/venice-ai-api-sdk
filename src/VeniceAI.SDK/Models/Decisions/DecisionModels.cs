using System.Text.Json.Serialization;
using VeniceAI.SDK.Models.Common;

namespace VeniceAI.SDK.Models.Decisions;

/// <summary>
/// Request to evaluate a state against a map of typed questions.
/// </summary>
public class DecisionRequest
{
    /// <summary>
    /// The content to evaluate: a plain string for text, or structured data (object/array)
    /// such as chat logs, records, or application state.
    /// </summary>
    [JsonPropertyName("state")]
    public object? State { get; set; }

    /// <summary>
    /// ID of the decision model to use (e.g. "jev-latest").
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Map of typed questions keyed by an id you choose. At least one question is required.
    /// Answers are returned under the same ids.
    /// </summary>
    [JsonPropertyName("questions")]
    public Dictionary<string, DecisionQuestion> Questions { get; set; } = new();
}

/// <summary>
/// A single typed decision question. Use the static factory methods to create one
/// of the supported types: <c>noul</c>, <c>choice</c>, or <c>score</c>.
/// </summary>
public class DecisionQuestion
{
    /// <summary>
    /// The question type: noul, choice, or score.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The question to evaluate against the state (string, object, or array).
    /// </summary>
    [JsonPropertyName("instructions")]
    public object? Instructions { get; set; }

    /// <summary>
    /// Rubric for the question, whose shape depends on <see cref="Type"/>.
    /// </summary>
    [JsonPropertyName("criteria")]
    public object? Criteria { get; set; }

    /// <summary>
    /// Creates a yes/no (probability) question.
    /// </summary>
    /// <param name="instructions">The yes/no question to evaluate.</param>
    /// <param name="trueMeaning">What a yes (value near 1) means.</param>
    /// <param name="falseMeaning">What a no (value near 0) means.</param>
    /// <returns>The configured question.</returns>
    public static DecisionQuestion Noul(string instructions, string? trueMeaning = null, string? falseMeaning = null)
    {
        return new DecisionQuestion
        {
            Type = "noul",
            Instructions = instructions,
            Criteria = (trueMeaning is null && falseMeaning is null)
                ? null
                : new { @true = trueMeaning, @false = falseMeaning }
        };
    }

    /// <summary>
    /// Creates a single-option choice question with a full probability distribution.
    /// </summary>
    /// <param name="instructions">What the model should decide.</param>
    /// <param name="criteria">Map of option name to rubric description (null when an option needs no extra detail).</param>
    /// <returns>The configured question.</returns>
    public static DecisionQuestion Choice(string instructions, Dictionary<string, string?> criteria)
    {
        return new DecisionQuestion
        {
            Type = "choice",
            Instructions = instructions,
            Criteria = criteria
        };
    }

    /// <summary>
    /// Creates a probability-weighted question on an ordered rubric.
    /// </summary>
    /// <param name="instructions">What the model should rate.</param>
    /// <param name="criteria">Ordered level descriptions, lowest to highest (at least two).</param>
    /// <returns>The configured question.</returns>
    public static DecisionQuestion Score(string instructions, List<string> criteria)
    {
        return new DecisionQuestion
        {
            Type = "score",
            Instructions = instructions,
            Criteria = criteria
        };
    }
}

/// <summary>
/// Response from the Decisions API.
/// </summary>
public class DecisionResponse : BaseResponse
{
    /// <summary>
    /// The model that performed the evaluation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// One answer per question, keyed by the same ids used in the request.
    /// </summary>
    [JsonPropertyName("answers")]
    public Dictionary<string, DecisionAnswer> Answers { get; set; } = new();

    /// <summary>
    /// Token usage for the evaluation.
    /// </summary>
    [JsonPropertyName("usage")]
    public DecisionUsage? Usage { get; set; }
}

/// <summary>
/// A single answer returned by the Decisions API. Which properties are populated
/// depends on <see cref="Type"/> (noul, choice, or score).
/// </summary>
public class DecisionAnswer
{
    /// <summary>
    /// The answer type: noul, choice, or score.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The yes/no answer on a scale from 0 (no) to 1 (yes).
    /// </summary>
    [JsonPropertyName("noul")]
    public double? Noul { get; set; }

    /// <summary>
    /// The highest-probability option (choice questions).
    /// </summary>
    [JsonPropertyName("choice")]
    public string? Choice { get; set; }

    /// <summary>
    /// Every option mapped to its probability (choice and score questions).
    /// </summary>
    [JsonPropertyName("probabilities")]
    public Dictionary<string, double>? Probabilities { get; set; }

    /// <summary>
    /// How certain the model is, derived from the probability distribution.
    /// </summary>
    [JsonPropertyName("confidence")]
    public double? Confidence { get; set; }

    /// <summary>
    /// The probability-weighted answer across the levels (score questions).
    /// </summary>
    [JsonPropertyName("score")]
    public double? Score { get; set; }

    /// <summary>
    /// Each level number mapped back to its description (score questions).
    /// </summary>
    [JsonPropertyName("legend")]
    public Dictionary<string, string>? Legend { get; set; }
}

/// <summary>
/// Token usage for a Decisions API request.
/// </summary>
public class DecisionUsage
{
    /// <summary>
    /// Tokens consumed by the state and questions.
    /// </summary>
    [JsonPropertyName("input_tokens")]
    public int InputTokens { get; set; }

    /// <summary>
    /// Tokens produced evaluating the questions.
    /// </summary>
    [JsonPropertyName("output_tokens")]
    public int OutputTokens { get; set; }
}
