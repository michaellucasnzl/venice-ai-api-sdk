using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using VeniceAI.SDK.Generated;

namespace VeniceAI.SDK;

/// <summary>
/// Exception thrown by Venice AI SDK operations.
/// </summary>
public class VeniceAIException : Exception
{
    private const string ErrorsProperty = "_errors";

    /// <summary>
    /// Gets the HTTP status code associated with the error.
    /// </summary>
    public int? StatusCode { get; }

    /// <summary>
    /// Gets the error code from the API response.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Gets the validation errors from the API response.
    /// </summary>
    public Dictionary<string, List<string>>? ValidationErrors { get; }

    /// <summary>
    /// Gets the raw response from the API.
    /// </summary>
    public string? RawResponse { get; }

    /// <summary>
    /// Initializes a new instance of the VeniceAIException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public VeniceAIException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VeniceAIException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public VeniceAIException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VeniceAIException class with detailed error information.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="validationErrors">The validation errors from the API.</param>
    /// <param name="rawResponse">The raw response from the API.</param>
    /// <param name="innerException">The inner exception.</param>
    public VeniceAIException(
        string message,
        int statusCode,
        string? errorCode = null,
        Dictionary<string, List<string>>? validationErrors = null,
        string? rawResponse = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        ValidationErrors = validationErrors;
        RawResponse = rawResponse;
    }

    /// <summary>
    /// Creates a VeniceAIException from an ApiException.
    /// </summary>
    /// <param name="apiException">The API exception to convert.</param>
    /// <returns>A VeniceAIException with detailed error information.</returns>
    public static VeniceAIException FromApiException(ApiException apiException)
    {
        var statusCode = apiException.StatusCode;
        var rawResponse = apiException.Response;

        try
        {
            if (!string.IsNullOrEmpty(rawResponse))
            {
                using var document = JsonDocument.Parse(rawResponse);
                var root = document.RootElement;

                // Try to parse as validation error first
                var validationResult = TryParseValidationError(root);
                if (validationResult.IsValidationError)
                {
                    return new VeniceAIException(
                        validationResult.Message,
                        statusCode,
                        validationErrors: validationResult.ValidationErrors,
                        rawResponse: rawResponse,
                        innerException: apiException);
                }

                // Try to parse as standard error
                var standardResult = TryParseStandardError(root);
                if (standardResult.IsStandardError)
                {
                    return new VeniceAIException(
                        standardResult.Message,
                        statusCode,
                        errorCode: standardResult.ErrorCode,
                        rawResponse: rawResponse,
                        innerException: apiException);
                }
            }
        }
        catch (JsonException)
        {
            // If we can't parse the JSON, fall through to default handling
        }

        // Fallback to using the raw response
        var message = $"API Error (Status: {statusCode}): {rawResponse}";
        return new VeniceAIException(message, statusCode, rawResponse: rawResponse, innerException: apiException);
    }

    private static (bool IsValidationError, string Message, Dictionary<string, List<string>>? ValidationErrors) TryParseValidationError(JsonElement root)
    {
        if (!root.TryGetProperty(ErrorsProperty, out var errorsElement) || errorsElement.ValueKind != JsonValueKind.Array)
        {
            return (false, string.Empty, null);
        }

        var validationErrors = new Dictionary<string, List<string>>();

        // Parse root-level errors
        ParseErrorsArray(errorsElement, validationErrors, ErrorsProperty);

        // Parse field-specific errors
        foreach (var property in root.EnumerateObject())
        {
            if (property.Name != ErrorsProperty && property.Value.ValueKind == JsonValueKind.Object &&
                property.Value.TryGetProperty(ErrorsProperty, out var fieldErrors) &&
                fieldErrors.ValueKind == JsonValueKind.Array)
            {
                ParseErrorsArray(fieldErrors, validationErrors, property.Name);
            }
        }

        if (validationErrors.Count > 0)
        {
            var message = "Validation failed for the following fields:\n" +
                         string.Join("\n", validationErrors.Select(kvp =>
                             $"- {kvp.Key}: {string.Join(", ", kvp.Value)}"));
            return (true, message, validationErrors);
        }

        return (false, string.Empty, null);
    }

    private static (bool IsStandardError, string Message, string? ErrorCode) TryParseStandardError(JsonElement root)
    {
        if (root.TryGetProperty("error", out var errorElement))
        {
            var errorCode = errorElement.GetString();
            var message = $"API Error: {errorCode}";
            return (true, message, errorCode);
        }

        return (false, string.Empty, null);
    }

    private static void ParseErrorsArray(JsonElement errorsElement, Dictionary<string, List<string>> validationErrors, string fieldName)
    {
        var fieldErrorList = new List<string>();
        foreach (var error in errorsElement.EnumerateArray())
        {
            if (error.ValueKind == JsonValueKind.String)
            {
                fieldErrorList.Add(error.GetString() ?? "Unknown error");
            }
        }
        if (fieldErrorList.Count > 0)
        {
            validationErrors[fieldName] = fieldErrorList;
        }
    }
}
