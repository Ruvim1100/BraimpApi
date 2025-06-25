using Braimp.Application.Features.Submissions.Queries.GetSubmissionDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Submissions.GetSubmissionById;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Submissions.GetById, Handler)
            .RequireAuthorization("User")
            .Produces<SubmissionDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Submissions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId,
        [FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetSubmissionDetailsQuery
        {
            Id = id,
            AssignmentId = assignmentId,
            CourseId = courseId,
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
