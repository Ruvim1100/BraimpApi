using Braimp.Application.Features.CourseImages.Commands.DeleteCourseImage;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.CourseImages.DeleteCourseImage;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.CourseImages.Delete, Handler)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.CourseImages)
            .RequireAuthorization("User");
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteCourseImageCommand
        {
            CourseId = courseId,
            Id = id
        };

        await mediator.Send(command);
        return Results.Ok();
    }
}
