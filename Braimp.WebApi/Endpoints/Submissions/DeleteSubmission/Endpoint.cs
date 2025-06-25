using Braimp.Application.Features.Submissions.Commands.DeleteSubmission;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Submissions.DeleteSubmission;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Submissions.Delete, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Submissions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId, 
        [FromRoute] Guid id, IMediator mediator)
    {
        var command = new DeleteSubmissionCommand
        {
            Id = id,
            AssignmentId = assignmentId,
            CourseId = courseId,
        };

        await mediator.Send(command);
        return Results.NoContent();
    }
}
