using Braimp.Application.Features.LessonBlocks.Commands.UpdateLessonBlock;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.LessonBlocks.UpdateLessonBlock;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.LessonBlocks.Update, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.LessonBlocks);
    }

    private async Task<IResult> Handler([FromRoute] Guid lessonId, [FromRoute] Guid id, [FromBody] Request request,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new UpdateLessonBlockCommand
        {
            LessonId = lessonId,
            Id = id,
            Content = request.Content
        };

        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    }
}
