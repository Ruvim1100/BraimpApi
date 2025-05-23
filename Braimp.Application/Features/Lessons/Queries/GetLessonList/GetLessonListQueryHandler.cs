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
        var query = dbContext.Lessons.Where(lesson => lesson.ModuleId == request.ModuleId);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(lesson =>
                lesson.Title.Contains(request.SearchTerm) ||
                (lesson.Description != null && lesson.Description.Contains(request.SearchTerm)));
        }

        if (request.IsPublished.HasValue)
        {
            query = query.Where(lesson => lesson.IsPublished == request.IsPublished.Value);
        }

        var lessons = await query.OrderBy(lesson => lesson.SortIndex)
            .ProjectTo<LessonLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new LessonListResponse { Lessons = lessons };
    }
}
