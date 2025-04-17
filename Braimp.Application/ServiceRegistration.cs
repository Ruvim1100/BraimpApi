using Braimp.Application.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Braimp.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
