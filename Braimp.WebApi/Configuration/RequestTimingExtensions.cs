using Braimp.WebApi.Middleware;

namespace Braimp.WebApi.Configuration;
public static class RequestTimingExtensions
{
    public static IApplicationBuilder UseTimig(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTimingMiddleware>();
    }
}
