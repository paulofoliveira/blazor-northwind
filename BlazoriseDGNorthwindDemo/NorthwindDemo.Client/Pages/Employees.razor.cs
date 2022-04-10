using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using NorthwindDemo.Client.Services;
using NorthwindDemo.Infrastructure.Shared.Pagination;
using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Pages
{
    public partial class Employees
    {
        [Inject]
        private IEmployeeDataService EmployeeDataService { get; set; }
        protected List<EmployeeDto> EmployeeList { get; set; } = new List<EmployeeDto>();
        protected int Total { get; set; }
        protected EmployeeDto SelectedEmployee { get; set; }
        private async Task OnReadData(DataGridReadDataEventArgs<EmployeeDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                var pagedQueryModel = GetPagedQueryModel(e);

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    var pagedResult = await EmployeeDataService.GetData(pagedQueryModel);

                    Total = pagedResult.RowCount;
                    EmployeeList = pagedResult.Items.ToList();
                }
            }
        }
        private static PagedQueryModel GetPagedQueryModel(DataGridReadDataEventArgs<EmployeeDto> e)
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
