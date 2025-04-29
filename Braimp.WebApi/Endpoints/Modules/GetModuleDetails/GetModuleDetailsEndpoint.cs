using Braimp.Application.Features.Modules.Queries.GetModuleDetails;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Modules.GetModuleDetails;
public class GetModuleDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Modules.GetById, Handler)
            .Produces<ModuleDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetModuleDetailsQuery { Id = id };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
