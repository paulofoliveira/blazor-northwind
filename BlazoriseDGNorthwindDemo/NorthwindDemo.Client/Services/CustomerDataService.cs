using NorthwindDemo.Infrastructure.Shared.Pagination;
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
        public async Task<PagedResult<CustomerDto>> GetCustomers(PagedQueryModel pagedQueryModel)
        {
            return await _httpClient.GetFromJsonAsync<PagedResult<CustomerDto>>($"api/customers?{pagedQueryModel.ToQueryString()}");
        }
    }

    public interface ICustomerDataService
    {
        Task<PagedResult<CustomerDto>> GetCustomers(PagedQueryModel pagedQueryModel);
    }
}
