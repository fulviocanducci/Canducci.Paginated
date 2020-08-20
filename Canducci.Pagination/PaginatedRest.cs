using Canducci.Pagination.Bases;
using Canducci.Pagination.Interfaces;

namespace Canducci.Pagination
{
    public sealed class PaginatedRest<T> : PaginatedRestBase<T>
    {
        internal PaginatedRest(IPaginated<T> source)
            : base(source)
        { }
    }
}
