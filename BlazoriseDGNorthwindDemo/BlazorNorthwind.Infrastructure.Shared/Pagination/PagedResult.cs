namespace BlazorNorthwind.Infrastructure.Shared.Pagination
{
    // Read more: "Paging with Entity Framework Core": https://www.codingame.com/playgrounds/5363/paging-with-entity-framework-core

    /// <summary>
    /// Based class PagedResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
        public IList<T> Items { get; set; } = new List<T>();
    }
}
