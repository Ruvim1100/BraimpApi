using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Categories.Commands.DeleteCategory;

internal class DeleteCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await dbContext.CourseCategories
            .FirstOrDefaultAsync(category => category.Id == request.Id, cancellationToken);

        dbContext.CourseCategories.Remove(category!);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
