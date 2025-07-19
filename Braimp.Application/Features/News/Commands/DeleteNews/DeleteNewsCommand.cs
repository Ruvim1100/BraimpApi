using MediatR;

namespace Braimp.Application.Features.News.Commands.DeleteNews;
public class DeleteNewsCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
