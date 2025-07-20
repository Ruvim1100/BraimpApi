using Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.EnrollmentRequests.GetEnrollmentRequests;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.EnrollmentRequest.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<EnrollmentRequestListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.EnrollmentRequests);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetEnrollmentRequestListQuery
        {
            CourseId = courseId
        };

        var enrollmentRequests = await mediator.Send(query, cancellationToken);

        return Results.Ok(enrollmentRequests);
    }
}
