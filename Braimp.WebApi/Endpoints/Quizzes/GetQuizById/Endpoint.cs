using Braimp.Application.Features.Quizzes.Queries.GetQuizDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Quizzes.GetQuizById;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Quizzes.GetById, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<QuizDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Quizzes);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetQuizDetailsQuery
        {
            Id = id,
            CourseId = courseId
        };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
