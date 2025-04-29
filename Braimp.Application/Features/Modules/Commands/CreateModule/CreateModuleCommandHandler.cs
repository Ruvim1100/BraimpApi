using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.LearningContent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateModuleCommand, Guid>
{
    public async Task<Guid> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var courseExist = await dbContext.Courses.
            AnyAsync(course => course.Id == request.CourseId, cancellationToken);

        if (!courseExist)
            throw new NotFoundException(nameof(Course), request.CourseId);

        var module = new Module
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsVisibleToStudent = request.IsVisibleToStudent,
            SortIndex = request.SortIndex,
            CourseId = request.CourseId,
        };

        dbContext.Modules.Add(module);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return module.Id;
    }
}
