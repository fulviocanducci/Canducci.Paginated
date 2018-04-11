using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Canducci.WebApp.Test.Models;
using Canducci.Pagination;
namespace Canducci.WebApp.Test.Controllers
{
    public class HomeController : Controller
    {
        public DatabaseContext Database { get; }

        public HomeController(DatabaseContext database)
        {
            Database = database;
        }
        public IActionResult Index(int? page)
        {
            var result = Database.People.OrderBy(x => x.Id).ToPaginated(page ?? 1, 3);
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
        public IActionResult Materialize(int? page)
        {
            var result = Database.People.OrderBy(x => x.Id).ToPaginated(page ?? 1, 3);
            return View(result);
        }
    }
}
