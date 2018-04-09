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
    }
}
