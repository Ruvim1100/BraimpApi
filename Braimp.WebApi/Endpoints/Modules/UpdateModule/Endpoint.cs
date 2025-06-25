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
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Modules);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, [FromBody] Request request, 
        IMediator mediator,  IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateModuleCommand>(request);
        command.CourseId = courseId;
        command.Id = id;
        await mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}