namespace Canducci.Pagination.Mvc
{
    public class PaginatedOptions
    {
        public string CssClassLi { get; set; } = "page-item";
        public string CssClassLiDisabled { get; set; } = "disabled";
        public string CssClassUl { get; set; } = "pagination";
        public string CssClassLiActive { get; set; } = "active";
        public string CssClassLink { get; set; } = "page-link";

        public string PreviousLabel { get; set; } = "Previous";
        public string NextLabel { get; set; } = "Next";
    }
}
