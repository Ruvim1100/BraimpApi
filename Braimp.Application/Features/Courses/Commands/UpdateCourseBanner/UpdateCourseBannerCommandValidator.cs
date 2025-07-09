using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourseBanner;
public class UpdateCourseBannerCommandValidator : AbstractValidator<UpdateCourseBannerCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public UpdateCourseBannerCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(command => command.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MaximumLength(255).WithMessage("Display name must be less than 255 characters");

        RuleFor(command => command.OriginalFileName)
            .Must(fileName =>
            {
                var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
                return AllowedExtensions.Contains(ext);
            })
            .WithMessage("Only image files (.jpg, .jpeg, .png, .webp) are allowed.");

        RuleFor(command => command.FileStream)
            .NotNull().WithMessage("File stream is required")
            .Must(stream => stream.CanRead && stream.Length > 0)
            .WithMessage("File stream must be readable and not empty");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist");

        RuleFor(command => command.FileStream.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024)
            .WithMessage("File size must be less than 5MB.");

    }

    private async Task<bool> CourseExists(UpdateCourseBannerCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.Id, cancellationToken);
}
