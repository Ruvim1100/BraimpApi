using FluentValidation;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class GetModuleListQueryValidator : AbstractValidator<GetModuleListQuery>
{
    public GetModuleListQueryValidator()
    {
        RuleFor(q => q.CourseId)
            .NotEmpty()
            .WithMessage("CourseId must be provided.");

        RuleFor(q => q.SearchTerm)
            .MaximumLength(200)
            .WithMessage("SearchTerm must not exceed 200 characters.");
    }
}