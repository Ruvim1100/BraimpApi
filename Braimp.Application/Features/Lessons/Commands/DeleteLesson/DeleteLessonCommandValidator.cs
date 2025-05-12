using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Commands.DeleteLesson;
public class DeleteLessonCommandValidator : AbstractValidator<DeleteLessonCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteLessonCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("LessonId is required.")
            .NotEqual(Guid.Empty).WithMessage("LessonId cannot be empty.");

        RuleFor(command => command.ModuleId)
            .NotEmpty().WithMessage("ModuleId is required.")
            .NotEqual(Guid.Empty).WithMessage("ModuleId cannot be empty.");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required.")
            .NotEqual(Guid.Empty).WithMessage("CourseId cannot be empty.");

        RuleFor(command => command)
            .MustAsync(LessonExists)
            .WithMessage("Lesson does not exist in the specified module or course.");
    }

    private Task<bool> LessonExists(DeleteLessonCommand command, CancellationToken cancellationToken) =>
        _dbContext.Lessons.AnyAsync(lesson => lesson.Id == command.Id
                         && lesson.ModuleId == command.ModuleId
                         && lesson.Module.CourseId == command.CourseId,
                      cancellationToken);
}
