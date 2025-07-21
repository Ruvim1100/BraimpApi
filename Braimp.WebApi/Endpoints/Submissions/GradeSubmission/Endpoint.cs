using Braimp.Application.Features.Submissions.Commands.GradeSubmission;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Submissions.GradeSubmission;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Submissions.Grade, Handler).
            RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Submissions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId,
        [FromRoute] Guid id, [FromBody] Request request, IMediator mediator, ILogger<Endpoint> logger, CancellationToken cancellationToken)
    {
        var command = new GradeSubmissionCommand
        {
            CourseId = courseId,
            AssignmentId = assignmentId,
            Id = id,
            Grade = request.Grade,
            ReviewComment = request.ReviewComment
        };


        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
