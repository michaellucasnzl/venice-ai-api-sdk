# Venice AI SDK API Test

This is a simple test application to verify the Venice AI API endpoints work correctly.

## Setup

Before running this test, you need to configure your API key using one of these methods:

### Method 1: User Secrets (Recommended for Development)

```bash
cd samples/VeniceAI.SDK.ApiTest
dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
```

You can also optionally set a custom base URL:
```bash
dotnet user-secrets set "VeniceAI:BaseUrl" "https://api.venice.ai/api/v1/"
```

### Method 2: Environment Variables

Set the environment variable:
- Windows: `set VENICEAI__APIKEY=your-api-key-here`
- Linux/Mac: `export VENICEAI__APIKEY=your-api-key-here`

## Running the Test

```bash
cd samples/VeniceAI.SDK.ApiTest
dotnet run
```

## What it Tests

1. **Models Endpoint**: Lists available models
2. **Chat Endpoint**: Sends a simple chat completion request

The test will show you if the API endpoints are working correctly and display sample responses.
