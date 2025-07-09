using AutoMapper;
using Braimp.Application.Features.Assignments.Commands.UpdateAssignment;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Assignments.UpdateAssignment;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Assignments.Update, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Assignments);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, [FromBody] Request request, IMediator mediator, 
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateAssignmentCommand>(request);
        command.CourseId = courseId;
        command.Id = id;

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
