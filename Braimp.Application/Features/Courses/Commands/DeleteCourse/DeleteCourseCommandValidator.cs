using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteCourseCommandValidator(IBraimpDbContext dbContext) 
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty);
        RuleFor(command => command)
            .MustAsync(CourseExists).WithMessage("Course was not found.");
    }
    private async Task<bool> CourseExists(DeleteCourseCommand command, CancellationToken cancellationToken) =>
       await _dbContext.Courses.AnyAsync(course => course.Id == command.Id);
}
