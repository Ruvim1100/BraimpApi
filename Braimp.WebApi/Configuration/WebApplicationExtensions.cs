using Braimp.Infrastructure;

namespace Braimp.WebApi.Extensions;
public static class WebApplicationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BraimpDbContext>();

        try
        {
            await DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
            throw;
        }
    }
}
