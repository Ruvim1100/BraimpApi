using Braimp.Application.Features.LessonBlocks.Queries.GetLessonBlockList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.LessonBlocks.GetLessonBlocks;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.LessonBlocks.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<LessonBlockListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.LessonBlocks);
    }

    private async Task<IResult> Handler([FromRoute] Guid lessonId, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetLessonBlockListQuery { LessonId = lessonId };
        var lessonBlocks = await mediator.Send(query, cancellationToken);

        return Results.Ok(lessonBlocks);
    }
}
