using Canducci.Pagination.Interfaces;

namespace Canducci.Pagination
{
    public static class PaginatedExtensions
    {
        public static Paginated<T> ToPaginated<T>(this System.Collections.Generic.IEnumerable<T> subSet, int pageNumber, int pageSize)
        {
            return new Paginated<T>(subSet, pageNumber, pageSize);
        }

        public static Paginated<T> ToPaginated<T>(this System.Linq.IQueryable<T> subSet, int pageNumber, int pageSize)
        {
            return new Paginated<T>(subSet, pageNumber, pageSize);
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
                                   source.LastItemOnPage
                                   );
        }       
    }
}
