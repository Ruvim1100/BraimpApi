using Braimp.Application.Abstraction;
using Braimp.Domain.Abstraction;
using Braimp.Infrastructure.Ai.Services;
using Braimp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Braimp.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"] ??
            throw new ArgumentNullException();

        services.AddDbContext<BraimpDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped<IBraimpDbContext>(provider => provider.GetService<BraimpDbContext>()!);
        services.AddScoped<IUnitOfWork>(provider => provider.GetService<BraimpDbContext>()!);

        services.AddSingleton<IAiService, AzureOpenAiService>();

        services.AddScoped<ICourseAuthorizationService, CourseAuthorizationService>();

        return services;
    }
}
