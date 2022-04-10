using Microsoft.AspNetCore.Components;
using NorthwindDemo.Client.Services;
using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Pages
{
    public partial class Customers
    {
        [Inject]
        private ICustomerDataService CustomerDataService { get; set; }

        protected List<CustomerDto> CustomerList { get; set; }

        protected CustomerDto SelectedCustomer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CustomerList = await CustomerDataService.GetCustomers();
            await base.OnInitializedAsync();
        }
    }
}
