using Braimp.Application.Features.Courses.Queries.GetCourseList;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.GetCourses;
public class GetCoursesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.Get, Handler)
            .Produces<CourseListVm>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler(IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCourseListQuery(), cancellationToken);
        return Results.Ok(result);
    }
}

