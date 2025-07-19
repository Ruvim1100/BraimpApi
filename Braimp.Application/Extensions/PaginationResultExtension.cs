using Braimp.Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Extensions;
public static class PaginationResultExtension
{
    public static async Task<PaginationResult<T>> ToPagedListAsync<T>(this IQueryable<T> source, PaginationRequest<T> paginationRequest, CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        var items = await source
            .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PaginationResult<T>(items, paginationRequest.Page, paginationRequest.PageSize, totalCount);
    }
}
