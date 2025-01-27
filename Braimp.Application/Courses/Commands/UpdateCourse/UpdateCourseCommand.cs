using MediatR;

namespace Braimp.Application.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? CourseCategoryId { get; set; }
    }
}
