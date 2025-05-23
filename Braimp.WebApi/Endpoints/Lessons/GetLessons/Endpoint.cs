using Braimp.Application.Features.Lessons.Queries.GetLessonList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Lessons.GetLessons;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Lessons.Get, Handler)
           .WithName("GetLessonList")
           .WithSummary("Get list of lessons in a module")
           .Produces<LessonListResponse>(StatusCodes.Status200OK)
           .ProducesValidationProblem()
           .WithTags(EndpointTags.Lessons); ;
    }

    private async Task<IResult> Handler([AsParameters] Request request,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetLessonListQuery
        {
            CourseId = request.CourseId,
            ModuleId = request.ModuleId,
            SearchTerm = request.SearchTerm,
            IsPublished = request.IsPublished
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
