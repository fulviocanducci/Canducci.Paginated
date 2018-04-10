using System.Collections.Generic;
namespace Canducci.Pagination.Bases
{
    public abstract class PaginatedBase<T> : List<T>, IList<T>
    {
        public int PageCount { get; protected set; }
        public int TotalItemCount { get; protected set; }
        public int PageNumber { get; protected set; }
        public int PageSize { get; protected set; }
        public bool HasPreviousPage { get; protected set; }
        public bool HasNextPage { get; protected set; }
        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }
        public int FirstItemOnPage { get; protected set; }
        public int LastItemOnPage { get; protected set; }

        internal PaginatedBase() { }
        public PaginatedBase(IEnumerable<T> subSet)
            : base(subSet)
        {
        }

        public PaginatedMetaData ToPaginatedMetaData() 
            => new PaginatedMetaData(
                               PageCount,
                               TotalItemCount,
                               PageNumber,
                               PageSize,
                               HasPreviousPage,
                               HasNextPage,
                               IsFirstPage,
                               IsLastPage,
                               FirstItemOnPage,
                               LastItemOnPage
                               );
    }    
}
