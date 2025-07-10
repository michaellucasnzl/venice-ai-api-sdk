# Contributing to Venice AI .NET SDK

We welcome contributions to the Venice AI .NET SDK! This document provides guidelines for contributing to the project.

## Code of Conduct

By participating in this project, you agree to abide by our Code of Conduct. Please read it before contributing.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request, please create an issue on GitHub:

1. Check if the issue already exists
2. Use the appropriate issue template
3. Provide detailed information about the problem
4. Include reproduction steps for bugs
5. Add relevant labels

### Submitting Pull Requests

1. Fork the repository
2. Create a feature branch from `main`
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Update documentation if necessary
7. Submit a pull request

### Development Setup

1. Clone the repository:
```bash
git clone https://github.com/veniceai/venice-ai-api-sdk.git
cd venice-ai-api-sdk
```

2. Install .NET 8.0 SDK:
```bash
# Download from https://dotnet.microsoft.com/download/dotnet/8.0
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Build the solution:
```bash
dotnet build
```

5. Run tests:
```bash
dotnet test
```

### Project Structure

```
venice-ai-api-sdk/
├── src/
│   └── VeniceAI.SDK/          # Main SDK library
├── tests/
│   ├── VeniceAI.SDK.Tests/    # Unit tests
│   └── VeniceAI.SDK.IntegrationTests/ # Integration tests
├── samples/
│   └── VeniceAI.SDK.Samples/  # Sample applications
├── docs/                      # Documentation
└── README.md
```

### Coding Standards

- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Include unit tests for new features
- Maintain backward compatibility
- Use async/await patterns appropriately

### Testing

#### Unit Tests

Run unit tests with:
```bash
dotnet test tests/VeniceAI.SDK.Tests
```

#### Integration Tests

Integration tests require a valid API key:
```bash
export VENICE_AI_API_KEY="your-api-key"
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

Or set it in `appsettings.json`:
```json
{
  "VeniceAI": {
    "ApiKey": "your-api-key"
  }
}
```

### Adding New Features

When adding new features:

1. Create models in the appropriate namespace
2. Add service interfaces and implementations
3. Update the main client interface
4. Add comprehensive tests
5. Update documentation
6. Add samples if applicable

### API Changes

For changes to the public API:

1. Consider backward compatibility
2. Update XML documentation
3. Add changelog entry
4. Update README examples
5. Consider version bump requirements

### Documentation

- Update README.md for new features
- Add XML documentation for public APIs
- Update CHANGELOG.md
- Add examples for new functionality
- Update API reference documentation

### Release Process

1. Update version in project files
2. Update CHANGELOG.md
3. Create release notes
4. Test thoroughly
5. Create GitHub release
6. Publish to NuGet

## Style Guidelines

### Code Style

- Use 4 spaces for indentation
- Follow .NET naming conventions
- Use `var` when type is obvious
- Prefer explicit types for clarity
- Use meaningful names for variables and methods

### Example:
```csharp
// Good
public async Task<ChatCompletionResponse> CreateChatCompletionAsync(
    ChatCompletionRequest request, 
    CancellationToken cancellationToken = default)
{
    if (request == null)
        throw new ArgumentNullException(nameof(request));
    
    var response = await SendPostRequestAsync<ChatCompletionResponse>(
        "/chat/completions", 
        request, 
        cancellationToken);
    
    return response;
}

// Bad
public async Task<ChatCompletionResponse> CreateAsync(ChatCompletionRequest r, CancellationToken ct = default)
{
    var resp = await SendPostRequestAsync<ChatCompletionResponse>("/chat/completions", r, ct);
    return resp;
}
```

### Documentation Style

- Use XML documentation for all public APIs
- Include parameter descriptions
- Provide usage examples
- Document exceptions that may be thrown

### Example:
```csharp
/// <summary>
/// Creates a chat completion using the specified request.
/// </summary>
/// <param name="request">The chat completion request containing messages and parameters.</param>
/// <param name="cancellationToken">Token to cancel the operation.</param>
/// <returns>A task representing the chat completion response.</returns>
/// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
/// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
/// <example>
/// <code>
/// var request = new ChatCompletionRequest
/// {
///     Model = "llama-3.3-70b",
///     Messages = new List&lt;ChatMessage&gt;
///     {
///         new UserMessage("Hello!")
///     }
/// };
/// 
/// var response = await client.Chat.CreateChatCompletionAsync(request);
/// Console.WriteLine(response.Choices[0].Message.Content);
/// </code>
/// </example>
public async Task<ChatCompletionResponse> CreateChatCompletionAsync(
    ChatCompletionRequest request, 
    CancellationToken cancellationToken = default)
```

## Questions?

If you have questions about contributing, please:

1. Check the documentation
2. Look at existing issues
3. Create a new issue with the "question" label
4. Join our Discord community
5. Email us at support@venice.ai

Thank you for contributing to the Venice AI .NET SDK!
