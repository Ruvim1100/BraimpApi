using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AI.TranslateLesson;
public class TranslateLessonCommandValidator : AbstractValidator<TranslateLessonCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public TranslateLessonCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.LessonId)
                   .NotEmpty().WithMessage("LessonId is Required");

        RuleFor(command => command)
            .MustAsync(LessonExists).WithMessage("Lesson was not Found");
    }

    private async Task<bool> LessonExists(TranslateLessonCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Lessons.AnyAsync(lesson => lesson.Id == command.LessonId,
            cancellationToken);
}
