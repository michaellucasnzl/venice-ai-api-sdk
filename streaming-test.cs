using VeniceAI.SDK;
using VeniceAI.SDK.Models.Chat;

// Create Venice AI client
var client = new VeniceAIClient("key-goes-here");

// Create a streaming chat request
var streamRequest = new ChatCompletionRequest
{
    Model = "llama-3.3-70b",
    Messages = new List<ChatMessage>
    {
        new UserMessage("Write a short poem about artificial intelligence.")
    },
    MaxTokens = 100,
    Temperature = 0.8
};

Console.WriteLine("=== Streaming Test ===");
Console.Write("AI Poem: ");

try
{
    await foreach (var chunk in client.Chat.CreateChatCompletionStreamAsync(streamRequest))
    {
        if (chunk.IsSuccess && chunk.Choices?.Count > 0)
        {
            var content = chunk.Choices[0].Message.Content;
            if (!string.IsNullOrEmpty(content))
            {
                Console.Write(content);
                await Task.Delay(50); // Small delay to make streaming visible
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\nError: {ex.Message}");
}

Console.WriteLine("\n=== Streaming Test Complete ===");
