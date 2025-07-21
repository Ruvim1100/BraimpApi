using Braimp.Application.Features.Courses.Queries.GetPendingCoursesList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.GetPendingCourses;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetPending, Handler)
            .RequireAuthorization(Roles.Admin)
            .Produces<PendingCourseListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetPendingCourseListQuery();
        var courses = await mediator.Send(query, cancellationToken);
        return Results.Ok(courses);
    }
}
