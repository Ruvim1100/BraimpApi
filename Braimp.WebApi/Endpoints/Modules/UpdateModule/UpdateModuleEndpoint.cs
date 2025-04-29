using AutoMapper;
using Braimp.Application.Features.Modules.Commands.UpdateModule;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.UpdateModule;
public class UpdateModuleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Modules.Update, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler([FromBody] UpdateModuleDto updateModuleDto, IMediator mediator, 
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateModuleCommand>(updateModuleDto);
        await mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}