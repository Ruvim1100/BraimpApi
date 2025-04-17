using AutoMapper;
using Braimp.Application.Features.Courses.Commands.CreateCourse;
using Braimp.WebApi.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.CreateCourse
{
    public class CreateCourseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/courses", Handler)
                .Produces(StatusCodes.Status201Created)
                .ProducesValidationProblem();
        }

        private static async Task<IResult> Handler([FromBody] CreateCourseDto createCourseDto, 
            IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateCourseCommand>(createCourseDto);
            await mediator.Send(command, cancellationToken);

            return Results.Created();
        }
    }
}
