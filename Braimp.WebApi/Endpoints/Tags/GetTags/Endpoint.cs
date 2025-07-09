using Braimp.Application.Features.Tags.Queries.GetTagList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Tags.GetTags;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Tags.Get, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<TagListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Tags);
    }

    private async Task<IResult> Handler(IMediator mediator,  CancellationToken cancellationToken)
    {
        var command = new GetTagListQuery();
        var tags = await mediator.Send(command, cancellationToken);

        return Results.Ok(tags);
    }
}
