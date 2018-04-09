using System;
using System.Collections.Generic;
namespace Canducci.Pagination
{
    public interface IStaticPaginated<T>: IPaginated<T>, IList<T>, IDisposable
    {        
    }
}
