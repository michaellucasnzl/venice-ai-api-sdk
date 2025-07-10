# Setting Your Venice AI API Key

To use the Venice AI SDK with your API key, you need to configure it using .NET User Secrets. This keeps your API key secure and out of source control.

## For Integration Tests

Run this command from the root of the repository:

```bash
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here" --project tests/VeniceAI.SDK.IntegrationTests
```

## For Sample Application

Run this command from the root of the repository:

```bash
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here" --project samples/VeniceAI.SDK.Samples
```

## Alternative: Environment Variable

You can also set the API key as an environment variable:

```bash
# Windows Command Prompt
set VeniceAI__ApiKey=your-api-key-here

# Windows PowerShell
$env:VeniceAI__ApiKey="your-api-key-here"

# Linux/Mac
export VeniceAI__ApiKey=your-api-key-here
```

## Running the Tests

After setting your API key, you can run the integration tests:

```bash
dotnet test tests/VeniceAI.SDK.IntegrationTests
```

## Running the Sample Application

After setting your API key, you can run the sample application:

```bash
dotnet run --project samples/VeniceAI.SDK.Samples
```

## Verifying Your Setup

To verify that your API key is configured correctly, you can run a simple test:

```bash
dotnet test tests/VeniceAI.SDK.IntegrationTests --filter "TestCategory=Chat"
```

This will run only the chat-related tests to verify your API key is working.
