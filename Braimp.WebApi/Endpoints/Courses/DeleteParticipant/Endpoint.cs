using Braimp.Application.Features.Courses.Commands.DeleteParticipant;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.DeleteParticipant;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Courses.DeleteParticipant, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid userId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteParticipantCommand
        {
            UserId = userId,
            CourseId = courseId
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
