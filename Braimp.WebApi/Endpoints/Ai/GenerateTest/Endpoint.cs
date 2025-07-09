using Braimp.Application.Features.AI.GenerateTest;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Ai.GenerateTest;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Quizzes.Generate, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<Guid>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Quizzes);
    }

    public async Task<IResult> Handler([FromRoute] Guid courseId, [FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new GenerateTestCommand
        {
            CourseId = courseId,
            Title = request.Title,
            QuestionCount = request.QuestionCount,
            Language = request.Language,
            SourceText = request.SourceText,
        };

        var id = await mediator.Send(command, cancellationToken);
        return Results.Ok(new { id });
    }
}
