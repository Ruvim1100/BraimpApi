using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.UpdateNews;
public class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateNewsCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("NewsId cannot be empty");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId cannot be empty");

        When(command => command.Title != null, () => 
        {
            RuleFor(command => command.Title)
            .MaximumLength(100)
            .WithMessage("Title cannot exceed 100 characters");
        });

        When(command => command.Content != null, () =>
        {
            RuleFor(command => command.Content)
            .MaximumLength(1000)
            .WithMessage("Content cannot exceed 1000 characters");
        });

        RuleFor(command => command)
            .MustAsync(NewsExists)
            .WithMessage("News doesn't exists");
    }

    private async Task<bool> NewsExists(UpdateNewsCommand command, CancellationToken cancellationToken) =>
        await _dbContext.CourseNews
        .AnyAsync(news => news.Id == command.Id &&
        news.CourseId == command.CourseId,
            cancellationToken);
}
