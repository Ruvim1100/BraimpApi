using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.DeleteParticipant;

public class DeleteParticipantCommandValidator : AbstractValidator<DeleteParticipantCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public DeleteParticipantCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("UserId is Required");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(command => command)
            .MustAsync(ParticipantExists);
    }

    private async Task<bool> ParticipantExists(DeleteParticipantCommand command, CancellationToken cancellationToken) =>
        await _dbContext.CourseParticipants
        .AnyAsync(participant => participant.UserId == command.UserId &&
        participant.CourseId == command.CourseId,
            cancellationToken);
}
