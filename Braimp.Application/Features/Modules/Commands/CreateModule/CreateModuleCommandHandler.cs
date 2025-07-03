using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.LearningContent;
using MediatR;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateModuleCommand, Guid>
{
    public async Task<Guid> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = new Module
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            IsPublished = request.IsPublished,
            SortIndex = request.SortIndex,
            CourseId = request.CourseId,
        };

        dbContext.Modules.Add(module);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return module.Id;
    }
}
