using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.CourseImages.Commands.DeleteCourseImage;
public class DeleteCourseImageCommandValidator : AbstractValidator<DeleteCourseImageCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public DeleteCourseImageCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("Course Id is required");

        RuleFor(command => command)
            .MustAsync(CourseImageExists)
            .WithMessage("Course Image not found");
    }

    private async Task<bool> CourseImageExists(DeleteCourseImageCommand command, CancellationToken cancellationToken) =>
        await _dbContext.CourseImages.AnyAsync(image => image.Id == command.Id && 
        image.CourseId == command.CourseId, cancellationToken);
}
