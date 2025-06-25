//using System.Diagnostics;

namespace Braimp.WebApi.Middleware;
public class RequestTimingMiddleware
{
    private readonly ILogger<RequestTimingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //var stopwatch = Stopwatch.StartNew();
        //stopwatch.Start();
        var start = DateTime.UtcNow;
        await _next.Invoke(context);
 //       stopwatch.Stop();
        _logger.LogInformation($"Timing: {context.Request.Path}: {(DateTime.UtcNow - start).TotalMilliseconds}"); //readable
    }
}
