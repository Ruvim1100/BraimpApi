using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Queries.GetLessonBlockList;
public class GetLessonBlockListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetLessonBlockListQuery, LessonBlockListResponse>
{
    public async Task<LessonBlockListResponse> Handle(GetLessonBlockListQuery request, CancellationToken cancellationToken)
    {
        var lessonBlocks = await dbContext.LessonBlocks
            .Where(block => block.LessonId == request.LessonId)
            .OrderBy(block => block.SortIndex)
            .ProjectTo<LessonBlockLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new LessonBlockListResponse { LessonBlocks = lessonBlocks };
    }
}
