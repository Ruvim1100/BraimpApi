using Braimp.Application.Features.Modules.Queries.GetModuleList;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Modules.GetModule;
public class GetModuleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Modules.Get, Handler)
            .Produces<ModuleListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler([AsParameters] GetModuleListQuery query, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}
