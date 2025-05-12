using AutoMapper;
using Braimp.Application.Features.Modules.Commands.UpdateModule;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.UpdateModule;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Modules.Update, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Modules);
    }

    private async Task<IResult> Handler([FromBody] Request updateModuleDto, IMediator mediator, 
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateModuleCommand>(updateModuleDto);
        await mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}