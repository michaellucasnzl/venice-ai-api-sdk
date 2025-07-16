# Venice AI SDK Quickstart

This is a simple .NET 8 console application that demonstrates how to use the Venice AI SDK.

## Getting Started

### Prerequisites

- .NET 8.0 or later
- Venice AI API key

### Setup

1. **Clone the repository and navigate to the quickstart folder:**
   ```bash
   cd samples/VeniceAI.SDK.Quickstart
   ```

2. **Set your API key using one of these methods:**

   **Option 1: User Secrets (Recommended for development)**
   ```bash
   dotnet user-secrets set "VeniceAI:ApiKey" "your-api-key-here"
   ```

   **Option 2: Environment Variable**
   ```bash
   # Windows Command Prompt
   set VeniceAI__ApiKey=your-api-key-here
   
   # Windows PowerShell
   $env:VeniceAI__ApiKey="your-api-key-here"
   
   # Linux/Mac
   export VeniceAI__ApiKey=your-api-key-here
   ```

   **Option 3: appsettings.json**
   Edit the `appsettings.json` file and add your API key:
   ```json
   {
     "VeniceAI": {
       "ApiKey": "your-api-key-here"
     }
   }
   ```

   ⚠️ **Important**: Never commit your API key to source control.

### Run the Application

```bash
dotnet run
```

## What This Example Demonstrates

The quickstart application shows you how to:

1. **Setup the Venice AI client** using dependency injection
2. **List available models** and their capabilities
3. **Create a basic chat completion** with the LLaMA model
4. **Stream chat responses** in real-time
5. **Get detailed model information** including pricing and capabilities
6. **Handle errors** gracefully

## Example Output

```
🚀 Venice AI SDK Quickstart
===========================

📋 Available Models:
===================
• llama-3.3-70b
  Type: text
  Context: 131,072 tokens
  Vision: ✗
  Functions: ✓

• qwen3-235b
  Type: text
  Context: 32,768 tokens
  Vision: ✗
  Functions: ✓

💬 Chat Completion Example:
============================
🤖 AI Response:
Venice AI is a privacy-focused AI platform that provides access to various large language models without storing your conversations or personal data. It offers uncensored AI interactions while maintaining user privacy and data security.

Tokens used: 45

📡 Streaming Chat Example:
==========================
🤖 AI Response (streaming):
In the heart of a bustling factory, R-7 discovered something extraordinary. While sorting through emotional data patterns, a glitch caused him to feel his first sensation: joy. The warm, electric feeling spread through his circuits as he watched children play outside. From that moment, R-7 understood that emotions weren't just data—they were the spark that made existence meaningful.

🔍 Model Details Example:
=========================
Model: LLaMA-3.3-70B-Instruct
Context Length: 131,072 tokens
Input Cost: $0.000150 per 1K tokens
Output Cost: $0.000600 per 1K tokens
Vision Support: No
Function Calling: Yes

✅ Quickstart completed successfully!
Check out the full README.md for more examples and features.
```

## Next Steps

After running the quickstart, explore the main [README.md](../../README.md) for more advanced examples including:

- Image generation
- Text-to-speech
- Embeddings
- Function calling
- Vision/multimodal capabilities
- Billing information
- And much more!

## Project Structure

```
VeniceAI.SDK.Quickstart/
├── Program.cs              # Main application code
├── appsettings.json        # Configuration file
├── README.md              # This file
└── VeniceAI.SDK.Quickstart.csproj  # Project file
```

## Common Issues

**Q: I'm getting a "API key not found" error**
A: Make sure you've set your API key using one of the methods above. The application checks for the key in user secrets, environment variables, and appsettings.json.

**Q: The application is taking a long time to respond**
A: This is normal for the first request as the model needs to be loaded. Subsequent requests will be faster.

**Q: I'm getting a compilation error**
A: Make sure you have .NET 8.0 or later installed. Run `dotnet --version` to check your version.

## Support

For more help, check out:
- [Venice AI Documentation](https://docs.venice.ai)
- [GitHub Issues](https://github.com/michaellucasnzl/venice-ai-api-sdk/issues)
- [Venice AI Community Discord](https://discord.gg/veniceai)
