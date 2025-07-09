using Braimp.Application.Features.LessonBlocks.Commands.DeleteLessonBlock;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.LessonBlocks.DeleteLessonBlock;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.LessonBlocks.Delete, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.LessonBlocks);
    }

    private async Task<IResult> Handler([FromRoute] Guid lessonId, [FromRoute] Guid id, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteLessonBlockCommand
        {
            LessonId = lessonId,
            Id = id
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
