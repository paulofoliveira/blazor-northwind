using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using NorthwindDemo.Client.Services;
using NorthwindDemo.Infrastructure.Shared.Pagination;
using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Pages
{
    public partial class Customers
    {
        [Inject]
        private ICustomerDataService CustomerDataService { get; set; }

        protected List<CustomerDto> CustomerList { get; set; } = new List<CustomerDto>();
        protected int Total { get; set; }

        protected CustomerDto SelectedCustomer { get; set; }

        private async Task OnReadData(DataGridReadDataEventArgs<CustomerDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                var pagedQueryModel = GetPagedQueryModel(e);

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    var pagedResult = await CustomerDataService.GetCustomers(pagedQueryModel);

                    Total = pagedResult.RowCount;
                    CustomerList = pagedResult.Items.ToList();
                }
            }
        }
        private static PagedQueryModel GetPagedQueryModel(DataGridReadDataEventArgs<CustomerDto> e)
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

            result.SortableColumns = e.Columns.Where(x => x.SortDirection != SortDirection.Default)
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
