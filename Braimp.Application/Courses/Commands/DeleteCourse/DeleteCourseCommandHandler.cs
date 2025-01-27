using Braimp.Application.Common.Exceptions;
using Braimp.Application.Interfaces;
using Braimp.Domain.Entities;
using MediatR;

namespace Braimp.Application.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
    {
        private readonly IBraimpDbContext _dbContext;

        public DeleteCourseCommandHandler(IBraimpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses
                .FindAsync(new object[] {request.Id}, cancellationToken);

            if (course == null || request.OwnerId != course.OwnerId)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
