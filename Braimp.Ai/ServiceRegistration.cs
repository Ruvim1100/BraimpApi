using Microsoft.Extensions.DependencyInjection;
using Braimp.Application.Abstraction;
using Braimp.Infrastructure.Ai.Services;
using Microsoft.Extensions.Configuration;
using Braimp.Ai.Option;

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
