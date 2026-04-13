using Microsoft.Extensions.DependencyInjection;

namespace FeaturePlanner.Web.Services;

/// <summary>
/// Extension methods for registering Azure OpenAI services in the dependency injection container.
/// </summary>
public static class AzureOpenAiServiceExtensions
{
    /// <summary>
    /// Registers the Azure OpenAI service in the dependency injection container.
    /// Configuration should be provided via appsettings.json under "AzureOpenAi" section.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for further configuration</returns>
    public static IServiceCollection AddAzureOpenAiService(this IServiceCollection services)
    {
        services.AddScoped<IAzureOpenAiService, AzureOpenAiService>();
        return services;
    }
}
