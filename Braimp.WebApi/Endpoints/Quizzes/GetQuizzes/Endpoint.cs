using Braimp.Application.Features.Quizzes.Queries.GetQuizzes;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Quizzes.GetQuizzes;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Quizzes.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<QuizListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Quizzes);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, IMediator mediator, 
        CancellationToken cancellationToken)
    {
        var query = new GetQuizListQuery{ CourseId = courseId };
        var result = await mediator.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}
