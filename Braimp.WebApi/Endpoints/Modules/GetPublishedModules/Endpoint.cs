using Braimp.Application.Features.Modules.Queries.GetPublishedModuleList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.GetPublishedModules;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Modules.GetPublished, Handler)
            .Produces<PublishedModuleListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Modules)
            .RequireAuthorization("User");
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, IMediator mediator, 
        CancellationToken cancellationToken)
    {
        var query = new GetPublishedModuleListQuery { CourseId = courseId };
        var modules = await mediator.Send(query);
        return Results.Ok(modules);
    }
}
