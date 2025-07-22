using Braimp.Application.Features.QuizAttempts.Commands.CreateQuizAttempt;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.QuizAttempts.CreateAttempt;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.QuizAttempts.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<QuizAttemptCreatedModel>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizAttempts);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid quizId, IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizAttemptCommand
        {
            CourseId = courseId,
            QuizId = quizId
        };

        var quizAttemptId = await mediator.Send(command);
        return Results.Ok(quizAttemptId);
    }
}
