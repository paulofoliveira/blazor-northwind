using BlazorNorthwind.Client.Components;
using BlazorNorthwind.Client.Services;
using BlazorNorthwind.Models;

namespace BlazorNorthwind.Client.Pages
{
    public class CustomersComponent : DataGridComponent<CustomerDto, ICustomerDataService>
    {
        protected override string Title => "Customers";
    }
}
