using Microsoft.EntityFrameworkCore;

namespace Braimp.Infrastructure;
public class DbInitializer
{
    public static async Task Initialize(BraimpDbContext context)
    {
        Console.WriteLine("Running EF Core migrations...");
        await context.Database.MigrateAsync();
        Console.WriteLine("Migrations applied successfully.");
    }
}
