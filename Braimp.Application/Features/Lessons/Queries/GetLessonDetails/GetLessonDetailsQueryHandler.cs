using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
public class GetLessonDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetLessonDetailsQuery, LessonDetailsResponse>
{
    public async Task<LessonDetailsResponse> Handle(GetLessonDetailsQuery request, CancellationToken cancellationToken)
    {
        var lesson = await dbContext.Lessons
            .FirstAsync(lesson => lesson.Id == request.Id, cancellationToken);
        return mapper.Map<LessonDetailsResponse>(lesson);
    }
}
