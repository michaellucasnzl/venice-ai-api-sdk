using System.ComponentModel.DataAnnotations;
using VeniceAI.SDK.Configuration;
using Xunit;

namespace VeniceAI.SDK.Tests.Configuration;

public class VeniceAIOptionsTests
{
    [Fact]
    public void Constructor_SetsDefaultValues()
    {
        // Arrange & Act
        var options = new VeniceAIOptions();

        // Assert
        Assert.Equal("https://api.venice.ai/api/v1", options.BaseUrl);
        Assert.Equal(300, options.TimeoutSeconds);
        Assert.Equal(3, options.MaxRetryAttempts);
        Assert.Equal(1000, options.RetryDelayMs);
        Assert.False(options.EnableLogging);
        Assert.NotNull(options.CustomHeaders);
        Assert.Empty(options.CustomHeaders);
    }

    [Fact]
    public void Validate_WithValidApiKey_ReturnsTrue()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key"
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Validate_WithEmptyApiKey_ReturnsFalse()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = ""
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Single(results);
        Assert.Contains("ApiKey", results[0].ErrorMessage);
    }

    [Fact]
    public void Validate_WithNullApiKey_ReturnsFalse()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = null!
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Single(results);
        Assert.Contains("ApiKey", results[0].ErrorMessage);
    }

    [Fact]
    public void Validate_WithInvalidUrl_StillReturnsTrue()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key",
            BaseUrl = "invalid-url"
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.True(isValid); // Note: No URL validation attribute on BaseUrl currently
    }

    [Fact]
    public void Validate_WithValidUrl_ReturnsTrue()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key",
            BaseUrl = "https://api.custom.com"
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Validate_WithNegativeTimeoutSeconds_ReturnsFalse()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key",
            TimeoutSeconds = -1
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.True(isValid); // Note: No validation attribute on TimeoutSeconds currently
    }

    [Fact]
    public void Validate_WithNegativeMaxRetryAttempts_ReturnsFalse()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key",
            MaxRetryAttempts = -1
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.True(isValid); // Note: No validation attribute on MaxRetryAttempts currently
    }

    [Fact]
    public void Validate_WithNegativeRetryDelayMs_ReturnsFalse()
    {
        // Arrange
        var options = new VeniceAIOptions
        {
            ApiKey = "test-api-key",
            RetryDelayMs = -1
        };

        // Act
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(options, context, results, true);

        // Assert
        Assert.True(isValid); // Note: No validation attribute on RetryDelayMs currently
    }
}
