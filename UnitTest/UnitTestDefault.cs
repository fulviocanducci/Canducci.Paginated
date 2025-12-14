using Canducci.Pagination;
using Canducci.Pagination.Bases;
using Canducci.Pagination.Interfaces;
using Canducci.Pagination.MetaData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitTest.Models;

namespace UnitTest
{
   [TestClass]
   public class UnitTestDefault
   {
      private DatabaseContext Database { get; set; }
      
      [TestInitialize]
      public void UnitDefaultInit()
      {
         Database = new DatabaseContext();
      }

      public IEnumerable<int> GetNumbers()
      {
         for (int i = 0; i < 100; i++)
            yield return i;
      }
      [TestMethod]
      public void TestPaginatedWithIQuerable()
      {
         int pageNumber = 1;
         int pageSize = 2;

         Paginated<People> st = Database.People.ToPaginated(pageNumber, pageSize);


         PaginatedMetaData metaData = st.ToPaginatedMetaData();

         Assert.IsInstanceOfType(st, typeof(Paginated<People>));
         Assert.IsInstanceOfType(st, typeof(IPaginated));
         Assert.IsInstanceOfType(st, typeof(IPaginated<People>));
         Assert.IsInstanceOfType(st, typeof(IList));
         Assert.IsInstanceOfType(st, typeof(IList<People>));
         Assert.IsInstanceOfType(st, typeof(List<People>));
         Assert.IsInstanceOfType(st, typeof(PaginatedBase<People>));
         Assert.IsInstanceOfType(metaData, typeof(PaginatedMetaData));
         Assert.AreEqual(1, metaData.FirstItemOnPage);
         Assert.IsTrue(metaData.HasNextPage);
         Assert.IsFalse(metaData.HasPreviousPage);
         Assert.IsTrue(metaData.IsFirstPage);
         Assert.IsFalse(metaData.IsLastPage);
         Assert.AreEqual(2, metaData.LastItemOnPage);
         Assert.AreEqual(8, metaData.MaximumPageNumbersToDisplay);
         Assert.AreEqual(5, metaData.PageCount);
         //Assert.AreEqual(5, metaData.Pages.Count);
         Assert.AreEqual(1, metaData.PageNumber);
         Assert.AreEqual(2, metaData.PageSize);
         Assert.AreEqual(metaData.TotalItemCount, st.TotalItemCount);
      }

      [TestMethod]
      public void TestStaticPaginated()
      {
         List<int> numbers = GetNumbers().ToList();
         int pageNumber = 1;
         int pageSize = 10;

         StaticPaginated<int> st =
             new StaticPaginated<int>(
                 numbers.Skip(pageNumber).Take(pageSize),
                 pageNumber,
                 pageSize,
                 numbers.Count);

         PaginatedMetaData metaData = st.ToPaginatedMetaData();

         Assert.IsInstanceOfType(st, typeof(StaticPaginated<int>));
         Assert.IsInstanceOfType(st, typeof(IPaginated));
         Assert.IsInstanceOfType(st, typeof(IPaginated<int>));
         Assert.IsInstanceOfType(st, typeof(IList));
         Assert.IsInstanceOfType(st, typeof(IList<int>));
         Assert.IsInstanceOfType(st, typeof(List<int>));
         Assert.IsInstanceOfType(st, typeof(PaginatedBase<int>));
         Assert.IsInstanceOfType(metaData, typeof(PaginatedMetaData));
         Assert.AreEqual(1, metaData.FirstItemOnPage);
         Assert.IsTrue(metaData.HasNextPage);
         Assert.IsFalse(metaData.HasPreviousPage);
         Assert.IsTrue(metaData.IsFirstPage);
         Assert.IsFalse(metaData.IsLastPage);
         Assert.AreEqual(10, metaData.LastItemOnPage);
         Assert.AreEqual(8, metaData.MaximumPageNumbersToDisplay);
         Assert.AreEqual(10, metaData.PageCount );
         //Assert.AreEqual(5, metaData.Pages.Countt, 5);
         Assert.AreEqual(1, metaData.PageNumber);
         Assert.AreEqual(10, metaData.PageSize);
         //Assert.AreEqual(metaData.TotalItemCount, numbers.Count);
      }
   }
}