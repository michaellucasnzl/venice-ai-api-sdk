# Venice AI SDK Version Configuration

This file documents the versioning strategy for the Venice AI SDK.

## Current Version
- **Major**: 1
- **Minor**: 2  
- **Patch**: 0
- **Build**: Auto-incremented from GitHub workflow run number

## Version Format
- **Release**: `{Major}.{Minor}.{Patch}.{Build}` (e.g., `1.2.0.12345`)
- **Debug**: `{Major}.{Minor}.{Patch}.{Build}-dev` (e.g., `1.2.0.12345-dev`)

## Versioning Rules

### Major Version (Breaking Changes)
Increment when making incompatible API changes:
- Removing public APIs
- Changing method signatures
- Changing behavior in backwards-incompatible ways
- Major architectural changes

### Minor Version (New Features)
Increment when adding functionality in a backwards-compatible manner:
- Adding new public APIs
- Adding new features
- Adding new optional parameters
- Performance improvements

### Patch Version (Bug Fixes)
Increment when making backwards-compatible bug fixes:
- Bug fixes
- Security patches
- Documentation updates
- Internal refactoring

### Build Number (Auto-increment)
- Automatically set to GitHub workflow run number
- Increments on every push to main branch
- Ensures unique version for every build

## How to Update Versions

### For Major/Minor/Patch Changes
Update the values in `Directory.Build.props`:
```xml
<VersionMajor>1</VersionMajor>
<VersionMinor>2</VersionMinor>
<VersionPatch>0</VersionPatch>
```

### For Build Number
The build number is automatically set by the CI/CD pipeline using the GitHub workflow run number.

## Version Sources
1. **Directory.Build.props** - Central version definition
2. **GitHub Actions** - Build number from `github.run_number`
3. **VersionInfo.cs** - Runtime version access

## Examples
- Development build: `1.2.0.12345-dev`
- Release build: `1.2.0.12345`
- NuGet package: `VeniceAI.SDK.1.2.0.12345.nupkg`
- Git tag: `v1.2.0.12345`
