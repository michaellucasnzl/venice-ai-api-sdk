using System.Reflection;

namespace VeniceAI.SDK;

/// <summary>
/// Provides version and build information for the Venice AI SDK.
/// </summary>
public static class VersionInfo
{
    /// <summary>
    /// Gets the current version of the Venice AI SDK.
    /// </summary>
    public static string Version => GetAssemblyVersion();
    
    /// <summary>
    /// Gets the current file version of the Venice AI SDK.
    /// </summary>
    public static string FileVersion => GetAssemblyFileVersion();
    
    /// <summary>
    /// Gets the informational version of the Venice AI SDK.
    /// </summary>
    public static string InformationalVersion => GetAssemblyInformationalVersion();
    
    /// <summary>
    /// Gets the build number of the Venice AI SDK.
    /// </summary>
    public static string BuildNumber => GetBuildNumber();
    
    /// <summary>
    /// Gets the major version number.
    /// </summary>
    public static int MajorVersion => GetVersionComponent(0);
    
    /// <summary>
    /// Gets the minor version number.
    /// </summary>
    public static int MinorVersion => GetVersionComponent(1);
    
    /// <summary>
    /// Gets the patch version number.
    /// </summary>
    public static int PatchVersion => GetVersionComponent(2);
    
    private static string GetAssemblyVersion()
    {
        var assembly = Assembly.GetAssembly(typeof(VersionInfo));
        return assembly?.GetName().Version?.ToString() ?? "Unknown";
    }
    
    private static string GetAssemblyFileVersion()
    {
        var assembly = Assembly.GetAssembly(typeof(VersionInfo));
        var fileVersionAttribute = assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>();
        return fileVersionAttribute?.Version ?? "Unknown";
    }
    
    private static string GetAssemblyInformationalVersion()
    {
        var assembly = Assembly.GetAssembly(typeof(VersionInfo));
        var informationalVersionAttribute = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return informationalVersionAttribute?.InformationalVersion ?? GetAssemblyVersion();
    }
    
    private static string GetBuildNumber()
    {
        var version = GetAssemblyFileVersion();
        var parts = version.Split('.');
        return parts.Length >= 4 ? parts[3] : "0";
    }
    
    private static int GetVersionComponent(int index)
    {
        var version = GetAssemblyVersion();
        var parts = version.Split('.');
        if (parts.Length > index && int.TryParse(parts[index], out var component))
        {
            return component;
        }
        return 0;
    }
}
