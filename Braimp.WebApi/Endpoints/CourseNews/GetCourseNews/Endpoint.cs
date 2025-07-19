using Braimp.Application.Features.News.Queries.GetCourseNewsList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.CourseNews.GetCourseNews;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.CourseNews.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<CourseNewsListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.CourseNews);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetCourseNewsListQuery
        {
            CourseId = courseId
        };

        var news = await mediator.Send(query, cancellationToken);
        return Results.Ok(news);
    }
}
