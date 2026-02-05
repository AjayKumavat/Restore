using Microsoft.EntityFrameworkCore;

namespace API.RequestHelper;

public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        Metadata = new PaginationMetadata
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize) 
            //Totalpages example, count = 12, pageSize = 10 => 12 / 10 = 1.2 => Ceiling = 2 pages
        };
        AddRange(items);
    }

    public PaginationMetadata Metadata { get; set; }

    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, 
        int pageNumber, int pageSize)
    {
        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}