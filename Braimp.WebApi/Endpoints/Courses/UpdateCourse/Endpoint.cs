using AutoMapper;
using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourse;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.Courses.Update, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler([FromBody] Request updateCourseDto, IMediator mediator, 
        IMapper mapper, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateCourseCommand>(updateCourseDto);
        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}
