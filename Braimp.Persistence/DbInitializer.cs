
namespace Braimp.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(BraimpDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
