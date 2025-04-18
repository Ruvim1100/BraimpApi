using AutoMapper;
using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourse
{
    public class UpdateCourseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(ApiRoutes.Courses.Update, Handler)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesValidationProblem();
        }

        private async Task<IResult> Handler([FromBody] UpdateCourseDto updateCourseDto, IMediator mediator, 
            IMapper mapper, CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateCourseCommand>(updateCourseDto);
            command.OwnerId = UserFakeClaimsConstants.OwnerId;
            await mediator.Send(command, cancellationToken);
            return Results.NoContent();
        }
    }
}
