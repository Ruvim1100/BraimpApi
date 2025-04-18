using Braimp.Application.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Braimp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"] ??
                throw new ArgumentNullException();

            services.AddDbContext<BraimpDbContext>(options => { options.UseSqlServer(connectionString); });

            services.AddScoped<IBraimpDbContext>(provider => provider.GetService<BraimpDbContext>()!);

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<BraimpDbContext>()!);

            return services;
        }
    }
}
