using Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.GetEnrolledCourses;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetEnrolled, Handler)
            .RequireAuthorization("User")
            .Produces<EnrolledCourseListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetEnrolledCourseListQuery();
        var courses = await mediator.Send(query);
        return Results.Ok(courses);
    }
}
