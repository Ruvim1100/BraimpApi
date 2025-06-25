using Braimp.Application.Features.Lessons.Commands.DeleteLesson;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Lessons.DeleteLesson;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Lessons.Delete, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromRoute] Guid courseId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteLessonCommand {Id = id, ModuleId = moduleId, CourseId = courseId };
        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
