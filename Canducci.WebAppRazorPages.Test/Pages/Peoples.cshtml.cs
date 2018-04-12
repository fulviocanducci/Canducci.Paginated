using System.Linq;
using Canducci.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Canducci.WebAppRazorPages.Test.Pages
{
    public class PeoplesModel : PageModel
    {
        private readonly DatabaseContext Context;
        public PeoplesModel(DatabaseContext context)
        {
            Context = context;            
        }

        public Paginated<People> Items { get; private set; }
        
        public void OnGet(int? current)
        {            
            Items = Context.People
                .OrderBy(x => x.Name)
                .OrderBy(x => x.Id)
                .ToPaginated(current ?? 1, 4);
        }
    }
}