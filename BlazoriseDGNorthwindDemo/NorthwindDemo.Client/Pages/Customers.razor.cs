using NorthwindDemo.Client.Components;
using NorthwindDemo.Client.Services;
using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Pages
{
    public class CustomersComponent : DataGridComponent<CustomerDto, ICustomerDataService>
    {
        protected override string Title => "Customers";
    }
}
