using AutoMapper;
using Braimp.Application.Features.Quizzes.Commands.CreateQuiz;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Quizzes.CreateQuiz;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Quizzes.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Quizzes);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromBody] Request request, 
        IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateQuizCommand>(request);
        command.CourseId = courseId;

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
