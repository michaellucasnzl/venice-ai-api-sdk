using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace VeniceAI.SDK.ApiTest;

/// <summary>
/// Simple test to verify the Venice AI API endpoint works.
/// </summary>
public class Program
{
    // Protected constructor to satisfy code analysis for utility class
    protected Program() { }

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Venice AI API Test");
        Console.WriteLine("==================");
        
        // Load configuration from user secrets
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();

        var apiKey = configuration["VeniceAI:ApiKey"];
        // Default URL for Venice AI API - configurable via user secrets
#pragma warning disable CA1055 // URI-like parameters should not be strings
        var baseUrl = configuration["VeniceAI:BaseUrl"] ?? "https://api.venice.ai/api/v1/";
#pragma warning restore CA1055
        
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("❌ API key not found!");
            Console.WriteLine("Please set your API key using one of these methods:");
            Console.WriteLine("1. User Secrets: dotnet user-secrets set \"VeniceAI:ApiKey\" \"your-api-key-here\"");
            Console.WriteLine("2. Environment Variable: set VENICEAI__APIKEY=your-api-key-here");
            return;
        }

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        
        try
        {
            // Test models endpoint
            Console.WriteLine("Testing models endpoint...");
            var response = await httpClient.GetAsync("models");
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"URL: {response.RequestMessage?.RequestUri}");
            
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {content.Substring(0, Math.Min(200, content.Length))}...");
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("✓ Models endpoint works!");
            }
            else
            {
                Console.WriteLine("✗ Models endpoint failed");
            }
            
            // Test chat endpoint
            Console.WriteLine("\nTesting chat endpoint...");
            var chatPayload = """
                {
                    "model": "llama-3.3-70b",
                    "messages": [
                        {"role": "user", "content": "Hello"}
                    ],
                    "max_tokens": 10
                }
                """;
            
            var chatContent = new StringContent(chatPayload, Encoding.UTF8);
            chatContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var chatResponse = await httpClient.PostAsync("chat/completions", chatContent);
            Console.WriteLine($"Chat Status: {chatResponse.StatusCode}");
            Console.WriteLine($"Chat URL: {chatResponse.RequestMessage?.RequestUri}");
            
            var chatResult = await chatResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Chat Response: {chatResult.Substring(0, Math.Min(200, chatResult.Length))}...");
            
            if (chatResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("✓ Chat endpoint works!");
            }
            else
            {
                Console.WriteLine("✗ Chat endpoint failed");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
