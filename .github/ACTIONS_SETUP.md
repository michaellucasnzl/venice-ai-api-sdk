# GitHub Actions Setup for Venice AI .NET SDK

This repository includes automated GitHub Actions workflows to build, test, and publish the Venice AI .NET SDK to NuGet.org whenever code is pushed to the main branch.

## 🚀 Quick Start

1. **Add NuGet API Key** (Required for publishing):
   - Get your API key from [nuget.org](https://nuget.org) → Account Settings → API Keys
   - In GitHub: Repository Settings → Secrets and variables → Actions
   - Add new secret: `NUGET_API_KEY` = your API key

2. **Push to main branch** and watch the magic happen! ✨

## 📋 Workflows Overview

### `nuget-publish.yml` - Main CI/CD Workflow
- **Triggers**: Push to `main`, Pull Requests, Manual dispatch
- **Features**:
  - ✅ Builds and tests the entire solution
  - ✅ Smart version detection from project file
  - ✅ Checks if version already exists on NuGet (avoids duplicates)
  - ✅ Publishes to NuGet.org only if version is new
  - ✅ Creates GitHub releases with changelogs
  - ✅ Uploads package artifacts
  - ✅ Test result reporting

### `api-change-detection.yml` - Automated API Monitoring
- **Triggers**: Daily at 2 AM UTC, Manual dispatch
- **Features**:
  - ✅ Monitors Venice AI API for changes
  - ✅ Downloads latest API specification
  - ✅ Compares with baseline and detects changes
  - ✅ Regenerates SDK when API changes
  - ✅ Creates pull requests with updated code
  - ✅ Provides detailed change reports

## ⚙️ Required Setup

### 1. NuGet API Key (Essential)

**Step 1: Get your NuGet API Key**
1. Sign in to [nuget.org](https://nuget.org)
2. Go to your account → "API Keys"
3. Click "Create"
4. Set these options:
   - **Key Name**: `GitHub Actions - Venice AI SDK`
   - **Select Scopes**: `Push new packages and package versions`
   - **Select Packages**: `All packages` (or select specific packages)
   - **Glob Pattern**: `VeniceAI.*` (optional, for security)
5. Copy the generated API key

**Step 2: Add to GitHub Secrets**
1. In your GitHub repository, go to: **Settings** → **Secrets and variables** → **Actions**
2. Click **"New repository secret"**
3. Name: `NUGET_API_KEY`
4. Value: Paste your NuGet API key
5. Click **"Add secret"**

### 2. Repository Permissions (Optional)

For enhanced automation features:
1. Go to **Settings** → **Actions** → **General**
2. Under "Workflow permissions":
   - Select **"Read and write permissions"**
   - Check **"Allow GitHub Actions to create and approve pull requests"**

This enables the API change detection workflow to automatically create pull requests when the Venice AI API is updated.

## 🔧 How It Works

### Version Management
The workflow reads the version from your project file:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <Version>1.0.0</Version>
    <!-- other properties -->
  </PropertyGroup>
</Project>
```

### Publishing Process
1. **Code Push** → Triggers workflow
2. **Build & Test** → Ensures code quality
3. **Version Check** → Verifies version doesn't exist on NuGet
4. **Package Creation** → Creates optimized NuGet package
5. **Publish** → Uploads to NuGet.org
6. **Release** → Creates GitHub release with changelog

### API Change Detection Process
1. **Daily Check** → Monitors Venice AI API for changes
2. **Download Latest** → Fetches current API specification
3. **Compare** → Detects differences from baseline
4. **Regenerate SDK** → Updates client code when changes found
5. **Create PR** → Submits pull request for review
6. **Manual Review** → You review and merge changes

### Pull Request Testing
- All PRs automatically build and test
- No publishing occurs (only on main branch)
- See test results directly in PR

### Smart Features
- 🔍 **Duplicate Prevention**: Checks if version exists before publishing
- 📊 **Test Reporting**: Visual test results in GitHub
- 📦 **Artifact Storage**: Packages stored for 30 days
- 🏷️ **Auto-Tagging**: Creates git tags for releases
- 📝 **Release Notes**: Auto-generated release descriptions
- 🤖 **API Monitoring**: Automatically detects API changes and updates SDK
- 🔄 **Auto-Regeneration**: Keeps SDK in sync with Venice AI API changes

## 📖 Usage Examples

### 1. Regular Development (Recommended)
```bash
# 1. Make your changes
git add .
git commit -m "Add new feature"

# 2. Update version in src/VeniceAI.SDK/VeniceAI.SDK.csproj
# Change: <Version>1.0.0</Version>
# To:     <Version>1.0.1</Version>

# 3. Push to main
git push origin main

# 4. Watch GitHub Actions build and publish automatically! 🎉
```

### 2. Manual Workflow Trigger
1. Go to your repository on GitHub
2. Click **Actions** tab
3. Select **"Build and Publish to NuGet"**
4. Click **"Run workflow"** button
5. Choose the branch and click **"Run workflow"**

### 3. API Change Detection (Automated)
The repository automatically monitors the Venice AI API for changes:
```bash
# This happens automatically every day at 2 AM UTC
# No manual action needed!

# When API changes are detected:
# 1. SDK is regenerated automatically
# 2. Pull request is created for review
# 3. You review and merge the changes
# 4. Publishing workflow runs on merge
```

### 4. Pull Request Testing
- All PRs automatically build and test
- No publishing occurs (only on main branch)
- See test results directly in PR

## 🔍 Monitoring & Troubleshooting

### View Build Status
- **Actions Tab**: See all workflow runs
- **Badges**: Add status badges to README
- **Notifications**: GitHub will email on failures

### Common Issues & Solutions

| Issue | Solution |
|-------|----------|
| ❌ "403 Forbidden" on NuGet push | Check `NUGET_API_KEY` secret is set correctly |
| ❌ "Package already exists" | Update version in project file |
| ❌ Build fails | Check .NET version, dependencies, test failures |
| ❌ No publish on main | Ensure changes are in `src/` or `tests/` folders |
| ❌ API detection not working | Check if Venice AI API endpoints are accessible |
| ❌ PR creation fails | Verify repository write permissions are enabled |

### Debug a Failed Workflow
1. Go to **Actions** tab
2. Click on the failed workflow run
3. Click on the failed job
4. Expand the failing step to see detailed logs
5. Look for red error messages

## 🛡️ Security Features

- ✅ API keys stored as encrypted secrets
- ✅ Only publishes from protected `main` branch
- ✅ Requires successful tests before publishing
- ✅ Uses official, pinned GitHub Actions
- ✅ No secrets exposed in logs
- ✅ Automated API monitoring with manual review process

## 📋 Manual Commands

If you need to publish manually:

```bash
# Build the project
dotnet build src/VeniceAI.SDK/VeniceAI.SDK.csproj --configuration Release

# Create package
dotnet pack src/VeniceAI.SDK/VeniceAI.SDK.csproj --configuration Release --output ./nupkg

# Publish to NuGet (replace YOUR_API_KEY)
dotnet nuget push ./nupkg/*.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

## 🎯 Best Practices

1. **Version Updates**: Always update version before merging to main
2. **Semantic Versioning**: Use MAJOR.MINOR.PATCH format
3. **Test First**: Ensure all tests pass locally before pushing
4. **Changelog**: Update CHANGELOG.md with your changes
5. **Small Commits**: Make focused commits for easier tracking
6. **API Changes**: Review auto-generated PRs from API monitoring carefully
7. **Manual Testing**: Test API changes locally before merging auto-generated PRs

## 📞 Support

If you encounter issues:
1. Check the troubleshooting section above
2. Review GitHub Actions logs
3. Verify your NuGet API key permissions
4. Open an issue in the repository
