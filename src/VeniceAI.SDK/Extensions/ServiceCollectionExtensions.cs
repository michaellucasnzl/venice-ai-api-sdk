using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using VeniceAI.SDK.Configuration;
using VeniceAI.SDK.Services;
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
        return AddVeniceAI(services, options => options.ApiKey = apiKey);
    }

    private static IServiceCollection AddVeniceAIServices(IServiceCollection services)
    {
        // Register HTTP clients with proper named client configuration
        services.AddHttpClient<IChatService, ChatService>("ChatService", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
        });

        services.AddHttpClient<IImageService, ImageService>("ImageService", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
        });

        services.AddHttpClient<IEmbeddingService, EmbeddingService>("EmbeddingService", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
        });

        services.AddHttpClient<IAudioService, AudioService>("AudioService", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
        });

        services.AddHttpClient<IModelService, ModelService>("ModelService", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
        });

        services.AddHttpClient<IBillingService, BillingService>("BillingService", (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<VeniceAIOptions>>().Value;
            ConfigureHttpClient(client, options);
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
