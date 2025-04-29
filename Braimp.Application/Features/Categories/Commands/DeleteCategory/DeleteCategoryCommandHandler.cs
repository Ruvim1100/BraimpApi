using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses;
using MediatR;

namespace Braimp.Application.Features.Categories.Commands.DeleteCategory;

internal class DeleteCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await dbContext.CourseCategories
            .FindAsync([request.Id], cancellationToken);

        if (category == null)
            throw new NotFoundException(nameof(CourseCategory), request.Id);

        dbContext.CourseCategories.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
