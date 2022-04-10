using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using NorthwindDemo.Client.Services;
using NorthwindDemo.Infrastructure.Shared.Pagination;
using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Pages
{
    public partial class Orders
    {
        [Inject]
        private IOrderDataService OrderDataService { get; set; }
        protected List<OrderDto> OrderList { get; set; } = new List<OrderDto>();
        protected int Total { get; set; }
        protected OrderDto SelectedOrder { get; set; }
        private async Task OnReadData(DataGridReadDataEventArgs<OrderDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                var pagedQueryModel = GetPagedQueryModel(e);

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    var pagedResult = await OrderDataService.GetData(pagedQueryModel);

                    Total = pagedResult.RowCount;
                    OrderList = pagedResult.Items.ToList();
                }
            }
        }
        private static PagedQueryModel GetPagedQueryModel(DataGridReadDataEventArgs<OrderDto> e)
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
    }
}
