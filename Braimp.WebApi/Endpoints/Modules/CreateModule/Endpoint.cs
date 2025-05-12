using AutoMapper;
using Braimp.Application.Features.Modules.Commands.CreateModule;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Modules.CreateModule;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Modules.Create, Handler).WithName("CreateModule")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Modules);
    }

    private async Task<IResult> Handler([FromRoute]Guid courseId, [FromBody] Request request, 
        IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateModuleCommand>(request);
        command.CourseId = courseId;
        await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}
