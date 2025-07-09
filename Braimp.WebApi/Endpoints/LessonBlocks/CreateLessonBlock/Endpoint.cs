using Braimp.Application.Features.LessonBlocks.Commands.CreateLessonBlock;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.LessonBlocks.CreateLessonBlock;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.LessonBlocks.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.LessonBlocks);
    }

    private async Task<IResult> Handler([FromRoute] Guid lessonId, [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new CreateLessonBlockCommand
        {
            LessonId = lessonId,
            Type = request.Type,
            Content = request.Content,
        };

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
