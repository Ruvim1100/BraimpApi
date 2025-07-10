namespace Braimp.WebApi.Configuration;
public static class ConfigureCors
{
    public static IServiceCollection AddBraimpCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins("https://braimp-frontend-had8cgbjb7fxctev.westeurope-01.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            //options.AddPolicy("AllowAll", policy =>
            //{
            //    policy
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials();
            //});
        });

        return services;
    }
}
