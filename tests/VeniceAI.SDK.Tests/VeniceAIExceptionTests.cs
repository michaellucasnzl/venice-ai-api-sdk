using System.Collections.Generic;
using System.Text.Json;
using VeniceAI.SDK;
using Xunit;

namespace VeniceAI.SDK.Tests;

public class VeniceAIExceptionTests
{
    [Fact]
    public void FromHttpException_WithValidationErrors_ShouldParseCorrectly()
    {
        // Arrange
        var validationErrorResponse = """
            {
                "_errors": [],
                "seed": {
                    "_errors": ["Number must be greater than 0"]
                }
            }
            """;

        // Act
        var veniceException = VeniceAIException.FromHttpException(400, validationErrorResponse);

        // Assert
        Assert.Equal(400, veniceException.StatusCode);
        Assert.Contains("Validation failed for the following fields", veniceException.Message);
        Assert.Contains("seed: Number must be greater than 0", veniceException.Message);
        Assert.NotNull(veniceException.ValidationErrors);
        Assert.True(veniceException.ValidationErrors.ContainsKey("seed"));
        Assert.Equal("Number must be greater than 0", veniceException.ValidationErrors["seed"][0]);
        Assert.Equal(validationErrorResponse, veniceException.RawResponse);
    }

    [Fact]
    public void FromHttpException_WithStandardError_ShouldParseCorrectly()
    {
        // Arrange
        var standardErrorResponse = """
            {
                "error": "Invalid API key provided"
            }
            """;

        // Act
        var veniceException = VeniceAIException.FromHttpException(401, standardErrorResponse);

        // Assert
        Assert.Equal(401, veniceException.StatusCode);
        Assert.Equal("API Error: Invalid API key provided", veniceException.Message);
        Assert.Equal("Invalid API key provided", veniceException.ErrorCode);
        Assert.Equal(standardErrorResponse, veniceException.RawResponse);
    }

    [Fact]
    public void FromHttpException_WithMultipleValidationErrors_ShouldParseCorrectly()
    {
        // Arrange
        var multipleErrorResponse = """
            {
                "_errors": ["Request is invalid"],
                "temperature": {
                    "_errors": ["Temperature must be between 0 and 2"]
                },
                "max_tokens": {
                    "_errors": ["Max tokens must be positive", "Max tokens cannot exceed 4096"]
                }
            }
            """;

        // Act
        var veniceException = VeniceAIException.FromHttpException(400, multipleErrorResponse);

        // Assert
        Assert.Equal(400, veniceException.StatusCode);
        Assert.Contains("Validation failed for the following fields", veniceException.Message);
        Assert.Contains("_errors: Request is invalid", veniceException.Message);
        Assert.Contains("temperature: Temperature must be between 0 and 2", veniceException.Message);
        Assert.Contains("max_tokens: Max tokens must be positive, Max tokens cannot exceed 4096", veniceException.Message);

        Assert.NotNull(veniceException.ValidationErrors);
        Assert.Equal(3, veniceException.ValidationErrors.Count);
        Assert.True(veniceException.ValidationErrors.ContainsKey("_errors"));
        Assert.True(veniceException.ValidationErrors.ContainsKey("temperature"));
        Assert.True(veniceException.ValidationErrors.ContainsKey("max_tokens"));
        Assert.Equal(2, veniceException.ValidationErrors["max_tokens"].Count);
    }

    [Fact]
    public void FromHttpException_WithInvalidJson_ShouldHandleGracefully()
    {
        // Arrange
        var invalidJsonResponse = "Invalid JSON response";

        // Act
        var veniceException = VeniceAIException.FromHttpException(500, invalidJsonResponse);

        // Assert
        Assert.Equal(500, veniceException.StatusCode);
        Assert.Contains("API Error (Status: 500)", veniceException.Message);
        Assert.Contains("Invalid JSON response", veniceException.Message);
        Assert.Equal(invalidJsonResponse, veniceException.RawResponse);
    }

    [Fact]
    public void FromHttpException_WithEmptyResponse_ShouldHandleGracefully()
    {
        // Arrange
        var emptyResponse = "";

        // Act
        var veniceException = VeniceAIException.FromHttpException(500, emptyResponse);

        // Assert
        Assert.Equal(500, veniceException.StatusCode);
        Assert.Contains("API Error (Status: 500)", veniceException.Message);
        Assert.Equal("", veniceException.RawResponse);
    }
}
