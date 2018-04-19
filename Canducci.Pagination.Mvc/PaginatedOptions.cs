namespace Canducci.Pagination.Mvc
{
    public class PaginatedOptions
    {
        public string CssClassLi { get; set; } = "page-item";
        public string CssClassLiDisabled { get; set; } = "disabled";
        public string CssClassUl { get; set; } = "pagination";
        public string CssClassLiActive { get; set; } = "active";
        public string CssClassAnchor { get; set; } = "page-link";

        public string FirstLabel { get; set; } = "««";
        public string PreviousLabel { get; set; } = "«";
        public string NextLabel { get; set; } = "»";
        public string LastLabel { get; set; } = "»»";

        public int MaximumPageNumbersToDisplay { get; set; } = 8;

    }
}
