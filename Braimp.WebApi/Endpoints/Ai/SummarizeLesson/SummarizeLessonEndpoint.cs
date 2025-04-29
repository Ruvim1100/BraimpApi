using Braimp.Application.Common.Dtos;
using Braimp.Application.Features.AI.SummarizeLesson;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Ai.SummarizeLesson
{
    public class SummarizeLessonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(ApiRoutes.Ai.Summarize, Handler)
                .Produces<SummarizeLessonResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem();
        }

        private async Task<IResult> Handler(SummarizeLessonDto summarizeLessonDto, IMediator mediator, CancellationToken cancellationToken)
        {
            var command = new SummarizeLessonCommand(summarizeLessonDto.content);
            var result = await mediator.Send(command, cancellationToken);
            return Results.Ok(result);

        }
    }
}
