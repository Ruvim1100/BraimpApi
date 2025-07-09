using Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.GetEnrolledCourses;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetEnrolled, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<EnrolledCourseListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler([FromQuery] int page, [FromQuery] int pageSize, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetEnrolledCourseListQuery
        {
            Page = page,
            PageSize = pageSize
        };
        var courses = await mediator.Send(query, cancellationToken);
        return Results.Ok(courses);
    }
}