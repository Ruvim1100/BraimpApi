using Azure.Storage.Blobs;
using Braimp.Application.Abstraction;
using Braimp.Infrastructure.BlobStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Braimp.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new ArgumentNullException();

        services.AddDbContext<BraimpDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped<IBraimpDbContext>(provider => provider.GetService<BraimpDbContext>()!);
        services.AddScoped<IUnitOfWork>(provider => provider.GetService<BraimpDbContext>()!);
        
        services.AddSingleton<BlobServiceClient>(_ => new 
            BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]));
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}
