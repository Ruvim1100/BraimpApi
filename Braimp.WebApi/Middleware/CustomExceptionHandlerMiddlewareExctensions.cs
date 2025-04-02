namespace Braimp.WebApi.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExctensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
