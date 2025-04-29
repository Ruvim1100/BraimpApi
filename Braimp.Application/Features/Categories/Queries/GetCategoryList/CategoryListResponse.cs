namespace Braimp.Application.Features.Categories.Queries.GetCategoryList;
public class CategoryListResponse
{
    public IList<CategoryLookupDto> Categories { get; set; } = new List<CategoryLookupDto>();
}
