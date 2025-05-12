using AutoMapper;
using Braimp.Application.Features.Lessons.Commands.CreateLesson;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Lessons.CreateLesson;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Lessons.Create, Handler)
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid moduleId, 
        [FromBody] Request request, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateLessonCommand>(request);
        command.CourseId = courseId;
        command.ModuleId = moduleId;

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
