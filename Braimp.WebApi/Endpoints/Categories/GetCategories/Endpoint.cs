using Braimp.Application.Features.Categories.Queries.GetCategoryList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Categories.GetCategories;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Categories.Get, Handler)
            .RequireAuthorization("User")
            .Produces<CategoryListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Categories);
    }

    private async Task<IResult> Handler(IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoryListQuery(), cancellationToken);
        return Results.Ok(result);
    }
}
