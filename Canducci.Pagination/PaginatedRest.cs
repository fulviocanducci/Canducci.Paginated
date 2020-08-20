using Canducci.Pagination.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.Pagination
{
    public sealed class PaginatedRest<T> : IPaginatedRest
    {
        internal PaginatedRest(Paginated<T> source)
        {
            PageCount = source.PageCount;
            TotalItemCount = source.TotalItemCount;
            PageNumber = source.PageNumber;
            PageSize = source.PageSize;
            HasPreviousPage = source.HasPreviousPage;
            HasNextPage = source.HasNextPage;
            IsFirstPage = source.IsFirstPage;
            IsLastPage = source.IsLastPage;
            FirstItemOnPage = source.FirstItemOnPage;
            LastItemOnPage = source.LastItemOnPage;
            Items = source.AsEnumerable();
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
        public IEnumerable<T> Items { get; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
