using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Common.Pagination;
public class PagedList<T>
{
    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
    }

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be at least 1.");
        if (pageSize < 1 || pageSize > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be between 1 and 100.");

        var totalCount = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}