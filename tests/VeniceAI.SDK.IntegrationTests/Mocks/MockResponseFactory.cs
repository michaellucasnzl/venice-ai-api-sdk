using System.Net;
using RichardSzalay.MockHttp;

namespace VeniceAI.SDK.IntegrationTests.Mocks;

/// <summary>
/// Factory for creating mock HTTP responses for Venice AI API calls.
/// Uses the centralized MockHttpResponseFactory for consistent responses.
/// </summary>
public static class MockResponseFactory
{
    /// <summary>
    /// Creates mock responses for all Venice AI API endpoints.
    /// </summary>
    public static MockHttpMessageHandler CreateMockHandler()
    {
        var mockHttp = new MockHttpMessageHandler();

        // Chat completions
        mockHttp.When(HttpMethod.Post, "*/chat/completions")
            .Respond(_ => MockHttpResponseFactory.CreateChatCompletionResponse());

        // Streaming chat completions (same as regular for simplicity)
        mockHttp.When(HttpMethod.Post, "*/chat/completions")
            .WithQueryString("stream", "true")
            .Respond(_ => MockHttpResponseFactory.CreateChatCompletionStreamResponse());

        // Embeddings
        mockHttp.When(HttpMethod.Post, "*/embeddings")
            .Respond(_ => MockHttpResponseFactory.CreateEmbeddingResponse());

        // Audio speech generation
        mockHttp.When(HttpMethod.Post, "*/audio/speech")
            .Respond(_ => MockHttpResponseFactory.CreateAudioResponse());

        // Image generation
        mockHttp.When(HttpMethod.Post, "*/images/generations")
            .Respond(_ => MockHttpResponseFactory.CreateImageGenerationResponse());

        // Simple image generation
        mockHttp.When(HttpMethod.Post, "*/images/generate")
            .Respond(_ => MockHttpResponseFactory.CreateImageGenerationResponse());

        // Image styles
        mockHttp.When(HttpMethod.Get, "*/images/styles")
            .Respond(_ => MockHttpResponseFactory.CreateImageStylesResponse());

        // Models list
        mockHttp.When(HttpMethod.Get, "*/models")
            .Respond(_ => MockHttpResponseFactory.CreateModelsResponse());

        // Individual model
        mockHttp.When(HttpMethod.Get, "*/models/*")
            .Respond(_ => MockHttpResponseFactory.CreateModelResponse());

        // Model traits
        mockHttp.When(HttpMethod.Get, "*/models/traits")
            .Respond(_ => MockHttpResponseFactory.CreateModelTraitsResponse());

        // Model compatibility
        mockHttp.When(HttpMethod.Get, "*/models/compatibility")
            .Respond(_ => MockHttpResponseFactory.CreateModelCompatibilityResponse());

        // Billing usage
        mockHttp.When(HttpMethod.Get, "*/billing/usage*")
            .Respond(_ => MockHttpResponseFactory.CreateBillingUsageResponse());

        return mockHttp;
    }
}
