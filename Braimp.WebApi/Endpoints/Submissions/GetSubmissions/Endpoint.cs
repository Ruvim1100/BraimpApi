using Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Submissions.GetSubmissions;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Submissions.Get, Handler)
            .Produces<SubmissionListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Submissions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetSubmissionListQuery
        {
            AssignmentId = assignmentId,
            CourseId = courseId
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
