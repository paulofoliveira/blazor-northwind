using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Services
{
    public class OrderDataService : DataService<OrderDto>, IOrderDataService
    {
        public OrderDataService(HttpClient client) : base(client)
        {
        }

        public override string Endpoint => "orders";
    }

    public interface IOrderDataService : IDataService<OrderDto> { }
}
