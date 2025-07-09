using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Commands.DeleteLessonBlock;
public class DeleteLessonBlockCommandValidator : AbstractValidator<DeleteLessonBlockCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public DeleteLessonBlockCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.LessonId)
            .NotEmpty().WithMessage("LessonId is required");

        RuleFor(command => command)
            .MustAsync(LessonBlockExists).WithMessage("Lesson block with the specified ID and LessonId was not found.");
    }
    private async Task<bool> LessonBlockExists(DeleteLessonBlockCommand command, CancellationToken cancellationToken) =>
         await _dbContext.LessonBlocks.AnyAsync(block => block.Id == command.Id && block.LessonId == command.LessonId, cancellationToken);

}
