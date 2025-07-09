using Braimp.Application.Features.Courses.Commands.UpdateCourseThumbnail;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourseThumbnail;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Courses.UpdateThumbnailImage, Handler)
            .RequireAuthorization(Roles.User)
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .DisableAntiforgery()
            .WithTags(EndpointTags.Courses)
            .WithOpenApi(); ;
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromForm] IFormFile file,
        [FromForm] string displayName, IMediator mediator, CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var command = new UpdateCourseThumbnailCommand
        {
            Id = id,
            DisplayName = displayName,
            OriginalFileName = file.FileName,
            FileStream = stream,
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
