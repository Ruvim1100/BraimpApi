using Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.AssignmentFiles.GetAssignmentFiles;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.AssignmentFiles.Get, Handler)
            .RequireAuthorization("User")
            .Produces<AssignmentFileListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.AssignmentFiles);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetAssignmentFileListQuery
        { 
            CourseId = courseId,
            AssignmentId = assignmentId 
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
