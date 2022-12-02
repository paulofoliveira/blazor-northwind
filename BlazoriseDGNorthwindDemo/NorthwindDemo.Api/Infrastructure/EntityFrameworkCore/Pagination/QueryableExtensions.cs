using System.Linq.Expressions;

// Read me "How to dynamically order by certain entity properties in Entity Framework 7 (Core)": https://stackoverflow.com/questions/36298868/how-to-dynamically-order-by-certain-entity-properties-in-entity-framework-7-cor

namespace BlazorNorthwind.Api.Infrastructure.EntityFrameworkCore.Pagination
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<PagedQueryColumn> sortColumns)
        {
            var expression = source.Expression;

            int count = 0;

            foreach (var item in sortColumns)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.Name);
                var method = string.Equals(item.Direction, "desc", StringComparison.OrdinalIgnoreCase) ? (count == 0 ? "OrderByDescending" : "ThenByDescending") : (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method, new Type[] { source.ElementType, selector.Type }, expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }

            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }
    }
}
