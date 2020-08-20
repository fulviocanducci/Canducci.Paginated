using Canducci.Pagination.Bases;
using Canducci.Pagination.Interfaces;
using System.Collections.Generic;

namespace Canducci.Pagination
{
    public sealed class StaticPaginatedRest<T> : PaginatedRestBase<T>
    {
        internal StaticPaginatedRest(IStaticPaginated<T> source)
            : base(source)
        { }

        public StaticPaginatedRest(IEnumerable<T> subSet, int pageNumber, int pageSize, int totalItemCount)
            : this(new StaticPaginated<T>(subSet, pageNumber, pageSize, totalItemCount))
        { }
    }
}
