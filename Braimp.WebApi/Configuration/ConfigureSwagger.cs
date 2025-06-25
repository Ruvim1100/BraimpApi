using Microsoft.OpenApi.Models;

namespace Braimp.WebApi.Configuration;
public static class ConfigureSwagger
{
    public static IServiceCollection AddBraimpSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Braimp API", Version = "v1" });

            var tenantId = configuration["AzureAd:TenantId"];
            var policy = configuration["AzureAd:UserFlow"];
            var clientId = configuration["AzureAd:ClientId"];
            var scope = configuration["AzureAd:Scopes"];

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"https://braimpplatform.ciamlogin.com/{tenantId}/oauth2/v2.0/authorize?p={policy}"),
                        TokenUrl = new Uri($"https://braimpplatform.ciamlogin.com/{tenantId}/oauth2/v2.0/token?p={policy}"),
                        Scopes = new Dictionary<string, string>
                        {
                            { scope!, "Access Braimp API" }
                        }
                    }
                }
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { scope }
                }
            });

            c.CustomSchemaIds(type => type.FullName!);
        });

        return services;
    }
}