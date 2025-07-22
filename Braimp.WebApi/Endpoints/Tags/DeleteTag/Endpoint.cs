using Braimp.Application.Features.Tags.Commands.DeleteTag;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Tags.DeleteTag;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Tags.Delete, Handler)
            .RequireAuthorization(Roles.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Tags);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteTagCommand
        {
            Id = id,
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
