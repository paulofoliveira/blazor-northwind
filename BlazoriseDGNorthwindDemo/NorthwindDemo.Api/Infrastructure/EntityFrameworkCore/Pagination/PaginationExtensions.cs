using Microsoft.EntityFrameworkCore;
using BlazorNorthwind.Infrastructure.Shared.Pagination;

namespace BlazorNorthwind.Api.Infrastructure.EntityFrameworkCore.Pagination
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query, PagedQuery pagedQuery) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pagedQuery.PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            result.Items = await query.OrderBy(pagedQuery.SortColumns).Skip(pagedQuery.Page).Take(pagedQuery.PageSize).ToListAsync();

            return result;
        }

        public static PagedQuery ToPagedQuery(this PagedQueryModel pagedQueryModel)
        {
            return new()
            {
                Page = pagedQueryModel.Page,
                PageSize = pagedQueryModel.PageSize,
                SortColumns = pagedQueryModel.SortColumns.Select(x => new PagedQueryColumn()
                {
                    Name = x.Name,
                    Direction = x.Direction
                })
            };
        }
    }
}
