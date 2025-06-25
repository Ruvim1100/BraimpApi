using MediatR;

namespace Braimp.Application.Features.CourseImages.Commands.DeleteCourseImage;
public class DeleteCourseImageCommand : IRequest
{
    public Guid CourseId { get; set; }
    public Guid Id { get; set; }
}
