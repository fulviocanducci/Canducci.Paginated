using Canducci.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//https://github.com/aspnet/Mvc/tree/dev/src/Microsoft.AspNetCore.Mvc.TagHelpers
namespace Canducci.Pagination.Mvc
{
    [HtmlTargetElement("pagination", Attributes = ActionAttributeName)]
    [HtmlTargetElement("pagination", Attributes = ControllerAttributeName)]
    [HtmlTargetElement("pagination", Attributes = AreaAttributeName)]
    [HtmlTargetElement("pagination", Attributes = PageAttributeName)]
    [HtmlTargetElement("pagination", Attributes = PageHandlerAttributeName)]
    [HtmlTargetElement("pagination", Attributes = FragmentAttributeName)]
    [HtmlTargetElement("pagination", Attributes = HostAttributeName)]
    [HtmlTargetElement("pagination", Attributes = ProtocolAttributeName)]
    [HtmlTargetElement("pagination", Attributes = RouteAttributeName)]
    [HtmlTargetElement("pagination", Attributes = RouteValuesDictionaryName)]
    [HtmlTargetElement("pagination", Attributes = RouteValuesPrefix + "*")]
    [HtmlTargetElement("pagination", Attributes = PaginationPaginatedStyle)]
    [HtmlTargetElement("pagination", Attributes = PaginationCssClassUl)]
    [HtmlTargetElement("pagination", Attributes = PaginationPaginated)]
    [HtmlTargetElement("pagination", Attributes = PaginationLabelNext)]
    [HtmlTargetElement("pagination", Attributes = PaginationLabelPrevious)]
    [HtmlTargetElement("pagination", Attributes = PaginationCssClassAnchor)]
    [HtmlTargetElement("pagination", Attributes = PaginationCssClassLi)]
    [HtmlTargetElement("pagination", Attributes = PaginationCssClassLiDisabled)]
    [HtmlTargetElement("pagination", Attributes = PaginationLabelFirst)]
    [HtmlTargetElement("pagination", Attributes = PaginationLabelLast)]
    public class PaginatedTagHelper: TagHelper
    {
        #region private asp_tag_name
        private const string ActionAttributeName = "pagination-asp-action";
        private const string ControllerAttributeName = "pagination-asp-controller";
        private const string AreaAttributeName = "pagination-asp-area";
        private const string PageAttributeName = "pagination-asp-page";
        private const string PageHandlerAttributeName = "pagination-asp-page-handler";
        private const string FragmentAttributeName = "pagination-asp-fragment";
        private const string HostAttributeName = "pagination-asp-host";
        private const string ProtocolAttributeName = "pagination-asp-protocol";
        private const string RouteAttributeName = "pagination-asp-route";
        private const string RouteValuesDictionaryName = "pagination-asp-all-route-data";
        private const string RouteValuesPrefix = "pagination-asp-route-";
        private const string Href = "href";
        private IDictionary<string, string> _routeValues;
        #endregion

        #region pagination_tag_name
        private const string PaginationPaginatedStyle = "pagination-style";
        private const string PaginationCssClassUl = "pagination-css-class-ul";
        private const string PaginationCssClassLi = "pagination-css-class-li";
        private const string PaginationCssClassLiDisabled = "pagination-css-class-li-disabled";
        private const string PaginationPaginated = "pagination-paginated";
        private const string PaginationLabelNext = "pagination-label-next";
        private const string PaginationLabelPrevious = "pagination-label-previous";
        private const string PaginationCssClassAnchor = "pagination-css-class-anchor";
        private const string PaginationLabelFirst = "pagination-label-first";
        private const string PaginationLabelLast = "pagination-label-last";
        #endregion

        #region pagination_propriety
        [HtmlAttributeName(PaginationCssClassUl)]
        public string CssClassUl { get; set; }

        [HtmlAttributeName(PaginationCssClassLi)]
        public string CssClassLi { get; set; }

        [HtmlAttributeName(PaginationCssClassLiDisabled)]
        public string CssClassLiDisabled { get; set; }

        [HtmlAttributeName(PaginationCssClassAnchor)]
        public string CssClassAchor { get; set; }

        [HtmlAttributeName(PaginationPaginatedStyle)]
        public PaginatedStyle PaginatedStyle { get; set; } = PaginatedStyle.NumbersWithFirstPreviousNextLast;

        [HtmlAttributeName(PaginationPaginated)]
        public IPaginated Paginated { get; set; }

        [HtmlAttributeName(PaginationLabelNext)]
        public string LabelNext { get; set; } = "Next";

        [HtmlAttributeName(PaginationLabelPrevious)]
        public string LabelPrevious { get; set; } = "Previous";

        [HtmlAttributeName(PaginationLabelFirst)]
        public string LabelFirst { get; set; } = "First";

        [HtmlAttributeName(PaginationLabelLast)]
        public string LabelLast { get; set; } = "Last";
        #endregion

        #region private asp_propriety

        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        [HtmlAttributeName(AreaAttributeName)]
        public string Area { get; set; }

        [HtmlAttributeName(PageAttributeName)]
        public string Page { get; set; }

        [HtmlAttributeName(PageHandlerAttributeName)]
        public string PageHandler { get; set; }

        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }

