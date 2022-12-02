using BlazorNorthwind.Models;

namespace BlazorNorthwind.Client.Services
{
    public class CustomerDataService : DataService<CustomerDto>, ICustomerDataService
    {
        public CustomerDataService(HttpClient client) : base(client)
        {
        }

        public override string Endpoint => "customers";
    }

    public interface ICustomerDataService : IDataService<CustomerDto> { }
}
