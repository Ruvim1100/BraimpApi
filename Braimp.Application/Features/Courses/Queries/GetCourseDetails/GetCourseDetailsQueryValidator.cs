using FluentValidation;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailsQueryValidator : AbstractValidator<GetCourseDetailQuery>
{
    public GetCourseDetailsQueryValidator()
    {
        RuleFor(getCourseDetailQuery => getCourseDetailQuery.Id)
            .NotEmpty()
            .WithMessage("Course Id is requred");
    }
}
