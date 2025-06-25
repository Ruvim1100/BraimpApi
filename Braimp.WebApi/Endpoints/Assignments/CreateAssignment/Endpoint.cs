using AutoMapper;
using Braimp.Application.Features.Assignments.Commands.CreateAssignment;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Assignments.CreateAssignment;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Assignments.Create, Handler)
            .RequireAuthorization("User")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Assignments);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromBody] Request request, 
        IMediator mediator, IMapper mapper,  CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateAssignmentCommand>(request);
        command.CourseId = courseId;

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
