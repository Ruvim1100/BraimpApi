namespace Braimp.WebApi.Configuration;
public static class ConfigureCors
{
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
