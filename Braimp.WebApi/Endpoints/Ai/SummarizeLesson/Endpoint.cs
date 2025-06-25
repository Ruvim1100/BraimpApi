using Braimp.Application.Features.AI.SummarizeLesson;
using Braimp.Application.Modules;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Ai.SummarizeLesson
{
    public class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(ApiRoutes.Ai.Summarize, Handler)
                .RequireAuthorization("User")
                .Produces<AiMessage>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .WithName("SummarizeLessonEndpoint")
                .WithTags(EndpointTags.Ai);
        }

        private async Task<IResult> Handler(SummarizeLessonRequest summarizeLessonDto, IMediator mediator, CancellationToken cancellationToken)
        {
            var command = new SummarizeLessonCommand(summarizeLessonDto.content);
            var result = await mediator.Send(command, cancellationToken);
            return Results.Ok(result);

        }
    }
}
