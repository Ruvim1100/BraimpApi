using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.LearningContent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateModuleCommand, Guid>
{
    public async Task<Guid> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var maxSortIndex = await dbContext.Modules
            .Where(module => module.CourseId == request.CourseId)
            .MaxAsync(module => (int?)module.SortIndex, cancellationToken) ?? -1;
        var module = new Module
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            IsPublished = request.IsPublished,
            SortIndex = maxSortIndex + 1,
            CourseId = request.CourseId,
        };

        dbContext.Modules.Add(module);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return module.Id;
    }
}
