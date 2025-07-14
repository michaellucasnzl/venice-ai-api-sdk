#!/bin/bash

# Venice AI SDK Regeneration Script
# This script downloads the latest Venice AI API spec and regenerates the SDK

set -euo pipefail

# Default values
FORCE=false
SKIP_TESTS=false
OUTPUT_PATH="src/VeniceAI.SDK/Generated/VeniceAIGeneratedClient.cs"

# Color functions
print_success() { echo -e "\033[32m[SUCCESS]\033[0m $1"; }
print_info() { echo -e "\033[34m[INFO]\033[0m $1"; }
print_warning() { echo -e "\033[33m[WARNING]\033[0m $1"; }
print_error() { echo -e "\033[31m[ERROR]\033[0m $1"; }

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --force|-f)
            FORCE=true
            shift
            ;;
        --skip-tests|-s)
            SKIP_TESTS=true
            shift
            ;;
        --output|-o)
            OUTPUT_PATH="$2"
            shift 2
            ;;
        --help|-h)
            echo "Usage: $0 [OPTIONS]"
            echo "Options:"
            echo "  --force, -f       Force regeneration even if no changes detected"
            echo "  --skip-tests, -s  Skip running tests after generation"
            echo "  --output, -o      Specify output path for generated client"
            echo "  --help, -h        Show this help message"
            exit 0
            ;;
        *)
            print_error "Unknown option: $1"
            echo "Use --help for usage information"
            exit 1
            ;;
    esac
done

echo -e "\033[36mVenice AI SDK Regeneration Script\033[0m"
echo -e "\033[36m==================================\033[0m"
echo ""

# Check if we're in the right directory
if [ ! -f "VeniceAI.sln" ]; then
    print_error "This script must be run from the root of the Venice AI SDK repository"
    exit 1
fi

# Create openapi directory if it doesn't exist
mkdir -p openapi

# Download latest API spec
print_info "Downloading latest Venice AI API specification..."

# Try multiple potential endpoints for the Venice AI API spec
if curl -sSL https://api.venice.ai/api/v1/swagger.yaml -o openapi/latest-venice-ai-api.yaml; then
    print_success "Downloaded latest API specification from swagger.yaml endpoint"
elif curl -sSL https://api.venice.ai/swagger.yaml -o openapi/latest-venice-ai-api.yaml; then
    print_success "Downloaded latest API specification from root swagger.yaml endpoint"
elif curl -sSL https://api.venice.ai/api/v1/openapi.yaml -o openapi/latest-venice-ai-api.yaml; then
    print_success "Downloaded latest API specification from openapi.yaml endpoint"
elif curl -sSL https://api.venice.ai/openapi.yaml -o openapi/latest-venice-ai-api.yaml; then
    print_success "Downloaded latest API specification from root openapi.yaml endpoint"
else
    print_error "Failed to download API specification from any known endpoint"
    print_info "Tried endpoints:"
    print_info "  - https://api.venice.ai/api/v1/swagger.yaml"
    print_info "  - https://api.venice.ai/swagger.yaml"
    print_info "  - https://api.venice.ai/api/v1/openapi.yaml"
    print_info "  - https://api.venice.ai/openapi.yaml"
    exit 1
fi

# Check if baseline exists for comparison
if [ -f "openapi/venice-ai-api.yaml" ]; then
    print_info "Comparing with existing specification..."
    
    if ! diff -q "openapi/venice-ai-api.yaml" "openapi/latest-venice-ai-api.yaml" >/dev/null 2>&1; then
        print_success "Changes detected in API specification"
        
        # Save a simple diff report
        {
            echo "# Venice AI API Changes Detected"
            echo "Generated on: $(date)"
            echo ""
            echo "Baseline file size: $(wc -c < 'openapi/venice-ai-api.yaml') bytes"
            echo "Latest file size: $(wc -c < 'openapi/latest-venice-ai-api.yaml') bytes"
            echo ""
            echo "Files are different. Manual review of changes recommended."
        } > api-diff-report.txt
        print_info "Diff report saved to api-diff-report.txt"
    else
        print_warning "No changes detected in API specification"
        if [ "$FORCE" != "true" ]; then
            read -p "Do you want to regenerate anyway? (y/N): " response
            if [[ ! "$response" =~ ^[Yy]$ ]]; then
                print_info "Skipping regeneration"
                exit 0
            fi
        fi
    fi
else
    print_warning "No baseline specification found. This will be the first generation."
