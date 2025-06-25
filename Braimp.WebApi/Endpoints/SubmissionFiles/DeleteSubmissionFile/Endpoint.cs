using Braimp.Application.Features.SubmissionFiles.Commands.DeleteSubmissionFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.SubmissionFiles.DeleteSubmissionFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.SubmissionFiles.Delete, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.SubmissionFiles);
    }
    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid submissionId,
    IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteSubmissionFileCommand
        {
            Id = id,
            SubmissionId = submissionId
        };

        await mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}
