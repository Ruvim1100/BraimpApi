using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetPublishedLessonList;
public class GetPublishedLessonListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetPublishedLessonListQuery, PublishedLessonListResponse>
{
    public async Task<PublishedLessonListResponse> Handle(GetPublishedLessonListQuery request, CancellationToken cancellationToken)
    {
        var lessons = await dbContext.Lessons
            .Where(lesson => lesson.ModuleId == request.ModuleId && lesson.IsPublished)
            .OrderBy(lesson => lesson.SortIndex)
            .ProjectTo<PublishedLessonLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PublishedLessonListResponse { Lessons = lessons };
    }
}
