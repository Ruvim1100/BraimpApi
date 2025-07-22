using Braimp.Application.Features.QuizAttempts.Commands.SubmitQuizAnswers;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.QuizAttempts.SubmitQuizAttempt;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.QuizAttempts.Submit, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizAttempts);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid quizId,
    [FromRoute] Guid attemptId, [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new SubmitQuizAnswersCommand
        {
            QuizAttemptId = attemptId,
            Answers = request.Answers
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
