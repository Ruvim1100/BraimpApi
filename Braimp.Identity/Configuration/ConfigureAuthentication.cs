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
            configuration.GetSection("AzureAdApi"),
            jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme,
            subscribeToJwtBearerMiddlewareDiagnosticsEvents: false );

        services.AddAuthentication()
            .AddJwtBearer("AuthExtension", options =>
            {
                options.Authority =
                    $"{configuration["AzureAdApi:Instance"]}{configuration["AzureAdAuthExt:TenantId"]}/v2.0";
                options.Audience = configuration["AzureAdAuthExt:Audience"];
                options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
            });

        services.Configure<JwtBearerOptions>(
            JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
                var audience = configuration["AzureAdApi:Audience"];
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("User", p => p.RequireRole("User"));
            options.AddPolicy("Admin", p => p.RequireRole("Admin", "User"));

            options.AddPolicy("AuthExtension", p =>
            {
                p.RequireAuthenticatedUser();
                p.AuthenticationSchemes.Add("AuthExtension");
            });
        });
        return services;
    }
}
