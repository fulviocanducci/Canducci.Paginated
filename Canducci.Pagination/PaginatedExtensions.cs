using Canducci.Pagination.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Canducci.Pagination
{
    public static class PaginatedExtensions
    {
        public static Paginated<T> ToPaginated<T>(this IEnumerable<T> subSet, int pageNumber, int pageSize)
        {
            return new Paginated<T>(subSet, pageNumber, pageSize);
        }

        public static Paginated<T> ToPaginated<T>(this IQueryable<T> subSet, int pageNumber, int pageSize)
        {
            return new Paginated<T>(subSet, pageNumber, pageSize);
        }

        public static Task<Paginated<T>> ToPaginatedAsync<T>(this IEnumerable<T> subSet, int pageNumber, int pageSize)
        {
            return Task.Run(() => new Paginated<T>(subSet, pageNumber, pageSize));
        }

        public static Task<Paginated<T>> ToPaginatedAsync<T>(this IQueryable<T> subSet, int pageNumber, int pageSize)
        {
            return Task.Run(() => new Paginated<T>(subSet, pageNumber, pageSize));
        }        

        public static PaginatedMetaData ToPaginatedMetaData(this IPaginated source)
        {
            return new PaginatedMetaData(
                                   source.PageCount,
                                   source.TotalItemCount,
                                   source.PageNumber,
                                   source.PageSize,
                                   source.HasPreviousPage,
                                   source.HasNextPage,
                                   source.IsFirstPage,
                                   source.IsLastPage,
                                   source.FirstItemOnPage,
                                   source.LastItemOnPage,
                                   source.Pages,
                                   source.MaximumPageNumbersToDisplay);
        }       
    }
}
