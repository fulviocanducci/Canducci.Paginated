using System.Linq;
using Canducci.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Models;

namespace WebRazor.Pages
{
    public class PeoplesModel(DatabaseContext context) : PageModel
    {
        private readonly DatabaseContext Context = context;

        public Paginated<People> Items { get; private set; }
        
        public void OnGet(int? current)
        {            
            Items = Context.People
                .OrderBy(x => x.Name)
                    .ThenBy(x => x.Id)
                .ToPaginated(current ?? 1, 2);
        }
    }
}