using Canducci.Pagination.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Canducci.Pagination.Mvc
{
    [HtmlTargetElement("pagination", Attributes = CPageStyle)]
    [HtmlTargetElement("pagination", Attributes = CPageOptions)]
    [HtmlTargetElement("pagination", Attributes = CPagePaginated)]
    public class PaginatedTagHelper: TagHelper
    {
        protected const string CPageStyle = "page-style";
        protected const string CPageOptions = "page-options";
        protected const string CPagePaginated = "page-paginated";

        [HtmlAttributeName(CPageStyle)]
        public PaginatedStyle PaginatedStyle { get; set; } = PaginatedStyle.NumbersWithFirstPreviousNextLast;

        [HtmlAttributeName(CPageOptions)]
        public PaginatedOptions PaginatedOptions { get; set; }

        //[HtmlAttributeName(CPagePaginated)]
        //public IPaginated<T> Paginated { get; set; }

        protected void Render(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", PaginatedOptions.CssClassUl);
            //output.Content.Append(HtmlHelpers.PaginationNumbersWithPreviousNext())
           
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Render(context, output);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            if (childContent.IsEmptyOrWhiteSpace)
            {
                Render(context, output);
            }
        }
    }
}
