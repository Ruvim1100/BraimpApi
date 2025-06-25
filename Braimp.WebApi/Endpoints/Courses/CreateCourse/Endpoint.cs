using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Features.Courses.Commands.CreateCourse;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.CreateCourse;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Courses.Create, Handler)
            .RequireAuthorization("User")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    public async Task<IResult> Handler([FromBody] Request createCourseDto, 
        IMediator mediator, IMapper mapper, ICurrentUserService currentUser, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateCourseCommand>(createCourseDto);
        await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}
