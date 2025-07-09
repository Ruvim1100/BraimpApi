using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Commands.UpdateLessonBlock;
public class UpdateLessonBlockCommandValidator : AbstractValidator<UpdateLessonBlockCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public UpdateLessonBlockCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is Required");

        RuleFor(command => command.LessonId)
            .NotEmpty().WithMessage("LessonId is Required");

        RuleFor(command => command.Content)
            .NotEmpty().WithMessage("Content is Required")
            .MaximumLength(10000).WithMessage("Content cannot exceed 10,000 characters.");

        RuleFor(command => command)
            .MustAsync(LessonBlockExists).WithMessage("Lesson Block doesn't exist");
    }

    private async Task<bool> LessonBlockExists(UpdateLessonBlockCommand command, CancellationToken cancellationToken) =>
        await _dbContext.LessonBlocks.AnyAsync(block => block.Id == command.Id && block.LessonId == command.LessonId, cancellationToken);
}
