using Braimp.Application.Features.Assignments.Commands.DeleteAssignment;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Assignments.DeleteAssignment;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Assignments.Delete, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Assignments);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteAssignmentCommand
        {
            Id = id,
            CourseId = courseId
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
