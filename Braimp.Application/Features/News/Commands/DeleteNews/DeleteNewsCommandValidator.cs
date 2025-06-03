using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.DeleteNews;
public class DeleteNewsCommandValidator : AbstractValidator<DeleteNewsCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteNewsCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("NewsId is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(command => command)
            .MustAsync(NewsExists)
            .WithMessage("News doesn't exist");
    }

    private async Task<bool> NewsExists(DeleteNewsCommand command, CancellationToken cancellationToken) =>
        await _dbContext.CourseNews
        .AnyAsync(news => news.Id == command.Id && 
        news.CourseId == command.CourseId,
            cancellationToken);
}