        [HtmlAttributeName(HostAttributeName)]
        public string Host { get; set; }

        [HtmlAttributeName(FragmentAttributeName)]
        public string Fragment { get; set; }

        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get; set; }
        
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }
        #endregion

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public PaginatedTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        } 
        
        protected TagBuilder CreateTagBuilder(TagHelperOutput output, int page, string linkText, string cssClass = null)
        {  
            
            if (output.Attributes.ContainsName(Href))
            {
                if (Action != null ||
                    Controller != null ||
                    Area != null ||
                    Page != null ||
                    PageHandler != null ||
                    Route != null ||
                    Protocol != null ||
                    Host != null ||
                    Fragment != null ||
                    (_routeValues != null && _routeValues.Count > 0))
                {                    
                    throw new InvalidOperationException("User specified an href and one of the bound attributes; can't determine the href attribute.");
                }

                return null;
            }

            var routeLink = Route != null;
            var actionLink = Controller != null || Action != null;
            var pageLink = Page != null || PageHandler != null;

            if ((routeLink && actionLink) || (routeLink && pageLink) || (actionLink && pageLink))
            {   
                throw new InvalidOperationException("User specified an Action and RouteLink or RouteLink and PageLink or ActionLink and PageLink ");
            }

            RouteValueDictionary routeValues = null;
            if (_routeValues != null && _routeValues.Count > 0)
            {
                routeValues = new RouteValueDictionary(_routeValues);
            }
            else
            {
                routeValues = new RouteValueDictionary();
            }

            routeValues.Add("page", page);           

            if (Area != null)
            {                
                if (routeValues == null)
                {
                    routeValues = new RouteValueDictionary();
                }
                routeValues["area"] = Area;
            }

            TagBuilder tagBuilder;

#if NETSTANDARD2_0
            if (pageLink)
            {
                tagBuilder = Generator.GeneratePageLink(
                    ViewContext,
                    linkText: linkText,
                    pageName: Page,
                    pageHandler: PageHandler,
                    protocol: Protocol,
                    hostname: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: null);
            }
#endif
            if (routeLink)
            {
                tagBuilder = Generator.GenerateRouteLink(
                    ViewContext,
                    linkText: linkText,
                    routeName: Route,
                    protocol: Protocol,
                    hostName: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: null);
            }
            else
            {
                tagBuilder = Generator.GenerateActionLink(
                   ViewContext,
                   linkText: linkText,
                   actionName: Action,
                   controllerName: Controller,
                   protocol: Protocol,
                   hostname: Host,
                   fragment: Fragment,
                   routeValues: routeValues,
                   htmlAttributes: null);
            }
            if (cssClass != null) tagBuilder.AddCssClass(cssClass);
            return tagBuilder;
        }

        protected void Render(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", CssClassUl);
            
            switch (PaginatedStyle)
            {
                case PaginatedStyle.FirstPreviousNextLast:
                    {
                        First(output);
                        Previous(output);
                        Next(output);
                        Last(output);
                        break;
                    }
                case PaginatedStyle.Numbers:
                    {
                        
                        break;
                    }
                case PaginatedStyle.NumbersWithFirstPreviousNextLast:
                    {
                        
                        break;
                    }
                case PaginatedStyle.NumbersWithPreviousNext:
                    {
                        
                        break;
                    }
                case PaginatedStyle.PreviousNext:
                    {
                        Previous(output);
                        Next(output);
                        break;
                    }
            }

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

        #region first
        internal TagHelperOutput First(TagHelperOutput output)
        {            
            TagBuilder tagBuilder = CreateTagBuilder(output, 1, LabelFirst, CssClassAchor);
            output.Content.AppendHtmlTaLi(tagBuilder, Paginated.IsFirstPage ? CssClassLiDisabled + " " + CssClassLi : CssClassLi);
            return output;
        }
        #endregion
        #region Last
        internal TagHelperOutput Last(TagHelperOutput output)
        {            
            TagBuilder tagBuilder = CreateTagBuilder(output, Paginated.PageCount, LabelLast, CssClassAchor);
            output.Content.AppendHtmlTaLi(tagBuilder, Paginated.IsLastPage ? CssClassLiDisabled + " " + CssClassLi : CssClassLi);
            return output;
        }
        #endregion
        #region previous
        internal TagHelperOutput Previous(TagHelperOutput output)
        {
            int page = Paginated.IsFirstPage ? 1 : Paginated.PageNumber - 1;
            TagBuilder tagBuilder = CreateTagBuilder(output, page, LabelPrevious, CssClassAchor);
            output.Content.AppendHtmlTaLi(tagBuilder, Paginated.IsFirstPage ? CssClassLiDisabled + " " + CssClassLi : CssClassLi);
            return output;
        }

        #endregion
        #region next
        private TagHelperOutput Next(TagHelperOutput output)
        {         
            int page = Paginated.IsLastPage ? Paginated.PageCount: Paginated.PageNumber + 1;
            TagBuilder tagBuilder = CreateTagBuilder(output, page, LabelNext, CssClassAchor);
            output.Content.AppendHtmlTaLi(tagBuilder, Paginated.IsLastPage ? CssClassLiDisabled + " " + CssClassLi : CssClassLi);
            return output;
        }
        #endregion
    }
}
