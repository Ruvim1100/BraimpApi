using AutoMapper;
using Braimp.Application.Features.Categories.Commands.UpdateCategory;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Categories.UpdateCategory;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Categories.Update, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Categories);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto, IMediator mediator,
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(id, updateCategoryDto.Name);
        await mediator.Send(command);
        return Results.NoContent();
    }
}
