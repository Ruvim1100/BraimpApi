using Braimp.Application.Features.SubmissionFiles.Commands.CreateSubmissionFile;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Braimp.WebApi.Endpoints.SubmissionFiles.CreateSubmissionFile;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.SubmissionFiles.Create, Handler)
            .RequireAuthorization("User")
            .Accepts<IFormFile>("multipart/form-data")
            .Produces<Guid>(StatusCodes.Status200OK)
            .DisableAntiforgery()
            .WithTags(EndpointTags.SubmissionFiles)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid submissionId, [FromForm] IFormFile file, 
        [FromForm] string displayName, IMediator mediator, CancellationToken cancellationToken)
    {

        var command = new CreateSubmissionFileCommand
        {
            SubmissionId = submissionId,
            DisplayName = displayName,
            OriginalFileName = file.FileName,
            FileStream = file.OpenReadStream(),
            Encoding = Encoding.UTF8
        };

        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
