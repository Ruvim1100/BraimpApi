using FluentValidation;

namespace Braimp.Application.Features.Modules.Queries.GetModuleDetails;
public class GetModuleDetailsQueryValidator : AbstractValidator<GetModuleDetailsQuery>
{
    public GetModuleDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Module id is required.");
    }
}
