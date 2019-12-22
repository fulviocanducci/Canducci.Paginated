using Canducci.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using WebMvc.Models;

namespace WebMvc.Controllers
{
   public class HomeController : Controller
   {
      private ILogger<HomeController> Logger { get; }
      public DatabaseContext Database { get; }

      public HomeController(DatabaseContext database, ILogger<HomeController> logger)
      {
         Database = database;
         Logger = logger;
      }
      public IActionResult Index(int? current)
      {
         var result = Database.People.OrderBy(x => x.Id).ToPaginated(current ?? 1, 1);
         return View(result);
      }

      public IActionResult About()
      {
         ViewData["Message"] = "Your application description page.";

         return View();
      }

      public IActionResult Contact()
      {
         ViewData["Message"] = "Your contact page.";

         return View();
      }

      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }

      [Route("materialize")]
      public IActionResult Materialize(int? current)
      {
         var result = Database.People.OrderBy(x => x.Id).ToPaginated(current ?? 1, 3);
         return View(result);
      }
   }
}
