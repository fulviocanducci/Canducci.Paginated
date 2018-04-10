using System;
using System.Collections.Generic;
using System.Linq; 
namespace Canducci.Pagination
{
    public class Paginated<T> : Bases.PaginatedBase<T>, Interfaces.IPaginated<T>
    {
        public Paginated(IQueryable<T> superSet, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");
                        
            TotalItemCount = superSet == null ? 0 : superSet.Count();
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
            
            if (superSet != null && TotalItemCount > 0)
                AddRange(pageNumber == 1
                    ? superSet.Skip(0).Take(pageSize).ToList()
                    : superSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                );
        }

        public Paginated(IEnumerable<T> superSet, int pageNumber, int pageSize)
            : this(superSet.AsQueryable(), pageNumber, pageSize)
        {
        }
        
        public static implicit operator PaginatedMetaData(Paginated<T> source)
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
