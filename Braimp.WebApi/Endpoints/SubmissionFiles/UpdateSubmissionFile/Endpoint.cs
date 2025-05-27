using Braimp.Application.Features.SubmissionFiles.Commands.UpdateSubmissionFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.SubmissionFiles.UpdateSubmissionFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.SubmissionFiles.Update, Handler)
            .Produces<Guid>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.SubmissionFiles);
    }

    private async Task<IResult> Handler([FromRoute] Guid submissionId, [FromRoute] Guid id,
        [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new UpdateSubmissionFileCommand
        {
            Id = id,
            Name = request.Name,
            SubmissionId = submissionId
        };

        var result = await mediator.Send(command);

        return Results.Ok(result);
    }
}
