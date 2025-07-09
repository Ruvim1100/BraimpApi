using Braimp.Application.Features.QuizQuestions.Commands.DeleteQuizQuestion;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.QuizQuestions.DeleteQuestion;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.QuizQuestions.Delete, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizQuestions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid quizId, [FromRoute] Guid id,
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteQuizQuestionCommand
        {
            CourseId = courseId,
            QuizId = quizId,
            Id = id,
        };
        await mediator.Send(command);
        return Results.NoContent();
    }
}
