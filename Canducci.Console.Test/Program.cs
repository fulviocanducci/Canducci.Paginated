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
            int total = 5;
            //TestIEnumerableStaticPaginted(page, total);
            TestIQueryablePaginated(page, total);
        }

        static void TestIQueryablePaginated(int page, int total)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var listOfQueryable0 = db.People.OrderBy(x => x.Id)
                    .ToPaginated(page, total);

                var listOfQueryable1 = db.People.OrderBy(x => x.Id)
                    .ToPaginated((page + 1), total);
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

            var paginated0 = new StaticPaginated<People>(listOfPeople0.ToArray(), page, total, countOfPeople);

            page = 2;
            IEnumerable<People> listOfPeople1 = listOfAllPeople
                .OrderBy(x => x.Id)
                .Skip((page - 1) * total)
                .Take(total)
                .ToArray();

            var paginated1 = new StaticPaginated<People>(listOfPeople1.ToArray(), page, total, countOfPeople);
        }
    }
}
