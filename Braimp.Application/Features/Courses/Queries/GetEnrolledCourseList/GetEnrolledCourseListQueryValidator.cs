using FluentValidation;

namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class GetEnrolledCourseListQueryValidator : AbstractValidator<GetEnrolledCourseListQuery>
{
    public GetEnrolledCourseListQueryValidator()
    {

        RuleFor(query => query.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than zero");

        RuleFor(query => query.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100");
    }
}
