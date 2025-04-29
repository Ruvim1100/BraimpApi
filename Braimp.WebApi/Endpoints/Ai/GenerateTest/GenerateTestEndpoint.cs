using Braimp.Application.Common.Dtos;
using Braimp.Application.Features.AI.GenerateTest;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Ai.GenerateTest;
public class GenerateTestEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Ai.Generate, Handler)
            .RequireAuthorization("Admin")
            .Produces<GenerateTestResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }

    public async Task<IResult> Handler([FromBody] GenerateTestDto generateTestDto, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new GenerateTestCommand(generateTestDto.content);
        var res = await mediator.Send(command, cancellationToken);
        return Results.Ok(res);
    }
}
