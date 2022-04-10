using NorthwindDemo.Models;
using System.Net.Http.Json;

namespace NorthwindDemo.Client.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly HttpClient _httpClient;
        public CustomerDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CustomerDto>> GetCustomers()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customers");
        }
    }

    public interface ICustomerDataService
    {
        Task<List<CustomerDto>> GetCustomers();
    }
}
