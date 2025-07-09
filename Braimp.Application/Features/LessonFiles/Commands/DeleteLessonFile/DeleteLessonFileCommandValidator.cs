using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonFiles.Commands.DeleteLessonFile;
public class DeleteLessonFileCommandValidator : AbstractValidator<DeleteLessonFileCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public DeleteLessonFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is Required");

        RuleFor(command => command.LessonId)
            .NotEmpty().WithMessage("Lesson Id is Required");

        RuleFor(command => command)
            .MustAsync(LessonFileExists).WithMessage("File doesn't exist");
    }

    private async Task<bool> LessonFileExists(DeleteLessonFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.LessonFiles
        .AnyAsync(file => file.Id == command.Id && 
        file.LessonId == command.LessonId, 
            cancellationToken);
}
