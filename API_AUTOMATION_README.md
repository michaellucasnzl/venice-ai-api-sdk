# Venice AI API Change Detection and SDK Regeneration

This directory contains automation tools to detect changes in the Venice AI API and automatically update the SDK accordingly.

## 🔄 How It Works

The system monitors the Venice AI API specification at `https://api.venice.ai/api/v1/swagger.yaml` and automatically:

1. **Detects Changes**: Compares the current API spec with the stored baseline
2. **Generates Client**: Uses NSwag to regenerate the C# client when changes are detected
3. **Creates PR**: Automatically creates a pull request with the changes
4. **Runs Tests**: Ensures the generated code builds and passes unit tests

## 📁 Files

### Primary Script
- **`scripts/regenerate-sdk.sh`** - **Main regeneration script** for all platforms (recommended)
  - Full feature parity with PowerShell version
  - Works on Linux, macOS, and Windows (Git Bash/WSL)
  - Used by GitHub Actions automation

### Legacy Scripts (Optional)
- **`scripts/regenerate-sdk-clean.ps1`** - PowerShell script for Windows users who prefer PowerShell
- **`scripts/regenerate-sdk.bat`** - Basic batch script for Windows fallback

### Configuration
- **`api-monitoring-config.json`** - Configuration for monitoring and generation settings
- **`nswag.json`** - NSwag configuration for C# client generation

### GitHub Actions
- **`.github/workflows/api-change-detection.yml`** - Automated workflow that runs daily

### API Specifications
- **`openapi/venice-ai-api.yaml`** - Baseline Venice AI API specification
- **`openapi/latest-venice-ai-api.yaml`** - Latest downloaded specification (temporary)

## 🚀 Quick Start

### Manual SDK Regeneration

**All Platforms (Recommended):**
```bash
# Make script executable (first time only)
chmod +x scripts/regenerate-sdk.sh

# Basic regeneration
./scripts/regenerate-sdk.sh

# Force regeneration (skip change detection)
./scripts/regenerate-sdk.sh --force

# Skip tests for faster execution
./scripts/regenerate-sdk.sh --skip-tests

# Show help
./scripts/regenerate-sdk.sh --help
```

**Windows PowerShell (Alternative):**
```powershell
# Basic regeneration
./scripts/regenerate-sdk-clean.ps1

# Force regeneration
./scripts/regenerate-sdk-clean.ps1 -Force

# Skip tests
./scripts/regenerate-sdk-clean.ps1 -SkipTests
```

### Script Features

The main `regenerate-sdk.sh` script provides:

✅ **Command Line Options**:
- `--force` / `-f`: Force regeneration even if no changes detected
- `--skip-tests` / `-s`: Skip running tests after generation  
- `--output` / `-o`: Custom output path for generated client
- `--help` / `-h`: Show usage help

✅ **Intelligent Change Detection**:
- Downloads latest Venice AI API specification
- Compares with stored baseline using file diff
- Interactive prompts (unless `--force` is used)
- Generates detailed diff reports

✅ **Automated Version Management**:
- Date-based versioning (1.0.YYYYMMDD)
- Updates all .csproj files automatically
- Extracts API version from specification
- Updates VERSION.md with changelog

✅ **Robust Generation Process**:
- NSwag CLI installation if missing
- JSON configuration manipulation (Python/jq/sed fallback)
- Backup and rollback capabilities
- Comprehensive error handling

✅ **Build and Test Integration**:
- Automatic solution restore and build
- Unit test execution (excludes integration tests)
- Colored output and progress reporting
- Cleanup of temporary files
~~~~
## 🤖 Automated Monitoring

The GitHub Actions workflow runs automatically:
- **Daily at 2 AM UTC** - Checks for API changes using the regeneration script
- **Manual trigger** - Can be triggered manually from the Actions tab

### How It Works

The workflow uses the same `regenerate-sdk.sh` script for consistency:

```yaml
- name: Regenerate SDK using script
  run: |
    chmod +x ./scripts/regenerate-sdk.sh
    ./scripts/regenerate-sdk.sh --force
```

This ensures that:
- ✅ Local development and CI use identical logic
- ✅ No duplication of regeneration code
- ✅ Easier maintenance and testing
- ✅ Consistent behavior across environments

### Manual Trigger
1. Go to the Actions tab in your repository
2. Select "Venice AI API Change Detection"
3. Click "Run workflow"
4. Optionally check "Force update" to regenerate even without changes

### Automated Pull Requests

When changes are detected, the workflow:
1. Runs the regeneration script
2. Commits the updated files
3. Creates a pull request with detailed change information
4. Adds appropriate labels for review

## 🔧 Configuration

### Monitoring Settings
Edit `api-monitoring-config.json` to customize:

- **API Source URL**: Where to fetch the latest spec
- **Schedule**: How often to check for changes
- **Generation Settings**: NSwag configuration
- **Testing Options**: What tests to run
- **Versioning Strategy**: How to version updates

