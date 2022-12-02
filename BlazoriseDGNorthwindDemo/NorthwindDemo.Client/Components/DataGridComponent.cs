using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using BlazorNorthwind.Client.Services;
using BlazorNorthwind.Infrastructure.Shared.Pagination;

namespace BlazorNorthwind.Client.Components
{
    public abstract class DataGridComponent<TDto, TDataService> : ComponentBase
        where TDto : class
        where TDataService : IDataService<TDto>
    {
        protected abstract string Title { get; }

        [Inject]
        protected TDataService DataService { get; set; }
        protected List<TDto> List { get; set; } = new List<TDto>();
        protected int Total { get; set; }
        protected TDto SelectedRow { get; set; }
        public int PageSize { get; set; } = 10;
        protected async Task OnReadData(DataGridReadDataEventArgs<TDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                var pagedQueryModel = GetPagedQueryModel(e);

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    var pagedResult = await DataService.GetData(pagedQueryModel);

                    Total = pagedResult.RowCount;
                    List = pagedResult.Items.ToList();

                    SelectedRow = null;

                    StateHasChanged();
                }
            }
        }
        protected static PagedQueryModel GetPagedQueryModel(DataGridReadDataEventArgs<TDto> e)
        {
            var result = e.ReadDataMode == DataGridReadDataMode.Virtualize ? new PagedQueryModel()
            {
                Page = e.VirtualizeOffset,
                PageSize = e.VirtualizeCount
            } : e.ReadDataMode == DataGridReadDataMode.Paging ? new PagedQueryModel()
            {
                Page = (e.Page - 1) * e.PageSize,
                PageSize = e.PageSize
            } : default;

            if (result == null)
                throw new Exception("Unhandled ReadDataMode");

            result.SortColumns = e.Columns.Where(x => x.SortDirection != SortDirection.Default)
                                     .OrderBy(x => x.SortIndex)
                                     .Select(x => new PagedQueryColumnModel
                                     {
                                         Name = x.Field,
                                         Direction = x.SortDirection == SortDirection.Ascending ? "asc" : "desc"
                                     }).ToArray();

            return result;
        }
        protected void PageSizeChanged(int pageSize)
        {
            PageSize = pageSize;
        }
    }
}
