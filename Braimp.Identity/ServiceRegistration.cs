using Braimp.Application.Abstraction;
using Braimp.Identity.Configuration;
using Braimp.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Braimp.Identity;
public static class ServiceRegistration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICourseAuthorizationService, CourseAuthorizationService>(); 
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddAuthentication(configuration);
        return services;
    }
}
