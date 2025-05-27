using Braimp.Application.Features.AssignmentFiles.Commands.DeleteAssignmentFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.AssignmentFiles.DeleteAssignmentFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.AssignmentFiles.Delete, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.AssignmentFiles);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid assignmentId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteAssignmentFileCommand 
        { 
            Id = id, 
            AssignmentId = assignmentId
        };

        await mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}
