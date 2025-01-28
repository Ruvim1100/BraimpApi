using Braimp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Braimp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<BraimpDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IBraimpDbContext>(provider =>
                provider.GetService<BraimpDbContext>());

            return services;
        }
    }
}
