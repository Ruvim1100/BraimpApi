using Braimp.Application.Features.Modules.Commands.DeleteModule;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Modules.DeleteModule;
public class DeleteModuleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Modules.Delete, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteModuleCommand()
        {
            Id = id
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
