using Braimp.Application.Features.Submissions.Commands.UpdateSubmission;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Submissions.UpdateSubmission;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Submissions.Update, Handler).
            Produces<Guid>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Submissions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId, 
        [FromRoute] Guid id, [FromBody] Request request,  IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new UpdateSubmissionCommand
        {
            Id = id,
            AssignmentId = assignmentId,
            CourseId = courseId,
            Text = request.Text
        };

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
