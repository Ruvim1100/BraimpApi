using Braimp.Application.Features.EnrollmentRequests.Commands.CreateEnrollmentRequest;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.EnrollmentRequests.CreateEnrollmentRequest;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.EnrollmentRequest.Create, Hanlder)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.EnrollmentRequests);
    }

    private async Task<IResult> Hanlder([FromRoute] Guid courseId, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new CreateEnrollmentRequestCommand
        {
            CourseId = courseId
        };

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
