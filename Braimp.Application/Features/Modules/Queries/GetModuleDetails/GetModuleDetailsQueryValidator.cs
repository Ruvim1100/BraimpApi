using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetModuleDetails;
public class GetModuleDetailsQueryValidator : AbstractValidator<GetModuleDetailsQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetModuleDetailsQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("ModuleId is required.");

        RuleFor(query => query)
            .MustAsync(ModuleExists)
            .WithMessage("Module doesn't exist");
    }

    public Task<bool> ModuleExists(GetModuleDetailsQuery query, CancellationToken cancellationToken) =>
        _dbContext.Modules.AnyAsync(module => module.Id == query.Id && module.CourseId == query.CourseId);
}
