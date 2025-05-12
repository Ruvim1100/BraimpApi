using MediatR;

namespace Braimp.Application.Pagination;
public class PaginationRequest<TResponse> : IRequest<PaginationResult<TResponse>>
{
    public string? SortBy { get; set; } = "CreatedAt";
    public bool? Descending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
