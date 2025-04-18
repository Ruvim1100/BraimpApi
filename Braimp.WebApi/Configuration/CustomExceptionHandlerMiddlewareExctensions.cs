using Braimp.WebApi.Middleware;

namespace Braimp.WebApi.Configuration;
public static class CustomExceptionHandlerMiddlewareExctensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
