using Braimp.Application.Features.Courses.Commands.DeleteCourse;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.DeleteCourse;
public class DeleteCourseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Courses.Delete, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand
        {
            Id = id,
            OwnerId = UserFakeClaimsConstants.OwnerId
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
