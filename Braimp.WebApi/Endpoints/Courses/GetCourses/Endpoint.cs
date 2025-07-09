using Braimp.Application.Features.Courses.Queries.GetCourseList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.GetCourses;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<CourseListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    public async Task<IResult> Handler([AsParameters] GetCourseListQuery query, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}

