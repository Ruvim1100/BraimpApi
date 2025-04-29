using AutoMapper;
using Braimp.Application.Features.Categories.Commands.UpdateCategory;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Categories.UpdateCategory;
public class UpdateCategoryEmdpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Categories.Update, Handler)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler([FromBody] UpdateCategoryDto updateCategoryDto, IMediator mediator, 
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateCategoryCommand>(updateCategoryDto);
        await mediator.Send(command);
        return Results.NoContent();
    }
}
