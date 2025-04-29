using MediatR;

namespace Braimp.Application.Features.Categories.Queries.GetCategoryList;
public class GetCategoryListQuery : IRequest<CategoryListResponse>;