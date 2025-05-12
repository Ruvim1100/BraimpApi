using Braimp.Application.Abstraction;
using Braimp.Application.Mapping;
using System.Reflection;

namespace Braimp.WebApi.Configuration;
public static class ConfigureMapping
{
    public static IServiceCollection AddBraimpMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            cfg.AddProfile(new AssemblyMappingProfile(typeof(IBraimpDbContext).Assembly));
        });
        return services;
    }
}
