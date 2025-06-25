using Braimp.Application.Features.Categories.Commands.DeleteCategory;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Categories.DeleteCategory;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Categories.Delete, Handler)
            .RequireAuthorization("Admin")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Categories);
    }

    private async Task<IResult> Handler(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);

        await mediator.Send(command,cancellationToken);
        return Results.NoContent();
    }
}
