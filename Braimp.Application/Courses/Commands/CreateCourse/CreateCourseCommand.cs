using Braimp.Domain.Entities;
using MediatR;

namespace Braimp.Application.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<Guid>
    {
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid CourseCategoryId { get; set; }
    }
}
