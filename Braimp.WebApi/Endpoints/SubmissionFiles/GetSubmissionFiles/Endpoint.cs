using Braimp.Application.Features.SubmissionFiles.Queries.GetSubmissionFileList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.SubmissionFiles.GetSubmissionFiles;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.SubmissionFiles.Get, Handler)
            .Produces<SubmissionFileListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.SubmissionFiles);
    }

    private async Task<IResult> Handler([FromRoute] Guid submissionId, [FromRoute] Guid assignmentId,
    IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetSubmissionFileListQuery
        {
            AssignmentId = assignmentId,
            SubmissionId = submissionId
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
