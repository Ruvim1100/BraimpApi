using Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Lessons.GetLessonById;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Lessons.GetById, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<LessonDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromRoute] Guid courseId,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetLessonDetailsQuery { Id = id, ModuleId = moduleId, CourseId = courseId };
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);  
    }
}
