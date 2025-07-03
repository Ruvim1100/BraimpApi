using Braimp.Application.Features.LessonFiles.Commands.CreateLessonFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Braimp.WebApi.Endpoints.LessonFiles.CreateLessonFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.LessonFiles.Create, Handler)
            .RequireAuthorization("User")
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .DisableAntiforgery()
            .WithTags(EndpointTags.LessonFiles)
            .WithOpenApi(); ;
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
            Encoding = Encoding.UTF8
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
