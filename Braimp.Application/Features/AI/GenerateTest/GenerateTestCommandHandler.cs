using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Quizzes.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Braimp.Application.Features.AI.GenerateTest;

public class GenerateTestCommandHandler(IAiService aiService, IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<GenerateTestCommand, Guid>
{
    public async Task<Guid> Handle(GenerateTestCommand request, CancellationToken cancellationToken)
    {
        var prompt = string.Format(PromptTemplates.GenerateTest, request.QuestionCount, request.Language, request.SourceText);
        var aiResponse = await aiService.GenerateTestAsync(prompt, cancellationToken);

        if (string.IsNullOrWhiteSpace(aiResponse.message))
        {
            throw new ArgumentException("AI returned an empty result.");
        }

        var generatedQuiz = JsonSerializer.Deserialize<GeneratedQuiz>(aiResponse.message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (generatedQuiz == null || generatedQuiz.Questions == null || generatedQuiz.Questions.Count == 0)
        {
            throw new ArgumentException("AI returned an invalid quiz structure.");
        }

        var maxSortIndex = await dbContext.Quizzes
            .Where(quiz => quiz.CourseId == request.CourseId)
            .MaxAsync(quiz => (int?)quiz.SortIndex, cancellationToken) ?? -1;

        var quizId = Guid.NewGuid();
        var quiz = new Quiz
        {
            Id = quizId,
            CourseId = request.CourseId,
            Title = request.Title,
            MaxAttempts = 1,
            IsPublished = false,
            IsRandomized = false,
            SortIndex = maxSortIndex + 1,
            Questions = new List<QuizQuestion>()
        };

        int questionSortIndex = 0;
        foreach (var generatedQuestion in generatedQuiz.Questions)
        {
            var questionId = Guid.NewGuid();
            var quizQuestion = new QuizQuestion
            {
                Id = questionId,
                Text = generatedQuestion.Text,
                QuestionType = QuestionType.SingleChoice,
                Weight = 1,
                QuizId = quizId,
                SortIndex = questionSortIndex++,
                QuestionOptions = new List<QuestionOption>()
            };

            foreach (var generatedOption in generatedQuestion.Options)
            {
                var questionOption = new QuestionOption
                {
                    Id = Guid.NewGuid(),
                    Text = generatedOption.Text,
                    QuizQuestionId = questionId,
                    IsCorrect = generatedOption.IsCorrect
                    
                };

                quizQuestion.QuestionOptions.Add(questionOption);
            }

            quiz.Questions.Add(quizQuestion);
        }

        dbContext.Quizzes.Add(quiz);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return quizId;
    }
}
