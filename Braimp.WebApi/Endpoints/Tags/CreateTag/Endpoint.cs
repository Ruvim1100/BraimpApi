using Braimp.Application.Features.Tags.Commands.CreateTag;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Tags.CreateTag;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Tags.Create, Handler)
            .RequireAuthorization(Roles.Admin)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Tags);
    }

    private async Task<IResult> Handler([FromBody] Request request, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new CreateTagCommand
        {
            Name = request.Name
        };

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
