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
        public SortedSet<int> Pages { get; protected set; }
        public void SetPages(int count = 3)
        {
            if (Pages == null)
            {
                Pages = new SortedSet<int>();
            }
            else
            {
                if (Pages.Count > 0)
                {
                    Pages.Clear();
                }
            }
            if (count <= 0)
            {
                throw new System.RankException("The count value has to be greater than 0 ideal is the number 3 or higher");
            }
            int interaction = count;
            int pageNumber = PageNumber;
            while (interaction >= 0 && pageNumber > 0)
            {
                Pages.Add(pageNumber--);
                interaction--;
            }
            interaction = 0;
            pageNumber = PageNumber;
            while (interaction < count && pageNumber < PageCount)
            {
                Pages.Add(++pageNumber);
                interaction++;
            }
        }

        internal PaginatedBase() { }
        public PaginatedBase(IEnumerable<T> subSet)
            : base(subSet) { }
    }    
}
