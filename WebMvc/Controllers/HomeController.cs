using Canducci.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [Route("rest/page/{page?}")]
        public JsonResult IndexRest(int? page)
        {
            var result = Database.People.OrderBy(x => x.Id).ToPaginatedRest(page ?? 1, 3);
            return Json(result);
        }

        [Route("static/page/{page?}")]
        public JsonResult IndexStaticRest(int? page)
        {
            int pageSize = 4;

            page ??= 1;

            int pageNumber = page.Value;

            IQueryable<People> data  = Database.People.OrderBy(x => x.Id);

            int totalItemCount = data.Count();

            var subSet = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();
            
            var result = new StaticPaginatedRest<People>(subSet, pageNumber, pageSize, totalItemCount);

            return Json(result);
        }

        [Route("materialize")]
        public IActionResult Materialize(int? current)
        {
            var result = Database.People.OrderBy(x => x.Id).ToPaginated(current ?? 1, 3);
            return View(result);
        }
    }
}
