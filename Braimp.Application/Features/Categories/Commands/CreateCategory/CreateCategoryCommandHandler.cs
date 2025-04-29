using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using MediatR;

namespace Braimp.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim()
        };

        dbContext.CourseCategories.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
