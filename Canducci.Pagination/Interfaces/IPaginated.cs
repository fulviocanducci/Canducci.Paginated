using System;
using System.Collections;
using System.Collections.Generic;
namespace Canducci.Pagination.Interfaces
{
    public interface IPaginated: IPaginatedRest
    {        
        SortedSet<int> Pages { get; }
        void SetPages(int count = 8);
        int MaximumPageNumbersToDisplay { get; }
    }

    public interface IPaginated<T> : IEnumerable<T>, IList<T>, IEnumerable, IList, IPaginated { }
}
