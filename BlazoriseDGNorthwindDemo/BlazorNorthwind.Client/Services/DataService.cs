using BlazorNorthwind.Infrastructure.Shared.Pagination;
using System.Net.Http.Json;

namespace BlazorNorthwind.Client.Services
{
    public abstract class DataService<TDto> : IDataService<TDto>
        where TDto : class
    {
        protected readonly HttpClient _client;

        public abstract string Endpoint { get; }

        public DataService(HttpClient client)
        {
            _client = client;
        }
        public virtual async Task<PagedResult<TDto>> GetData(PagedQueryModel pagedQueryModel)
        {
            var response = await _client.PostAsJsonAsync($"api/{Endpoint}", pagedQueryModel);
            return await response.Content.ReadFromJsonAsync<PagedResult<TDto>>();
        }
    }

    public interface IDataService<TDto> where TDto : class
    {
        Task<PagedResult<TDto>> GetData(PagedQueryModel pagedQueryModel);
    }
}
