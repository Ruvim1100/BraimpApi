using FluentValidation;

namespace Braimp.Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryValidator : AbstractValidator<GetCourseDetailQuery>
    {
        public GetCourseDetailsQueryValidator()
        {
            RuleFor(getCourseDetailQuery => getCourseDetailQuery.Id)
                .NotEqual(Guid.Empty);
            RuleFor(getCourseDetailQuery => getCourseDetailQuery.OwnerId)
                .NotEqual(Guid.Empty);
        }
    }
}
