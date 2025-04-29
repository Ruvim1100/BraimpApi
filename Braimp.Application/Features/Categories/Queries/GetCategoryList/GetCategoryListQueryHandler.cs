using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Categories.Queries.GetCategoryList;
public class GetCategoryListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetCategoryListQuery, CategoryListResponse>
{
    public async Task<CategoryListResponse> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await dbContext.CourseCategories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ProjectTo<CategoryLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CategoryListResponse
        {
            Categories = categories
        };
    }
}
