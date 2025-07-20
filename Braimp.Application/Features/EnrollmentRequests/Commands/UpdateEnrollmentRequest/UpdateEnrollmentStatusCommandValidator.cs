using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.UpdateEnrollmentRequest;
public class UpdateEnrollmentStatusCommandValidator : AbstractValidator<UpdateEnrollmentRequestCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public UpdateEnrollmentStatusCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is Required");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(command => command.Status)
            .NotEmpty().WithMessage("Status is Required");

        RuleFor(command => command)
            .MustAsync(EnrollmentRequestExists);
    }

    private async Task<bool> EnrollmentRequestExists(UpdateEnrollmentRequestCommand command, CancellationToken cancellationToken) =>
        await _dbContext.EnrollmentRequests
        .AnyAsync(request => request.Id == command.Id && 
        request.CourseId == command.CourseId, 
            cancellationToken);
}
