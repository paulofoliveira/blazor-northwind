namespace NorthwindDemo.Infrastructure.Shared.Pagination
{
    public class PagedQueryModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public PagedQueryColumnModel[] SortableColumns { get; set; }
    }

    public class PagedQueryColumnModel
    {
        public string Name { get; set; }
        public string Direction { get; set; }
    }
}
