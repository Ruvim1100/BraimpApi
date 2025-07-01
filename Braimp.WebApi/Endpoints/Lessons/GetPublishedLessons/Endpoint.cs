using Braimp.Application.Features.Lessons.Queries.GetPublishedLessonList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Lessons.GetPublishedLessons;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Lessons.GetPublished, Handler)
            .RequireAuthorization("User")
            .Produces<PublishedLessonListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid ModuleId,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetPublishedLessonListQuery
        {
            CourseId = courseId,
            ModuleId = ModuleId
        };

        var lessons = await mediator.Send(query);
        return Results.Ok(lessons);
    }
}
