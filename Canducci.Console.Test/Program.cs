using System;
using System.Collections.Generic;
using System.Linq;
using Canducci.Pagination;
namespace Canducci.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int page = 1; 
            int total = 3;
            //TestIEnumerableStaticPaginted(page, total);
            TestIQueryablePaginated(page, total);
            TestIEnumerableStaticPaginated(page, total);
        }

        static void TestIQueryablePaginated(int page, int total)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                Paginated<People> listOfQueryable0 = db.People
                    .OrderBy(x => x.Name)
                    .ToPaginated(page, total);
                
                PaginatedMetaData a0 = listOfQueryable0;

                Paginated<People> listOfQueryable1 = db.People
                    .OrderBy(x => x.Name)
                    .ToPaginated((page + 1), total);

                PaginatedMetaData a1 = listOfQueryable1;
            }
        }
        static void TestIEnumerableStaticPaginated(int page, int total)
        {
            PeopleList listOfAllPeople = new PeopleList();
            int countOfPeople = listOfAllPeople.Count;

            IEnumerable<People> listOfPeople0 = listOfAllPeople
                .OrderBy(x => x.Id)
                .Skip((page - 1) * total)
                .Take(total)
                .ToArray();

            StaticPaginated<People> paginated0 = new StaticPaginated<People>(listOfPeople0.ToArray(), page, total, countOfPeople);
            PaginatedMetaData b0 = paginated0;

            page = 2;
            IEnumerable<People> listOfPeople1 = listOfAllPeople
                .OrderBy(x => x.Id)
                .Skip((page - 1) * total)
                .Take(total)
                .ToArray();

            StaticPaginated<People> paginated1 = new StaticPaginated<People>(listOfPeople1.ToArray(), page, total, countOfPeople);
            PaginatedMetaData b1 = paginated1;
        }
    }
}
