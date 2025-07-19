using Braimp.Application.Features.News.Commands.CreateNews;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.CourseNews.CreateCourseNews;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.CourseNews.Create, Handler)
            .RequireAuthorization(Roles.User)
            .Accepts<Request>("multipart/form-data")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status201Created)
            .WithTags(EndpointTags.CourseNews)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromRoute] Guid courseId, [FromForm] Request request, 
        IMediator mediator, CancellationToken cancellationToken)
    {
        using var stream = request.File.OpenReadStream();

        var command = new CreateNewsCommand
        {
            CourseId = courseId,
            Title = request.Title,
            Content = request.Content,
            FileDisplayName = request.FileDisplayName,
            OriginalFileName = request.File.FileName,
            FileStream = stream,
        };

        await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
