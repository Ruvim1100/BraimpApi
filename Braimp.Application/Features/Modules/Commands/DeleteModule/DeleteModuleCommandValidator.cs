using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.DeleteModule;
public class DeleteModuleCommandValidator : AbstractValidator<DeleteModuleCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteModuleCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Module id is required.");

        RuleFor(command => command)
            .MustAsync(ModuleExists)
            .WithMessage("Module doesn't exist");
    }

    private Task<bool> ModuleExists(DeleteModuleCommand command, CancellationToken cancellationToken) =>
        _dbContext.Modules.AnyAsync(module => module.Id == command.Id 
        && module.CourseId == command.CourseId);
}
