using Braimp.Application.Features.Assignments.Queries.GetAssignmentList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Assignments.GetAssignments;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Assignments.Get, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Assignments);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetAssignmentListQuery { CourseId = courseId };
        var assignments = await mediator.Send(query);

        return Results.Ok(assignments);
    }
}
