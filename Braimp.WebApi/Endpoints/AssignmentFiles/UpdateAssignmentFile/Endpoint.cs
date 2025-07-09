using Braimp.Application.Features.AssignmentFiles.Commands.UpdateAssignmentFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.AssignmentFiles.UpdateAssignmentFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.AssignmentFiles.Update, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<Guid>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.AssignmentFiles);
    }

    private async Task<IResult> Handler([FromRoute] Guid assignmentId, [FromRoute] Guid id,
       [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new UpdateAssignmentFileCommand
        {
            Id = id,
            Name = request.Name,
            AssignmentId = assignmentId
        };

        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}
