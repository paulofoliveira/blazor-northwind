using BlazorNorthwind.Client.Components;
using BlazorNorthwind.Client.Services;
using BlazorNorthwind.Models;

namespace BlazorNorthwind.Client.Pages
{
    public class EmployeesComponent : DataGridComponent<EmployeeDto, IEmployeeDataService>
    {
        protected override string Title => "Employees";
    }
}
