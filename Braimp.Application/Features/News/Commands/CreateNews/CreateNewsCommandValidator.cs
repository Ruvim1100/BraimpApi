using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.CreateNews;
public class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateNewsCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters");

        RuleFor(command => command.Content)
            .NotEmpty().WithMessage("Content is required")
            .MaximumLength(1000).WithMessage("Content must be at most 1000 characters");

        RuleFor(command => command.FileStream.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024)
            .WithMessage("File size must be less than 5MB.");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't Exist");
    }

    private async Task<bool> CourseExists(CreateNewsCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId);
}
