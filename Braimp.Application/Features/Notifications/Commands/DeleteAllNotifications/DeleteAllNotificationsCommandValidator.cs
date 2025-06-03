using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.DeleteAllNotifications;
public class DeleteAllNotificationsCommandValidator : AbstractValidator<DeleteAllNotificationsCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public DeleteAllNotificationsCommandValidator(IBraimpDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("Course Id is Required");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist or you are not a participant");
    }

    private async Task<bool> CourseExists(DeleteAllNotificationsCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses
        .AnyAsync(
            course => course.Id == command.CourseId && 
            course.Participants.Any(participant => participant.UserId == _currentUserService.UserId),
            cancellationToken);
}
