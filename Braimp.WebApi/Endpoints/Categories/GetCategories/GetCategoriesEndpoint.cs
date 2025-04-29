using AutoMapper;
using Braimp.Application.Features.Categories.Queries.GetCategoryList;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Categories.GetCategories;

public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Categories.Get, Handler)
            .RequireAuthorization("User")
            .Produces<CategoryListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }

    private async Task<IResult> Handler(IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoryListQuery(), cancellationToken);
        return Results.Ok(result);
    }
}
