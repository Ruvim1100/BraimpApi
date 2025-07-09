using Braimp.Application.Features.AssignmentFiles.Commands.CreateAssignmentFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.AssignmentFiles.CreateAssignmentFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.AssignmentFiles.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Accepts<IFormFile>("multipart/form-data")
            .Produces<Guid>(StatusCodes.Status200OK)
            .DisableAntiforgery()
            .WithTags(EndpointTags.AssignmentFiles)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid assignmentId,
        [FromForm] IFormFile file, [FromForm] string displayName, IMediator mediator, CancellationToken cancellationToken)
    {

        var command = new CreateAssignmentFileCommand
        {
            CourseId = courseId,
            AssignmentId = assignmentId,
            DisplayName = displayName,
            OriginalFileName = file.FileName,
            FileStream = file.OpenReadStream(),
        };

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
