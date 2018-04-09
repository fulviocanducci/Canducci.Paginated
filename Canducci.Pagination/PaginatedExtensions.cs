using System.Collections.Generic;
using System.Linq;
namespace Canducci.Pagination
{
    public static class PaginatedExtensions
    {
        public static IPaginated<T> ToPaginated<T>(this IEnumerable<T> subSet, int pageNumber, int pageSize)
        {
            return new Paginated<T>(subSet, pageNumber, pageSize);
        }
        public static IPaginated<T> ToPaginated<T>(this IQueryable<T> subSet, int pageNumber, int pageSize)
        {
            return new Paginated<T>(subSet, pageNumber, pageSize);
        }
    }
}
