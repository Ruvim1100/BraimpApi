using Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;
using Braimp.WebApi.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Braimp.WebApi.Endpoints.QuizQuestions.CreateQuizQuestion;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.QuizQuestions.Create, Handler)
            .RequireAuthorization("User")
            .Accepts<Request>("multipart/form-data")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(EndpointTags.QuizQuestions)
            .WithOpenApi();
    }

    private async Task<IResult> Handler([FromForm] Request request, IMediator mediator, 
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizQuestionCommand
        {
            Text = request.Text,
            QuestionType = request.QuestionType,
            Weight = request.Weight,
            QuizId = request.QuizId,
            CourseId = request.CourseId,
            QuizOptions = request.QuizOptions?.Select(o => new QuizOptionModel
            {
                Text = o.Text,
                IsCorrect = o.IsCorrect
            }).ToList()
        };

        if (request.File != null && request.DisplayName != null)
        {
            var fileStream = request.File.OpenReadStream();

            command.Resource = new ResourceModel
            {
                DisplayName = request.DisplayName,
                OriginalFileName = request.File.FileName,
                FileStream = fileStream,
                Encoding = Encoding.UTF8
            };
        }

        var questionId = await mediator.Send(command, cancellationToken);
        return Results.Created();
    }
}
