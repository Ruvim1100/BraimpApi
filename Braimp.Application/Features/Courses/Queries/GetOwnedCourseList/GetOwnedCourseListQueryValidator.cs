using FluentValidation;

namespace Braimp.Application.Features.Courses.Queries.GetOwnedCourseList;
public class GetOwnedCourseListQueryValidator : AbstractValidator<GetOwnedCourseListQuery>
{
    public GetOwnedCourseListQueryValidator()
    {

        RuleFor(query => query.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than zero");

        RuleFor(query => query.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100");
    }
}
