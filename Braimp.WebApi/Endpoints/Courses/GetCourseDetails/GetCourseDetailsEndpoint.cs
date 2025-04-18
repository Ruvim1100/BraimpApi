using AutoMapper;
using Braimp.Application.Features.Courses.Queries.GetCourseDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.GetCourseDetails;
public class GetCourseDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetById, Handler)
            .Produces<CourseDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetCourseDetailQuery()
        {
            Id = id,
            OwnerId = UserFakeClaimsConstants.OwnerId
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
