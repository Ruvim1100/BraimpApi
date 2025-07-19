using Braimp.Application.Features.News.Commands.DeleteNews;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.CourseNews.DeleteCourseNews;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.CourseNews.Delete, Handler)
            .RequireAuthorization(Roles.User)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.CourseNews);
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromRoute] Guid id, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteNewsCommand
        {
            CourseId = courseId,
            Id = id
        };

        await mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
