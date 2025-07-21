using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Services;
using VeniceAI.SDK.Services.Base;
using VeniceAI.SDK.Services.Interfaces;

namespace VeniceAI.SDK.Extensions;

/// <summary>
/// Extension methods for dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string VeniceAIHttpClientName = "VeniceAI";
    /// <summary>
    /// Adds Venice AI services to the service collection with configuration.
    /// Only API key is configurable - all other settings are managed by the SDK.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration section containing the API key.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddVeniceAI(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        services.Configure<VeniceAIOptions>(configuration.GetSection(VeniceAIOptions.SectionName));
        services.AddSingleton<IValidateOptions<VeniceAIOptions>, VeniceAIOptionsValidator>();

        return AddVeniceAIServices(services);
    }

    /// <summary>
    /// Adds Venice AI services to the service collection with API key.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="apiKey">The API key.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddVeniceAI(this IServiceCollection services, string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));

        services.Configure<VeniceAIOptions>(options =>
        {
            options.ApiKey = apiKey;
            ValidateOptions(options);
        });

        return AddVeniceAIServices(services);
    }

    /// <summary>
    /// Adds Venice AI services to the service collection with API key and custom HttpClient configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="configureHttpClient">Action to configure the HttpClient for additional settings like timeout and headers.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddVeniceAI(this IServiceCollection services, string apiKey, Action<HttpClient> configureHttpClient)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));

        services.Configure<VeniceAIOptions>(options =>
        {
            options.ApiKey = apiKey;
            ValidateOptions(options);
        });

        return AddVeniceAIServices(services, configureHttpClient);
    }

    /// <summary>
    /// Adds Venice AI services with a pre-configured HttpClient instance.
    /// This allows users to provide their own HttpClient with custom configurations.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="httpClient">Pre-configured HttpClient instance.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddVeniceAI(this IServiceCollection services, string apiKey, HttpClient httpClient)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));

        ArgumentNullException.ThrowIfNull(httpClient);

        services.Configure<VeniceAIOptions>(options =>
        {
            options.ApiKey = apiKey;
            ValidateOptions(options);
        });

        // Register the provided HttpClient as a singleton
        services.AddSingleton(httpClient);

        return AddVeniceAIServicesWithProvidedHttpClient(services);
    }

    private static IServiceCollection AddVeniceAIServices(IServiceCollection services)
    {
        return AddVeniceAIServices(services, null);
    }

    private static IServiceCollection AddVeniceAIServices(IServiceCollection services, Action<HttpClient>? configureHttpClient)
    {
        // Register HTTP client for making direct API calls - use configurable name
        services.AddHttpClient<IVeniceAIClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);

            // Apply custom configuration if provided
            configureHttpClient?.Invoke(client);
        });

        // Also register with default name for backward compatibility
        services.AddHttpClient(VeniceAIHttpClientName, (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);

            // Apply custom configuration if provided
            configureHttpClient?.Invoke(client);
        });

        return RegisterVeniceAIServices(services, false);
    }

    private static IServiceCollection AddVeniceAIServicesWithProvidedHttpClient(IServiceCollection services)
    {
        return RegisterVeniceAIServices(services, true);
    }

    private static IServiceCollection RegisterVeniceAIServices(IServiceCollection services, bool useProvidedHttpClient)
    {
        // Register all services
        services.AddTransient<IChatService>(serviceProvider =>
            CreateService<ChatService>(serviceProvider, useProvidedHttpClient));

        services.AddTransient<IAudioService>(serviceProvider =>
            CreateService<AudioService>(serviceProvider, useProvidedHttpClient));

        services.AddTransient<IImageService>(serviceProvider =>
            CreateService<ImageService>(serviceProvider, useProvidedHttpClient));

        services.AddTransient<IEmbeddingService>(serviceProvider =>
            CreateService<EmbeddingService>(serviceProvider, useProvidedHttpClient));

        services.AddTransient<IModelService>(serviceProvider =>
            CreateService<ModelService>(serviceProvider, useProvidedHttpClient));

        services.AddTransient<IBillingService>(serviceProvider =>
            CreateService<BillingService>(serviceProvider, useProvidedHttpClient));

        services.AddTransient<IVeniceAIClient, VeniceAIClient>();

        return services;
    }

    private static T CreateService<T>(IServiceProvider serviceProvider, bool useProvidedHttpClient)
        where T : class
    {
        var httpClient = GetHttpClient(serviceProvider, useProvidedHttpClient);
        var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
        var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();

        return (T)Activator.CreateInstance(typeof(T), httpClient, options.ApiKey, logger)!;
    }

    private static HttpClient GetHttpClient(IServiceProvider serviceProvider, bool useProvidedHttpClient)
    {
        if (useProvidedHttpClient)
        {
            return serviceProvider.GetRequiredService<HttpClient>();
        }
        
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        return httpClientFactory.CreateClient(VeniceAIHttpClientName);
    }    private static void ConfigureHttpClient(HttpClient client, VeniceAIOptions options)
    {
        client.BaseAddress = new Uri(VeniceAIOptions.BaseUrl);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiKey}");
        client.Timeout = TimeSpan.FromSeconds(VeniceAIOptions.DefaultTimeoutSeconds);
    }

    private static void ValidateOptions(VeniceAIOptions options)
    {
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(options, context, results, true))
        {
            var errors = string.Join(", ", results.Select(r => r.ErrorMessage));
            throw new ArgumentException($"Invalid Venice AI options: {errors}");
        }
    }
}

/// <summary>
/// Validator for Venice AI options.
/// </summary>
public class VeniceAIOptionsValidator : IValidateOptions<VeniceAIOptions>
{
    /// <summary>
    /// Validates the options.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="options">The options.</param>
    /// <returns>The validation result.</returns>
    public ValidateOptionsResult Validate(string? name, VeniceAIOptions options)
    {
        var context = new ValidationContext(options);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(options, context, results, true))
        {
            var errors = results.Select(r => r.ErrorMessage!).ToArray();
            return ValidateOptionsResult.Fail(errors);
        }

        return ValidateOptionsResult.Success;
    }
}
