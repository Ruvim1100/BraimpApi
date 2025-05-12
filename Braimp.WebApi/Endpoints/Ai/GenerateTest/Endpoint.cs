using Braimp.Application.Features.AI.GenerateTest;
using Braimp.Application.Modules;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Ai.GenerateTest;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Ai.Generate, Handler)
            .RequireAuthorization("Admin")
            .Produces<AiMessage>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Ai)
            .WithName(EndpointNames.GenerateTest);
    }

    public async Task<IResult> Handler([FromBody] GenerateTestRequest generateTestDto, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new GenerateTestCommand(generateTestDto.content);
        var res = await mediator.Send(command, cancellationToken);
        return Results.Ok(res);
    }
}
