using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ILogger<CreateCategoryCommandHandler> logger) : IRequestHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting CreateCategoryCommand handling: Name={CategoryName}", request.Name);
        
        var category = new CourseCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim()
        };

        logger.LogDebug("Adding new category to DbContext: CategoryId={CategoryId}, Name={CategoryName}", 
            category.Id, category.Name);

        dbContext.CourseCategories.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CreateCategoryCommand completed successfully: " +
            "category created with Id={CategoryId}", category.Id);

        return category.Id;
    }
}
