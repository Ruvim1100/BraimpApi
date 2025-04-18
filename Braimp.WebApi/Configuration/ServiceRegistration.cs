using Braimp.Application.Abstraction;
using Braimp.Application.Common.Mapping;
using System.Reflection;

namespace Braimp.WebApi.Services;
public static class ServiceRegistration
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

    public static IServiceCollection AddBraimpCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin()));
        return services;
    }
}
