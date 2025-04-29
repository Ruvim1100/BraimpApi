using AutoMapper;
using Braimp.Application.Features.Modules.Commands.CreateModule;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.CreateModule;
public class CreateModuleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Modules.Create, Handler)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler([FromBody] CreateModuleDto createModuleDto, IMediator mediator, 
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateModuleCommand>(createModuleDto);
        await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}
