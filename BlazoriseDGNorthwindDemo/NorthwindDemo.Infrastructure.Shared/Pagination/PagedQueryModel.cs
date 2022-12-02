namespace BlazorNorthwind.Infrastructure.Shared.Pagination
{
    public class PagedQueryModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public PagedQueryColumnModel[] SortColumns { get; set; } = Array.Empty<PagedQueryColumnModel>();
    }

    public class PagedQueryColumnModel
    {
        public string Name { get; set; }
        public string Direction { get; set; }
    }
}
