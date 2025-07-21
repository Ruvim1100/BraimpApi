using Braimp.Application.Features.AI.SummarizeLesson;
using Braimp.Application.Modules;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Ai.SummarizeLesson
{
    public class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(ApiRoutes.Lessons.Summarize, Handler)
                .RequireAuthorization(Roles.User)
                .Produces<AiMessage>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .WithTags(EndpointTags.Lessons);
        }

        private async Task<IResult> Handler([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken)
        {
            var command = new SummarizeLessonCommand{LessonId = id };
            var result = await mediator.Send(command, cancellationToken);
            return Results.Ok(result);

        }
    }
}
