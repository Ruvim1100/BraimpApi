using Braimp.Ai.Option;
using Braimp.Ai.Services;
using Braimp.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Braimp.Ai;
public static class ServiceRegistration
{
    public static IServiceCollection AddAi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AiOptions>(configuration.GetSection(nameof(AiOptions)));
        services.AddSingleton<IAiService, AzureOpenAiService>();
        return services;
    }
}
