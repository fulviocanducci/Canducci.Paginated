using Canducci.Pagination.Bases;
using Canducci.Pagination.Interfaces;
using System;
using System.Collections.Generic;
namespace Canducci.Pagination
{
    public sealed class StaticPaginated<T> : PaginatedBase<T>, IStaticPaginated<T>
    {        
        public StaticPaginated(IEnumerable<T> subSet, int pageNumber, int pageSize, int totalItemCount)
            :base(subSet)
        {            
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");
                        
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            int numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount ? TotalItemCount : numberOfLastItemOnPage;
            SetPages();
        } 

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
