using AutoMapper.QueryableExtensions;
using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Tags.Queries.GetTagList;
public class GetTagListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetTagListQuery, TagListResponse>
{
    public async Task<TagListResponse> Handle(GetTagListQuery request, CancellationToken cancellationToken)
    {
        var tags = await dbContext.Tags
        .AsNoTracking()
            .ProjectTo<TagLookupModel>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync(cancellationToken);

        return new TagListResponse { Tags = tags };
    }
}
