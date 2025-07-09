using MediatR;

namespace Braimp.Application.Features.LessonBlocks.Queries.GetLessonBlockList;
public class GetLessonBlockListQuery : IRequest<LessonBlockListResponse>
{
    public Guid LessonId { get; set; }
}
