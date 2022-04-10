using NorthwindDemo.Infrastructure.Shared.Pagination;

namespace NorthwindDemo.Client.Services
{
    public static class PagedQueryModelExtensions
    {
        public static string ToQueryString(this PagedQueryModel pagedQueryModel)
        {
            return $"&page={pagedQueryModel.Page}&pageSize={pagedQueryModel.PageSize}";
        }
    }
}
