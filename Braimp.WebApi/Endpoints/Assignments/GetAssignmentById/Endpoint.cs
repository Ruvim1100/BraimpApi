using Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Assignments.GetAssignmentById;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Assignments.GetById, Handler)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Assignments);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, 
        IMediator mediator , CancellationToken cancellationToken)
    {
        var query = new GetAssignmentDetailsQuery
        {
            Id = id,
            CourseId = courseId,
        };

        var assignment = await mediator.Send(query);
        return Results.Ok(assignment);
    }
}
