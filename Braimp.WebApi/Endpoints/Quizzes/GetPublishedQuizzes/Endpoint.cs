using Braimp.Application.Features.Quizzes.Queries.GetPublishedQuizzes;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Quizzes.GetPublishedQuizzes;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Quizzes.GetPublished, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<PublishedQuizListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Quizzes);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId,  IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetPublishedQuizListQuery {CourseId = courseId };

        var quizzes = await mediator.Send(query, cancellationToken);
        return Results.Ok(quizzes);
    }
}
