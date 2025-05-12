using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using Braimp.Domain.Entities.Courses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await dbContext.CourseCategories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category is null)
            throw new NotFoundException(nameof(CourseCategory), request.Id);

        if (category.Name != request.Name)
            category.Name = request.Name;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
