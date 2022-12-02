using BlazorNorthwind.Client.Components;
using BlazorNorthwind.Client.Services;
using BlazorNorthwind.Models;

namespace BlazorNorthwind.Client.Pages
{
    public class OrdersComponent : DataGridComponent<OrderDto, IOrderDataService>
    {
        protected override string Title => "Orders";
    }
}
