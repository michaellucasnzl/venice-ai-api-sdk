# Contributing to Venice AI .NET SDK

Thank you for your interest in contributing to the Venice AI .NET SDK!

## How to Contribute

1. **Fork** the repository
2. **Create a branch** for your change (`git checkout -b feature/my-feature`)
3. **Make your changes** following the existing code style and conventions
4. **Add or update tests** for any new or changed functionality
5. **Run all tests** to ensure nothing is broken:
   ```bash
   dotnet test tests/VeniceAI.SDK.Tests
   ```
6. **Update the CHANGELOG** if your change is user-facing
7. **Submit a Pull Request** against the `main` branch

## Important Notes

- **Direct pushes to `main` are not accepted.** All changes must come through Pull Requests.
- All PRs will be reviewed and merged by the maintainer.
- Please keep PRs focused on a single change or feature.

## Code Style

- Follow the existing code style and conventions in the project
- Use meaningful names for variables, methods, and classes
- Include XML documentation comments on public APIs
- Keep methods focused and reasonably sized

## Reporting Issues

If you find a bug or have a feature request, please [open an issue](https://github.com/michaellucasnzl/venice-ai-api-sdk/issues) with:

- A clear title and description
- Steps to reproduce (for bugs)
- Expected vs actual behavior
- SDK version and .NET version

## License

By contributing, you agree that your contributions will be licensed under the [MIT License](../LICENSE).
