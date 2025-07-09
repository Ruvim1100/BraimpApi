using Braimp.Application.Features.LessonFiles.Commands.CreateLessonFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.LessonFiles.CreateLessonFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.LessonFiles.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .DisableAntiforgery()
            .WithTags(EndpointTags.LessonFiles)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid lessonId, [FromForm] IFormFile file, 
        [FromForm] string displayName, IMediator mediator, CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var command = new CreateLessonFileCommand
        {
            LessonId = lessonId,
            DisplayName = displayName,
            OriginalFileName = file.FileName,
            FileStream = stream,
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
