# HttpClient Separation Guide

When using the Venice AI SDK in applications that also use HttpClient for other services, it's important to ensure proper separation to avoid configuration conflicts and maintain clean architecture.

**Important:** The Venice AI SDK automatically manages the API endpoint (`https://api.venice.ai/api/v1/`) and authentication headers. You should not configure these manually as the SDK handles them internally.

## The Problem

Without proper separation, you might encounter:

1. **Configuration Conflicts**: Global HttpClient settings affecting Venice AI requests
2. **Timeout Issues**: Different services requiring different timeout configurations
3. **Header Conflicts**: Custom headers intended for one service affecting another
4. **Testing Difficulties**: Inability to mock different HttpClients independently
5. **Endpoint Management**: Manually managing API endpoints when the SDK should handle this

## Solutions Provided by Venice AI SDK

### 1. Named HttpClient Registration (Default - Recommended)

The SDK uses a named HttpClient ("VeniceAI") via `IHttpClientFactory` and automatically configures all Venice AI-specific settings:

```csharp
services.AddVeniceAI("your-api-key");
```

**What the SDK automatically handles:**
- Base URL: `https://api.venice.ai/api/v1/`
- Authorization header: `Bearer your-api-key`
- Default timeout: 300 seconds (5 minutes)
- Proper HttpClient lifetime management

**Benefits:**
- Automatic separation from your HttpClients
- No manual endpoint configuration needed
- No configuration interference

### 2. Custom HttpClient Configuration (For Additional Settings Only)

Configure additional settings for the Venice AI HttpClient without affecting others:

```csharp
services.AddVeniceAI("your-api-key", httpClient =>
{
    httpClient.Timeout = TimeSpan.FromMinutes(10); // Longer timeout for complex requests
    httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
    // Note: Don't configure BaseAddress or Authorization - SDK handles these
});
```

**What you should configure:**
- Timeout settings
- User-Agent headers
- Custom application headers
- Retry policies (via custom handlers)

**What the SDK handles automatically:**
- BaseAddress: Always set to Venice AI API endpoint
- Authorization: Bearer token from your API key
- Content-Type: Set appropriately for each request type

**Benefits:**
- Venice AI-specific configuration without affecting other clients
- SDK ensures correct endpoint and authentication
- Full control over performance-related settings

### 3. Provided HttpClient Instance

Supply your own pre-configured HttpClient (SDK will still configure Venice AI-specific settings):

```csharp
var customHttpClient = new HttpClient();
customHttpClient.Timeout = TimeSpan.FromMinutes(10);
// Don't set BaseAddress or Authorization - SDK will handle these

services.AddVeniceAI("your-api-key", customHttpClient);
```

**Benefits:**
- Complete control over HttpClient creation
- Can reuse existing HttpClient instances
- Useful for advanced scenarios (custom handlers, etc.)
- SDK ensures correct Venice AI configuration

**Important:** Even with a provided HttpClient, the SDK will automatically set the BaseAddress and Authorization header.

### 4. Custom HttpClient Name

Avoid naming conflicts by using a custom name:

```csharp
services.AddVeniceAI(options =>
{
    options.ApiKey = "your-api-key";
    options.HttpClientName = "MyVeniceAIClient";
});
```

**Benefits:**
- Prevents naming conflicts
- Multiple Venice AI configurations possible
- Clear identification in debugging

## Best Practices

### ✅ Do This

**For Simple Applications:**
```csharp
services.AddVeniceAI("your-api-key");
```

**For Applications with Multiple HTTP Services:**
```csharp
// Your API service
services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://api.myservice.com/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Venice AI service - SDK handles the endpoint
services.AddVeniceAI("your-api-key", httpClient =>
{
    httpClient.Timeout = TimeSpan.FromMinutes(5);
});
```

**For Custom HttpClient Names:**
```csharp
services.AddVeniceAI(options =>
{
    options.ApiKey = "your-api-key";
    options.HttpClientName = "MyVeniceAIClient";
    options.TimeoutSeconds = 600;
});
```

### ❌ Don't Do This

**Don't configure Venice AI endpoints manually:**
```csharp
// ❌ WRONG - SDK automatically handles this
services.AddVeniceAI("your-api-key", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.venice.ai/api/v1/");
});

// ❌ WRONG - SDK automatically sets authorization
services.AddVeniceAI("your-api-key", httpClient =>
{
    httpClient.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", "your-api-key");
});
```

**Don't use global HttpClient configuration:**
```csharp
// ❌ WRONG - This affects ALL HttpClients
services.AddHttpClient(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
});

services.AddVeniceAI("your-api-key"); // Will inherit global settings
```

### For Testing
Create separate configurations for different environments:
```csharp
#if DEBUG
services.AddVeniceAI("test-api-key", httpClient =>
{
    httpClient.Timeout = TimeSpan.FromSeconds(10); // Shorter for tests
});
#else
services.AddVeniceAI("prod-api-key", httpClient =>
{
    httpClient.Timeout = TimeSpan.FromMinutes(5);
});
#endif
```

### For Advanced Scenarios
Use custom HttpClient with message handlers:
```csharp
var handler = new HttpClientHandler();
// Configure handler...

var httpClient = new HttpClient(handler);
// Configure client...

services.AddVeniceAI("your-api-key", httpClient);
```

## Architecture Benefits

1. **Separation of Concerns**: Each service has its own HttpClient configuration
2. **Testability**: Easy to mock individual HttpClients
3. **Maintainability**: Clear ownership of HttpClient configurations
4. **Performance**: Proper HttpClient lifetime management via factory
5. **Flexibility**: Multiple configuration strategies available

## Migration from Shared HttpClient

If you're currently using a shared HttpClient approach, here's how to migrate:

### Before (Problematic)
```csharp
// Global HttpClient affecting everything
services.AddHttpClient(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
});

// Venice AI forced to use global configuration
services.AddVeniceAI("your-api-key");
```

### After (Separated)
```csharp
// Your API service with specific configuration
services.AddHttpClient("MyApiClient", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
});

// Venice AI with its own configuration
services.AddVeniceAI("your-api-key", httpClient =>
{
    httpClient.Timeout = TimeSpan.FromMinutes(5); // AI requests need more time
    httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp-AI/1.0");
});
```

This ensures each service has appropriate configurations without interference.
