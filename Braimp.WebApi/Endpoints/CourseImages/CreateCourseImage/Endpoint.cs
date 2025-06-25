using Braimp.Application.Features.CourseImages.Commands.CreateCourseImage;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Braimp.WebApi.Endpoints.CourseImages.CreateCourseImage;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.CourseImages.Create, Handler)
            .RequireAuthorization("User")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Accepts<IFormFile>("multipart/form-data")
            .DisableAntiforgery()
            .WithTags(EndpointTags.CourseImages)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromForm] IFormFile file, 
        [FromForm] string displayName, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new CreateCourseImageCommand
        {
            CourseId = courseId,
            DisplayName = displayName,
            OriginalFileName = file.FileName,
            FileStream = file.OpenReadStream(),
            Encoding = Encoding.UTF8
        };

        var result = await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
