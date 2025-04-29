using Braimp.Application.Common.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Braimp.WebApi.Middleware;
public class CustomExceptionHandlerMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {

            await HandleExceptionAsync(context, exception);
        }
    }

    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch(exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                result = JsonSerializer.Serialize(new { error = exception.Message });
                break;
            case ForbiddenAccessException:
                code = HttpStatusCode.Forbidden;
                result = JsonSerializer.Serialize(new { error = exception.Message});
                break;
            default:
                result = JsonSerializer.Serialize(new { error = exception.Message });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
