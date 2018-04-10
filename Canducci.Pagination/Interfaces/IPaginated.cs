using System;
using System.Collections;
using System.Collections.Generic;
namespace Canducci.Pagination.Interfaces
{
    public interface IPaginated<T> : IEnumerable<T>, IList<T>, IEnumerable, IList, IDisposable
    {
        int PageCount { get; }
        int TotalItemCount { get;  }
        int PageNumber { get;  }
        int PageSize { get;  }
        bool HasPreviousPage { get;  }
        bool HasNextPage { get;  }
        bool IsFirstPage { get;  }
        bool IsLastPage { get;  }
        int FirstItemOnPage { get;  }
        int LastItemOnPage { get; }
        PaginatedMetaData ToPaginatedMetaData();
    }
}
