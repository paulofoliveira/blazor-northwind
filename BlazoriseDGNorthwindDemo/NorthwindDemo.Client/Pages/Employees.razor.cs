using NorthwindDemo.Client.Components;
using NorthwindDemo.Client.Services;
using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Pages
{
    public class EmployeesComponent : DataGridComponent<EmployeeDto, IEmployeeDataService>
    {
        protected override string Title => "Employees";
    }
}
