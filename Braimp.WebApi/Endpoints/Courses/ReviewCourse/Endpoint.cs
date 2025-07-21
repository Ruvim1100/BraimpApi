using Braimp.Application.Features.Courses.Commands.ReviewCourse;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.ReviewCourse;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Courses.Review, Handler)
            .RequireAuthorization(Roles.Admin)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler([FromBody] Request request,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new ReviewCourseCommand
        {
            CourseId = request.Id,
            Status = request.Status
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
