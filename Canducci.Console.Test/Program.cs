using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canducci.Pagination;
using Canducci.Pagination.Interfaces;
using Canducci.Pagination.MetaData;
using Microsoft.EntityFrameworkCore;

namespace Canducci.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {           
            int page = 1; 
            int total = 2;            
            TestIQueryablePaginated(page, total);
            TestIEnumerableStaticPaginated(page, total);
        }

        static void TestIQueryablePaginated(int page, int total = 5)
        {
            using (DatabaseContext db = new DatabaseContext())
            {                
                var count = db.People.Count();
                Paginated<People> listOfQueryable0 = db.People
                    .OrderBy(x => x.Name)
                    .ToPaginated(page, total);

                Paginated<People> listOfQueryable1 = db.People
                    .OrderBy(x => x.Name)
                    .ToPaginated((page + 1), total);

                PaginatedMetaData am0 = listOfQueryable0.ToPaginatedMetaData();
                PaginatedMetaData am1 = listOfQueryable1.ToPaginatedMetaData();

                int[] ap0 = listOfQueryable0.Pages.ToArray();                
                int[] ap1 = listOfQueryable1.Pages.ToArray();

                listOfQueryable0.SetPages(1);
                int[] ap3 = listOfQueryable0.Pages.ToArray();

                listOfQueryable1.SetPages(1);
                int[] ap4 = listOfQueryable1.Pages.ToArray();

            }
        }
        static void TestIEnumerableStaticPaginated(int page, int total = 5)
        {
            PeopleList listOfAllPeople = new PeopleList();
            int countOfPeople = listOfAllPeople.Count;

            IEnumerable<People> listOfPeople0 = listOfAllPeople
                .OrderBy(x => x.Id)
                .Skip((page - 1) * total)
                .Take(total)
                .ToArray();

            StaticPaginated<People> paginated0 = new StaticPaginated<People>(listOfPeople0, page, total, countOfPeople);

            page += 1;
            IEnumerable<People> listOfPeople1 = listOfAllPeople
                .OrderBy(x => x.Id)
                .Skip((page) * total)
                .Take(total)
                .ToArray();

            StaticPaginated<People> paginated1 = new StaticPaginated<People>(listOfPeople1.ToArray(), page, total, countOfPeople);

            PaginatedMetaData bm0 = paginated0.ToPaginatedMetaData();
            PaginatedMetaData bm1 = paginated1.ToPaginatedMetaData();

            var ap0 = paginated0.Pages;
            var ap1 = paginated1.Pages;

        }
    }
}
