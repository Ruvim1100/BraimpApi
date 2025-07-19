using Braimp.Application.Features.News.Commands.UpdateNews;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.CourseNews.UpdateCourseNews;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.CourseNews.Update, Handler)
            .RequireAuthorization(Roles.User)
            .Accepts<Request>("multipart/form-data")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.CourseNews)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id,
        [FromForm] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        Stream? fileStream = null;
        if (request.File is not null)
        {
            fileStream = request.File.OpenReadStream();
        }

        var command = new UpdateNewsCommand
        {
            Id = id,
            CourseId = courseId,
            Title = request.Title,
            Content = request.Content,
            FileDisplayName = request.FileDisplayName,
            OriginalFileName = request.File?.FileName,
            FileStream = fileStream
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
