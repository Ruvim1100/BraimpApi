using Braimp.Application.Features.Modules.Commands.DeleteModule;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.DeleteModule;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Modules.Delete, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Modules);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid courseId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteModuleCommand()
        {
            Id = id,
            CourseId = courseId
        };

        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
