using Braimp.Application.Features.Courses.Queries.GetCourseDetails;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;

namespace Braimp.WebApi.Endpoints.Courses.GetCourseById;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Courses.GetById, Handler)
            .Produces<CourseDetailsResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.Courses);
    }

    private async Task<IResult> Handler(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetCourseDetailQuery() { Id = id };

        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}