using Braimp.Application.Features.Modules.Queries.GetModuleDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.GetModuleById;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Modules.GetById, Handler)
            .Produces<ModuleDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Modules);
    }

    private async Task<IResult> Handler([FromRoute] Guid id, [FromRoute] Guid courseId, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetModuleDetailsQuery { Id = id, CourseId = courseId};

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}
