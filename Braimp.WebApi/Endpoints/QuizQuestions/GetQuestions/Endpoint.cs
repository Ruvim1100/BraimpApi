using Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.QuizQuestions.GetQuestions;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.QuizQuestions.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<QuizQestionListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizQuestions);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid quizId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetQuizQuestionListQuery
        {
            CourseId = courseId,
            QuizId = quizId
        };

        var questions = await mediator.Send(query, cancellationToken);

        return Results.Ok(questions);
    }

}
