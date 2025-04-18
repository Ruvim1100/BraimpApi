using Microsoft.EntityFrameworkCore;

namespace Braimp.Infrastructure;
public class DbInitializer
{
    public static async Task Initialize(BraimpDbContext context)
    {
        await context.Database.MigrateAsync();
    }
}
