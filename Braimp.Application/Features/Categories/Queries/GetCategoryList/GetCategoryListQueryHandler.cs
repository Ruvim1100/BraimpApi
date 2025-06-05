using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Categories.Queries.GetCategoryList;
public class GetCategoryListQueryHandler(IBraimpDbContext dbContext, IMapper mapper, 
    ILogger<GetCategoryListQueryHandler> logger) : IRequestHandler<GetCategoryListQuery, CategoryListResponse>
{
    public async Task<CategoryListResponse> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting GetCategoryListQuery handling");

        var categories = await dbContext.CourseCategories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ProjectTo<CategoryLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        logger.LogInformation(
            "GetCategoryListQuery completed successfully: found {Count} categories",
            categories.Count);

        return new CategoryListResponse
        {
            Categories = categories
        };
    }
}
