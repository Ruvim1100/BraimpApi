using Braimp.Application.Features.Courses.Queries.GetStudentList;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Courses.GetStudents;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetStudents, Handler)
            .RequireAuthorization(Roles.User)
            .Produces<StudentListResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetStudentListQuery { CourseId = courseId };

        var students = await mediator.Send(query);
        return Results.Ok(students);
    }
}
