using Microsoft.EntityFrameworkCore;

namespace My_Movie.DTO
{
    public class PageList<T>
    {
        public PageList()
        {
        }

        public PageList(int totalCount, List<T> item, int page, int pageSize)
        {
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            Item = item;
        }

        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public bool HashNextPage => Page * PageSize < TotalCount;
        public bool HashPreviousPage => Page > 1;
        public List<T> Item { get; set; }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new(totalCount, items, page, pageSize);
        }
    }
}