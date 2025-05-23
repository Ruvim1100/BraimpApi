using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace Braimp.Identity.Configuration;
public static class ConfigureAuthentication
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(
            configuration.GetSection("AzureAd"),
            jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme,
            subscribeToJwtBearerMiddlewareDiagnosticsEvents: false );

        services.Configure<JwtBearerOptions>(
            JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("User", p => p.RequireRole("User", "Admin"));
            options.AddPolicy("Admin", p => p.RequireRole("Admin"));
        });
        return services;
    }
}
