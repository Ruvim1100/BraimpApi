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
            .RequireAuthorization(Roles.User)
            .Produces<Guid>(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons); ;
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromRoute] Guid courseId,
        [FromBody] Request request, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = new UpdateLessonCommand
        {
            Id = id,
            ModuleId = moduleId,
            CourseId = courseId,
            Title = request.Title,
            Description = request.Description,
            IsPublished = request.IsPublished
        };
        var lessonId = await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
