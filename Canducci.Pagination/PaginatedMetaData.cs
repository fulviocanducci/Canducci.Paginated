using System.Collections.Generic;

namespace Canducci.Pagination
{
    public struct PaginatedMetaData
    {
        internal PaginatedMetaData(
            int pageCount,
            int totalItemCount,
            int pageNumber,
            int pageSize,
            bool hasPreviousPage,
            bool hasNextPage,
            bool isFirstPage,
            bool isLastPage,
            int firstItemOnPage,
            int lastItemOnPage,
            SortedSet<int> pages)
        {
            PageCount = pageCount;
            TotalItemCount = totalItemCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            HasPreviousPage = hasPreviousPage;
            HasNextPage = hasNextPage;
            IsFirstPage = isFirstPage;
            IsLastPage = isLastPage;
            FirstItemOnPage = firstItemOnPage;
            LastItemOnPage = lastItemOnPage;
            Pages = pages;
        }
        public int PageCount { get; }
        public int TotalItemCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
        public bool IsFirstPage { get; }        
        public bool IsLastPage { get; }
        public int FirstItemOnPage { get; }
        public int LastItemOnPage { get; }
        public SortedSet<int> Pages { get; }
    }
}
