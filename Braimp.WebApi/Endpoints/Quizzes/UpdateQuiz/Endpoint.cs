using AutoMapper;
using Braimp.Application.Features.Quizzes.Commands.UpdateQuiz;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Quizzes.UpdateQuiz;
public class Endpoint : ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Quizzes.Update, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Quizzes);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, [FromBody] Request request,
        IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateQuizCommand>(request);
        command.CourseId = courseId;
        command.Id = id;

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
