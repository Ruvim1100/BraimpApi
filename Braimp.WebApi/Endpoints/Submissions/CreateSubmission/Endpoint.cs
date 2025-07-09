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
            .RequireAuthorization(Roles.User)
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(StatusCodes.Status201Created)
            .DisableAntiforgery()
            .WithTags(EndpointTags.Submissions)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId, 
        [FromForm] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        await using var stream = request.File.OpenReadStream();

        var command = new CreateSubmissionCommand
        {
            CourseId = courseId,
            AssignmentId = assignmentId,
            DisplayName = request.DisplayName,
            OriginalFileName = request.File.FileName,
            Text = request.Text,
            FileStream = stream
        };
        var result = await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}
