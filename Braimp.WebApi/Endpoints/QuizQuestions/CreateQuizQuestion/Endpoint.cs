using Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.QuizQuestions.CreateQuizQuestion;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.QuizQuestions.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizQuestions);
    }

    private async Task<IResult> Handler([FromRoute] Guid quizId, [FromRoute] Guid courseId,
        [FromBody] Request request, IMediator mediator, 
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizQuestionCommand
        {
            Text = request.Text,
            QuestionType = request.QuestionType,
            Weight = request.Weight,
            QuizId = quizId,
            CourseId = courseId,
            QuizOptions = request.QuizOptions?.Select(o => new QuizOptionModel
            {
                Text = o.Text,
                IsCorrect = o.IsCorrect
            }).ToList()
        };

        var questionId = await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
