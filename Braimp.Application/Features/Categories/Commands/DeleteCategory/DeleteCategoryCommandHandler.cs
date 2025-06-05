using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Categories.Commands.DeleteCategory;

internal class DeleteCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ILogger<DeleteCategoryCommandHandler> logger) : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting DeleteCategoryCommand handling: CategoryId={CatgoryId}", request.Id);

        var category = await dbContext.CourseCategories
            .FirstAsync(category => category.Id == request.Id, cancellationToken);

        logger.LogDebug("Removing category: Id={CategoryId}, Name={CategoryName}",
            category.Id, category.Name);

        dbContext.CourseCategories.Remove(category!);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("DeleteCategoryCommand completed successfully. " +
            "Deleted category with Id={CategoryId}", request.Id);

        return Unit.Value;
    }
}
