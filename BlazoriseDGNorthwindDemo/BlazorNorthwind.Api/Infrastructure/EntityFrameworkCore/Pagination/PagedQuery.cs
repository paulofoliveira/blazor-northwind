namespace BlazorNorthwind.Api.Infrastructure.EntityFrameworkCore.Pagination
{
    public class PagedQuery
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public IEnumerable<PagedQueryColumn> SortColumns { get; init; }
    }

    public class PagedQueryColumn
    {
        public string Name { get; init; }
        public string Direction { get; init; }
    }
}
