using Microsoft.EntityFrameworkCore;

namespace Braimp.Infrastructure;
public class DbInitializer
{
    public static async Task Initialize(BraimpDbContext context)
    {
        //context.Database.EnsureDeleted();
        await context.Database.MigrateAsync();
        //await BraimpDataSeeder.SeedAsync(context);
    }
}
