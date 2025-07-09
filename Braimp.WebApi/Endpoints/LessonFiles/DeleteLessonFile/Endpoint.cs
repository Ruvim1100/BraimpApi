using Braimp.Application.Features.LessonFiles.Commands.DeleteLessonFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.LessonFiles.DeleteLessonFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.LessonFiles.Delete, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuthorization(Roles.User)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.LessonFiles);
    }

    private async Task<IResult> Handler([FromRoute] Guid lessonId, [FromRoute] Guid id, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteLessonFileCommand
        {
            LessonId = lessonId,
            Id = id
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
