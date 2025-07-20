using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.DeleteEnrollmentRequest;
public class DeleteEnrollmentRequestCommandValidator : AbstractValidator<DeleteEnrollmentRequestCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public DeleteEnrollmentRequestCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is Required");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(command => command)
            .MustAsync(EnrollmentRequestExists);
    }

    private async Task<bool> EnrollmentRequestExists(DeleteEnrollmentRequestCommand command, CancellationToken cancellationToken) =>
    await _dbContext.EnrollmentRequests
    .AnyAsync(request => request.Id == command.Id &&
    request.CourseId == command.CourseId,
        cancellationToken);
}
