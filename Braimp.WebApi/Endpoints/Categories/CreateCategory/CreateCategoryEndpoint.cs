using AutoMapper;
using Braimp.Application.Features.Categories.Commands.CreateCategory;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Categories.CreateCategory;
public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Categories.Create, Handler)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler([FromBody] CreateCategoryDto createCategoryDto, 
        IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateCategoryCommand>(createCategoryDto);
        await mediator.Send(command, cancellationToken);

        return Results.Created();

    }
}
