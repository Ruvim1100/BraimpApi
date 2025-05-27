using AutoMapper;
using Braimp.Application.Features.Submissions.Commands.CreateSubmission;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Submissions.CreateSubmission;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Submissions.Create, Handler)
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Submissions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId, [FromBody] Request request,
        IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = new CreateSubmissionCommand
        {
            CourseId = courseId,
            AssignmentId = assignmentId,
            Text = request.Text
        };
        var result = await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}
