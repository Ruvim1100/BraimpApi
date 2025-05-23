using Braimp.Application.Abstraction;
using FluentValidation;

namespace Braimp.Application.Features.AssignmentFiles.Commands;
public class CreateAssignmentFileCommandValidator : AbstractValidator<CreateAssignmentFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateAssignmentFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
