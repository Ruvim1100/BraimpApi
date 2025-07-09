using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Commands.UpdateLesson;
public class UpdateLessonCommandValidator : AbstractValidator<UpdateLessonCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateLessonCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("LessonId is required.");

        RuleFor(command => command.ModuleId)
            .NotEmpty()
            .WithMessage("ModuleId is required.");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required.");
        
        RuleFor(command => command.Title)
            .MaximumLength(100)
            .WithMessage("Lesson title must be at most 100 characters.");

        RuleFor(command => command.Description)
            .MaximumLength(1000)
            .WithMessage("Lesson Description must be at most 1000 characters.");

        RuleFor(command => command)
            .MustAsync(LessonExists)
            .WithMessage("Lesson does not exist in the specified module or course.");
    }

    private Task<bool> LessonExists(UpdateLessonCommand command, CancellationToken cancellationToken) =>
        _dbContext.Lessons.AnyAsync(lesson => lesson.Id == command.Id 
        && lesson.ModuleId == command.ModuleId
        && lesson.Module.CourseId == command.CourseId,
            cancellationToken);
}
