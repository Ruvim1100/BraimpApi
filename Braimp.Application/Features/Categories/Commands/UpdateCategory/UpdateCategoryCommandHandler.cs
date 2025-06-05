using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, ILogger<UpdateCategoryCommandHandler> logger) 
    : IRequestHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting UpdateCategoryCommand handling: CategoryId={CategoryId}, NewName={NewName}",
            request.Id,
            request.Name);

        var category = await dbContext.CourseCategories
            .FirstAsync(x => x.Id == request.Id, cancellationToken);

        if (category.Name != request.Name)
        {
            logger.LogDebug("Changing category name from '{OldName}' to '{NewName}'",
                category.Name,
                request.Name);
            category.Name = request.Name.Trim();
        }
        else
        {
            logger.LogDebug("UpdateCategoryCommand: category name unchanged ('{CategoryName}')", 
                category.Name);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "UpdateCategoryCommand completed successfully: CategoryId={CategoryId}",
            request.Id);

        return Unit.Value;
    }
}
