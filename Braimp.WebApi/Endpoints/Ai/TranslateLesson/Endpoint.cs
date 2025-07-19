using Braimp.Application.Features.AI.TranslateLesson;
using Braimp.Application.Modules;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Ai.TranslateLesson;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Lessons.Translate, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<AiMessage>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Lessons);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid moduleId, [FromRoute] Guid lessonId,
        [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new TranslateLessonCommand
        {
            Language = request.Language,
            SourceText = request.LessonText
        };

        var translatedText = await mediator.Send(command, cancellationToken);
        return Results.Ok(translatedText);
    }
}
