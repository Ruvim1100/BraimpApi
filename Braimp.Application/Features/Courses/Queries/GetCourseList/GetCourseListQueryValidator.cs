using FluentValidation;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
internal class GetCourseListQueryValidator : AbstractValidator<GetCourseListQuery>
{
    public GetCourseListQueryValidator()
    {

        RuleFor(x => x.SortBy)
            .Must(value => string.IsNullOrWhiteSpace(value) ||
                           new[] { "title", "createdat" }.Contains(value.ToLower()))
            .WithMessage("SortBy must be either 'title' or 'createdat'");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than zero");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100");
    }
}