### NSwag Settings
Edit `nswag.json` to customize the generated client:

- **Namespace**: Target namespace for generated code
- **Class Name**: Name of the generated client class
- **Output Path**: Where to save the generated file
- **Generation Options**: Various code generation settings

## 📊 Change Detection

The system uses file-based comparison to detect changes:

1. **File Diff**: Uses `diff` command to compare specifications byte-by-byte
2. **Interactive Prompts**: Asks for confirmation unless `--force` is used
3. **Diff Reports**: Generates detailed reports showing file size changes and timestamps

### What Triggers Regeneration

- New endpoints added/removed
- Schema changes (new properties, type changes, etc.)
- Response format changes
- Authentication changes
- Breaking changes in existing endpoints
- Any modification to the OpenAPI specification

### What's Ignored

The system doesn't ignore any changes - it detects all modifications to the API specification. This ensures you never miss important updates, but you can use `--force` to regenerate without changes if needed.

## 🧪 Testing

After regeneration, the system runs:

1. **Build Validation**: Ensures the code compiles
2. **Unit Tests**: Runs all non-integration tests
3. **Basic Smoke Tests**: Validates basic SDK functionality

Integration tests are not run automatically as they require valid API keys.

## 📝 Generated Pull Requests

When changes are detected, the system creates a PR with:

- **Detailed Change Summary**: What changed in the API
- **Updated Generated Code**: New client with latest API support
- **Version Updates**: Bumped version numbers
- **Test Results**: Build and test status
- **Review Checklist**: Items for manual review

## 🔍 Manual Review Required

Always manually review generated PRs for:

1. **Breaking Changes**: Changes that might break existing code
2. **New Features**: Ensure new functionality is properly exposed
3. **Documentation Updates**: Update README/docs for new features
4. **Integration Tests**: Run with real API keys
5. **Custom Wrapper Updates**: Update any hand-written wrapper code

## 🛠️ Troubleshooting

### Common Issues

**NSwag Not Found:**
```bash
dotnet tool install --global NSwag.ConsoleCore --version 14.1.0
```

**API Spec Download Fails:**
- Check internet connectivity
- Verify the API URL is still valid
- Check if Venice.ai has changed their documentation structure

**Generation Fails:**
- Check the OpenAPI spec is valid
- Review NSwag configuration
- Check output directory permissions

**Tests Fail:**
- Review breaking changes in the API
- Update test fixtures if needed
- Check for new required properties

### Manual Recovery

If automation fails, you can always:

1. Download the spec manually: `curl -sSL https://api.venice.ai/api/v1/swagger.yaml -o openapi/venice-ai-api.yaml`
2. Run the regeneration script manually: `./scripts/regenerate-sdk.sh --force`
3. Fix any compilation errors
4. Update tests as needed

### Script-Specific Issues

**Permission Denied (Linux/macOS):**
```bash
chmod +x scripts/regenerate-sdk.sh
```

**Python/jq Not Found:**
The script automatically falls back to sed if Python or jq are not available.

**JSON Configuration Issues:**
The script handles JSON manipulation gracefully with multiple fallback methods.

## 📈 Monitoring and Alerts

To get notified of API changes:

1. **GitHub Notifications**: Watch the repository for PR notifications
2. **Email Alerts**: Configure in `api-monitoring-config.json`
3. **Slack/Teams**: Add webhook URLs to the configuration

## 🔒 Security

- API keys are never stored in the automation scripts
- Generated code is reviewed before merging
- All changes go through pull request review process
- Secrets are managed through GitHub repository settings

## 🎯 Architecture Benefits

### Unified Script Approach

The system uses a single, feature-complete bash script (`regenerate-sdk.sh`) as the primary automation tool:

✅ **Single Source of Truth**: All regeneration logic is centralized in one script  
✅ **Consistent Behavior**: Same logic runs locally and in CI/CD  
✅ **Cross-Platform**: Works on Linux, macOS, and Windows  
✅ **Maintainable**: Updates only need to be made in one place  
✅ **Testable**: Easy to test and debug locally before CI runs  

### Why Bash Over PowerShell for Automation

While PowerShell is excellent for Windows development, bash is better for CI/CD because:

- **GitHub Actions Native**: Ubuntu runners use bash by default
- **No Extra Dependencies**: No need to install PowerShell Core  
- **Faster Execution**: Lower startup overhead than PowerShell
- **Universal Compatibility**: Works in any Unix-like environment
- **Industry Standard**: Most CI/CD systems expect bash scripts

### Legacy Script Support

The PowerShell and batch scripts remain available for developers who prefer them, but the bash script is the recommended approach for both local development and automation.

## 📚 Further Reading

- [NSwag Documentation](https://github.com/RicoSuter/NSwag)
- [OpenAPI Specification](https://swagger.io/specification/)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Venice AI API Documentation](https://docs.venice.ai)
