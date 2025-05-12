using Microsoft.OpenApi.Models;

namespace Braimp.WebApi.Configuration;
public static class ConfigureSwagger
{
    public static IServiceCollection AddBraimpSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/authorize", configuration["AzureAd:TenantId"])),
                        TokenUrl = new Uri(string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/token", configuration["AzureAd:TenantId"])),
                        Scopes = new Dictionary<string, string>
                        {
                            {
                                configuration["AzureAd:Scopes"]!, "Access API" 
                            }
                        }
                    }
                }
            });

            c.CustomSchemaIds(type => type.FullName!);

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
                    new[] { configuration["AzureAd:Scopes"]! }
                }
            });

        });

        return services;
    }
}