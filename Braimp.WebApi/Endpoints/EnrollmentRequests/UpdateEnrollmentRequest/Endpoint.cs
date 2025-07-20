using Braimp.Application.Features.EnrollmentRequests.Commands.UpdateEnrollmentRequest;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.EnrollmentRequests.UpdateEnrollmentRequest;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.EnrollmentRequest.Update, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.EnrollmentRequests);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, 
        [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new UpdateEnrollmentRequestCommand
        {
            CourseId = courseId,
            Id = id,
            Status = request.Status,
            UserId = request.UserId
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
