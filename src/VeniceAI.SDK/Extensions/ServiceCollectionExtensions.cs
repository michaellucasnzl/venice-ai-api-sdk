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
    /// <summary>
    /// Adds Venice AI services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure Venice AI options.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddVeniceAI(this IServiceCollection services, Action<VeniceAIOptions> configureOptions)
    {
        services.Configure<VeniceAIOptions>(options =>
        {
            configureOptions(options);
            ValidateOptions(options);
        });

        return AddVeniceAIServices(services);
    }

    /// <summary>
    /// Adds Venice AI services to the service collection with configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration section.</param>
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

    private static IServiceCollection AddVeniceAIServices(IServiceCollection services)
    {
        // Register HTTP client for making direct API calls
        services.AddHttpClient("VeniceAI", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
        });

        // Register services with simplified HTTP client approach
        services.AddTransient<IChatService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("VeniceAI");
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();
            return new ChatService(httpClient, options.ApiKey, logger);
        });

        services.AddTransient<IAudioService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("VeniceAI");
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();
            return new AudioService(httpClient, options.ApiKey, logger);
        });

        services.AddTransient<IImageService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("VeniceAI");
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();
            return new ImageService(httpClient, options.ApiKey, logger);
        });

        services.AddTransient<IEmbeddingService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("VeniceAI");
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();
            return new EmbeddingService(httpClient, options.ApiKey, logger);
        });

        services.AddTransient<IModelService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("VeniceAI");
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();
            return new ModelService(httpClient, options.ApiKey, logger);
        });

        services.AddTransient<IBillingService>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("VeniceAI");
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            var logger = serviceProvider.GetRequiredService<ILogger<BaseHttpService>>();
            return new BillingService(httpClient, options.ApiKey, logger);
        });

        services.AddTransient<IVeniceAIClient, VeniceAIClient>();

        return services;
    }

    private static void ConfigureHttpClient(HttpClient client, VeniceAIOptions options)
    {
        client.BaseAddress = new Uri(options.BaseUrl);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiKey}");
        client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

        foreach (var header in options.CustomHeaders)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
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
