using AutoMapper;
using Braimp.Application.Features.Categories.Commands.CreateCategory;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Categories.CreateCategory;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Categories.Create, Handler)
            .RequireAuthorization(Roles.Admin)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Categories);
    }

    private async Task<IResult> Handler([FromBody] Request createCategoryDto, 
        IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateCategoryCommand>(createCategoryDto);
        await mediator.Send(command, cancellationToken);

        return Results.Created();

    }
}
