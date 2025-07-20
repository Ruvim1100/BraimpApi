using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
public class GetEnrollmentRequestListQueryValidator : AbstractValidator<GetEnrollmentRequestListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetEnrollmentRequestListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(query => query)
            .MustAsync(CourseExists).WithMessage("Course was not found");
    }

    private async Task<bool> CourseExists(GetEnrollmentRequestListQuery command, CancellationToken cancellationToken) =>
        await _dbContext.Courses
        .AnyAsync(course => course.Id == command.CourseId, 
            cancellationToken);
}
