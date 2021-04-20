using NetCoreAPI_Template_v3_with_auth.DTOs;
using System.Linq;

namespace NetCoreAPI_Template_v3_with_auth.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto pagination)
        {
            return queryable.Skip((pagination.Page - 1) * pagination.RecordsPerPage).Take(pagination.RecordsPerPage);
        }
    }
}