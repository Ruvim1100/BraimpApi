using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonList;
internal class GetLessonListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetLessonListQuery, LessonListResponse>
{
    public async Task<LessonListResponse> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
    {
        var lessons = await dbContext.Lessons
            .Where(lesson => lesson.ModuleId == request.ModuleId)
            .OrderBy(lesson => lesson.SortIndex)
            .ProjectTo<LessonLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new LessonListResponse { Lessons = lessons };
    }
}
