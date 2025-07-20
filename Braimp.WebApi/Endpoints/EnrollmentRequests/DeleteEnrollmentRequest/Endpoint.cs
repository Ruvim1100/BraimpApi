using Braimp.Application.Features.EnrollmentRequests.Commands.DeleteEnrollmentRequest;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.EnrollmentRequests.DeleteEnrollmentRequest;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.EnrollmentRequest.Delete, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.EnrollmentRequests);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid courseId, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteEnrollmentRequestCommand
        {
            CourseId = courseId,
            Id = id
        };

        await mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}
