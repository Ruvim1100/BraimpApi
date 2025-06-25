using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.CourseImages.Commands.CreateCourseImage;
public class CreatecourseImageCommandValidator : AbstractValidator<CreateCourseImageCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public CreatecourseImageCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(command => command.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MaximumLength(255).WithMessage("Display name must be less than 255 characters");

        RuleFor(command => command.FileStream)
            .NotNull().WithMessage("File stream is required")
            .Must(stream => stream.CanRead && stream.Length > 0)
            .WithMessage("File stream must be readable and not empty");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Assignemnt doesn't exist");
    }

    private async Task<bool> CourseExists(CreateCourseImageCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId, cancellationToken);
}
