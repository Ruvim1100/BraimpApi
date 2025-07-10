namespace Braimp.WebApi.Configuration;
public static class ConfigureCors
{
    public static IServiceCollection AddBraimpCors(this IServiceCollection services, IConfiguration configuration)
    {

        var origin = configuration["Cors:AllowedOrigin"];

        if (string.IsNullOrWhiteSpace(origin))
        {
            throw new InvalidOperationException("CORS origin not configured.");
        }

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins(origin)
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