fi

# Check if NSwag is installed
print_info "Checking for NSwag CLI..."
if ! command -v nswag &> /dev/null; then
    print_info "Installing NSwag CLI..."
    dotnet tool install --global NSwag.ConsoleCore --version 14.1.0
    if [ $? -ne 0 ]; then
        print_error "Failed to install NSwag CLI"
        exit 1
    fi
else
    print_success "NSwag CLI is available"
fi

# Update the baseline spec
print_info "Updating baseline specification..."
cp openapi/latest-venice-ai-api.yaml openapi/venice-ai-api.yaml
print_success "Baseline specification updated"

# Generate the C# client
print_info "Generating C# client using NSwag..."

# Create backup of existing generated client
if [ -f "$OUTPUT_PATH" ]; then
    cp "$OUTPUT_PATH" "$OUTPUT_PATH.bak"
    print_info "Backed up existing generated client"
fi

# Create temporary nswag config using Python, jq, or sed
if command -v python &> /dev/null; then
    python -c "
import json
import sys
try:
    with open('nswag.json', 'r') as f:
        config = json.load(f)
    config['documentGenerator']['fromDocument']['url'] = 'openapi/latest-venice-ai-api.yaml'
    with open('nswag-temp.json', 'w') as f:
        json.dump(config, f, indent=2)
except Exception as e:
    print(f'Error: {e}', file=sys.stderr)
    sys.exit(1)
"
elif command -v jq &> /dev/null; then
    jq '.documentGenerator.fromDocument.url = "openapi/latest-venice-ai-api.yaml"' nswag.json > nswag-temp.json
else
    # Fallback using sed
    sed 's|"url": "openapi/venice-ai-api.yaml"|"url": "openapi/latest-venice-ai-api.yaml"|g' nswag.json > nswag-temp.json
fi

# Run NSwag
if nswag run nswag-temp.json; then
    print_success "C# client generated successfully"
else
    print_error "Failed to generate C# client"
    # Restore backup
    if [ -f "$OUTPUT_PATH.bak" ]; then
        mv "$OUTPUT_PATH.bak" "$OUTPUT_PATH"
    fi
    rm -f nswag-temp.json
    exit 1
fi

# Clean up temporary file
rm -f nswag-temp.json

# Update version
CURRENT_DATE=$(date +%Y%m%d)
NEW_VERSION="1.0.$CURRENT_DATE"
print_info "Updating version to $NEW_VERSION..."

# Update version in csproj files
find . -name "*.csproj" -exec sed -i.bak "s|<Version>.*</Version>|<Version>$NEW_VERSION</Version>|g" {} \;
find . -name "*.csproj.bak" -delete

# Get API spec version
API_VERSION="Unknown"
if [ -f "openapi/venice-ai-api.yaml" ]; then
    API_VERSION=$(grep -E "^\s*version:\s*" openapi/venice-ai-api.yaml | head -1 | sed 's/.*version:\s*//' | tr -d '"' | tr -d "'" | xargs)
fi

# Update VERSION.md
cat > VERSION.md << EOF
# Version History

## $NEW_VERSION
- Updated to match Venice AI API changes
- Generated from API spec version: $API_VERSION
- Generated on: $(date)

Previous versions maintained in git history.
EOF

print_success "Version updated to $NEW_VERSION"

# Build and test
if [ "$SKIP_TESTS" != "true" ]; then
    print_info "Building solution..."
    if ! dotnet restore; then
        print_error "Restore failed"
        exit 1
    fi

    if ! dotnet build --configuration Release; then
        print_error "Build failed"
        exit 1
    fi
    print_success "Solution built successfully"

    print_info "Running unit tests..."
    if ! dotnet test tests/ --configuration Release --filter "Category!=Integration" --no-build --verbosity normal; then
        print_warning "Some unit tests failed. Please review the changes."
    else
        print_success "Unit tests passed"
    fi
fi

print_success "SDK regeneration completed!"
echo ""
print_info "Next steps:"
echo "1. Review the generated code in $OUTPUT_PATH"
echo "2. Check api-diff-report.txt for details on what changed (if it exists)"
echo "3. Update any custom wrapper code if needed"
echo "4. Run integration tests with valid API keys"
echo "5. Update documentation if new features were added"
echo "6. Commit and push your changes"

# Clean up backup files
rm -f "$OUTPUT_PATH.bak"
rm -f api-diff-report.txt

print_success "Done!"
