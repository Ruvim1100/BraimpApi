using Braimp.Application.Features.Courses.Queries.GetOwnedCourseList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.GetOwnedCourses;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetOwned, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<OwnedCourseListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler([FromQuery] int page, [FromQuery] int pageSize, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetOwnedCourseListQuery
        {
            Page = page,
            PageSize = pageSize
        };
        var courses = await mediator.Send(query, cancellationToken);
        return Results.Ok(courses);
    }
}
