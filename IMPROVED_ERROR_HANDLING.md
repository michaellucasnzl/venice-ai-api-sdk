# Venice AI SDK - Improved Error Handling

This document describes the improved error handling implemented in the Venice AI SDK for better user experience.

## Overview

The Venice AI SDK now provides comprehensive error handling that transforms raw API error responses into user-friendly, actionable error messages. Instead of receiving difficult-to-consume JSON responses, users get structured exception objects with detailed information.

## Key Improvements

### Before (Old Error Handling)
```csharp
try
{
    var response = await client.Chat.CreateChatCompletionAsync(request);
}
catch (VeniceAIException ex)
{
    // Generic error message: "Chat completion failed: Status: 400, Response: {"_errors":[],"seed":{"_errors":["Number must be greater than 0"]}}"
    Console.WriteLine(ex.Message);
}
```

### After (New Error Handling)
```csharp
try
{
    var response = await client.Chat.CreateChatCompletionAsync(request);
}
catch (VeniceAIException ex)
{
    // Structured error information
    Console.WriteLine($"Status Code: {ex.StatusCode}"); // 400
    Console.WriteLine($"Error Code: {ex.ErrorCode}"); // API error code if available
    Console.WriteLine($"Message: {ex.Message}"); // User-friendly message
    
    // Detailed validation errors
    if (ex.ValidationErrors?.Count > 0)
    {
        Console.WriteLine("Validation Errors:");
        foreach (var error in ex.ValidationErrors)
        {
            Console.WriteLine($"  - {error.Key}: {string.Join(", ", error.Value)}");
        }
    }
    
    // Raw response for debugging
    Console.WriteLine($"Raw Response: {ex.RawResponse}");
}
```

## VeniceAIException Properties

The enhanced `VeniceAIException` class now includes the following properties:

- **`StatusCode`** (`int?`): HTTP status code from the API response
- **`ErrorCode`** (`string?`): Error code from the API (for standard errors)
- **`ValidationErrors`** (`Dictionary<string, List<string>>?`): Detailed validation errors by field
- **`RawResponse`** (`string?`): Raw JSON response from the API for debugging
- **`Message`** (`string`): Human-readable error message

## Error Types Handled

### 1. Validation Errors (400 Bad Request)
When the API returns validation errors, the SDK parses them into a structured format:

```json
{
  "_errors": [],
  "seed": {
    "_errors": ["Number must be greater than 0"]
  },
  "temperature": {
    "_errors": ["Temperature must be between 0 and 2"]
  }
}
```

Results in:
```csharp
// ex.Message: "Validation failed for the following fields:\n- seed: Number must be greater than 0\n- temperature: Temperature must be between 0 and 2"
// ex.ValidationErrors["seed"] = ["Number must be greater than 0"]
// ex.ValidationErrors["temperature"] = ["Temperature must be between 0 and 2"]
```

### 2. Standard Errors (401, 403, 500, etc.)
For standard API errors:

```json
{
  "error": "Invalid API key provided"
}
```

Results in:
```csharp
// ex.Message: "API Error: Invalid API key provided"
// ex.ErrorCode: "Invalid API key provided"
// ex.StatusCode: 401
```

### 3. Unknown/Malformed Errors
For any errors that don't match the expected format:

```csharp
// ex.Message: "API Error (Status: 500): <raw response>"
// ex.StatusCode: 500
// ex.RawResponse: "<raw response>"
```

## Usage Examples

### Basic Error Handling
```csharp
try
{
    var response = await client.Chat.CreateChatCompletionAsync(request);
    // Process successful response
}
catch (VeniceAIException ex)
{
    // Handle Venice AI specific errors
    Console.WriteLine($"API Error: {ex.Message}");
    
    if (ex.StatusCode == 401)
    {
        Console.WriteLine("Authentication failed. Please check your API key.");
    }
    else if (ex.StatusCode == 400 && ex.ValidationErrors?.Count > 0)
    {
        Console.WriteLine("Request validation failed:");
        foreach (var error in ex.ValidationErrors)
        {
            Console.WriteLine($"  {error.Key}: {string.Join(", ", error.Value)}");
        }
    }
}
catch (Exception ex)
{
    // Handle other unexpected errors
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

### Handling Specific Validation Errors
```csharp
try
{
    var response = await client.Chat.CreateChatCompletionAsync(request);
}
catch (VeniceAIException ex) when (ex.ValidationErrors?.ContainsKey("seed") == true)
{
    Console.WriteLine($"Seed validation error: {string.Join(", ", ex.ValidationErrors["seed"])}");
    // Handle seed-specific validation error
}
catch (VeniceAIException ex) when (ex.ValidationErrors?.ContainsKey("temperature") == true)
{
    Console.WriteLine($"Temperature validation error: {string.Join(", ", ex.ValidationErrors["temperature"])}");
    // Handle temperature-specific validation error
}
```

## Benefits

1. **User-Friendly Messages**: Clear, actionable error messages instead of raw JSON
2. **Structured Data**: Easy programmatic access to error details
3. **Field-Specific Validation**: Identify exactly which fields have validation errors
4. **Debugging Support**: Raw response still available for debugging
5. **Consistent Error Handling**: All API errors handled through the same exception type
6. **Backward Compatibility**: Existing code continues to work with improved error messages

## Testing

The improved error handling includes comprehensive unit tests that verify:
- Parsing of validation errors with multiple fields
- Handling of standard API errors
- Graceful handling of malformed JSON responses
- Proper error message formatting

Run the error handling demo:
```bash
cd samples/VeniceAI.SDK.ErrorHandlingDemo
dotnet run
```

This demonstrates both authentication errors and validation errors with the improved error handling.
