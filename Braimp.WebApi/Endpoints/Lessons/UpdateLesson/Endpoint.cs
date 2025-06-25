using AutoMapper;
using Braimp.Application.Features.Lessons.Commands.UpdateLesson;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Lessons.UpdateLesson;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Lessons.Update, Handler)
            .RequireAuthorization("User")
            .Produces<Guid>(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons); ;
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromRoute] Guid courseId, 
        Request request, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var lessonId = await mediator.Send(mapper.Map<UpdateLessonCommand>(request));
        return Results.NoContent();
    }
}
