using Braimp.Application.Features.SystemStats.Queries;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.SystemStats;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.SystemStats.Get, Handler)
            .RequireAuthorization(Roles.Admin)
            .Produces<SystemStatsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.SystemStats);
    }

    private async Task<IResult> Handler(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetSystemStatsQuery();
        var stats = await mediator.Send(query, cancellationToken);

        return Results.Ok(stats);
    }
}
