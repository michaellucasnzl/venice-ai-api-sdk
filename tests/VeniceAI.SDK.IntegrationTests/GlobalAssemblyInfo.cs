using Xunit;

// Assembly-level trait to mark all tests in this assembly as Integration tests
// This allows filtering out integration tests in CI/CD with --filter "Category!=Integration"
[assembly: Trait("Category", "Integration")]

// Alternative using AssemblyMetadata for broader compatibility
[assembly: System.Reflection.AssemblyMetadataAttribute("Category", "Integration")]
