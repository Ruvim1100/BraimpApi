using Braimp.Application.Features.QuizAttempts.Queries.GetQuizAttemptList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.QuizAttempts.GetQuizAttempts;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.QuizAttempts.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<QuizAttemptListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizAttempts);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid quizId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetQuizAttemptListQuery
        {
            CourseId = courseId,
            QuizId = quizId
        };

        var attempts = await mediator.Send(query, cancellationToken);

        return Results.Ok(attempts);
    }
}
