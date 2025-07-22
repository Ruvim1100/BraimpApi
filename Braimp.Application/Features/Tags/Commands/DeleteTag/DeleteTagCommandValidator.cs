using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Tags.Commands.DeleteTag;
public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteTagCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is Required");

        RuleFor(command => command)
            .MustAsync(TagExists).WithMessage("Tag with the specified ID does not exist."); ;
    }

    private async Task<bool> TagExists(DeleteTagCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Tags.AnyAsync(tag => tag.Id == command.Id, cancellationToken);
}
