using Canducci.Pagination.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
//https://github.com/netdragoon/helpWebForms/blob/master/Canducci.HtmlHelpers/MethodsHelpers.cs
namespace Canducci.Pagination.Mvc
{
    public static class HtmlHelpers
    {
        #region get_value
        internal static object GetValue<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            where TModel : class
        {
            TModel model = htmlHelper.ViewData.Model;
            if (model == null)
            {
                return default(TProperty);
            }
            Func<TModel, TProperty> func = expression.Compile();
            return func(model);
        }
        #endregion

        #region internals
        internal static string ToStringContent(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }       

        internal static IHtmlContentBuilder AppendHtml(this IHtmlContentBuilder content, TagBuilder tag, string cssClass = null)
        {
            TagBuilder li = new TagBuilder("li")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            if (!string.IsNullOrEmpty(cssClass)) li.AddCssClass(cssClass);
            li.InnerHtml.AppendHtml(tag);            
            content.AppendHtml(li);
            content.AppendHtmlLine(Environment.NewLine);
            return content;
        }

        internal static TagBuilder TagLink(string url, string label, string cssClass = null)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.Attributes.Add("href", url);
            if (!string.IsNullOrEmpty(cssClass)) tag.Attributes.Add("class", cssClass);            
            tag.InnerHtml.Append(label);            
            return tag;
        }

        internal static HtmlContentBuilder CreateHtmlContentBuilder(PaginatedOptions options)
        {            
            HtmlContentBuilder content = new HtmlContentBuilder();
            content.AppendHtml($"<ul class=\"{options.CssClassUl}\">");
            content.AppendHtmlLine(Environment.NewLine);
            return content;
        }

        #endregion

        #region first_previous_next_last
        internal static HtmlContentBuilder First<T>(HtmlContentBuilder content, IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            TagBuilder tag = TagLink(paginated.IsFirstPage ? "#" : generatePageUrl(1), options.FirstLabel, options.CssClassAnchor);
            content.AppendHtml(tag, paginated.IsFirstPage ? options.CssClassLiDisabled : null);
            return content;
        }
        internal static HtmlContentBuilder Previous<T>(HtmlContentBuilder content, IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            TagBuilder tag = TagLink(paginated.IsFirstPage ? "#" : generatePageUrl(paginated.PageNumber - 1), options.PreviousLabel, options.CssClassAnchor);
            content.AppendHtml(tag, paginated.IsFirstPage ? options.CssClassLiDisabled : null);
            return content;            
        }
        internal static HtmlContentBuilder Next<T>(HtmlContentBuilder content, IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            TagBuilder tag = TagLink(paginated.IsLastPage ? "#": generatePageUrl(paginated.PageNumber + 1), options.NextLabel, options.CssClassAnchor);
            content.AppendHtml(tag, paginated.IsLastPage ? options.CssClassLiDisabled : null);
            return content;
        }
        internal static HtmlContentBuilder Last<T>(HtmlContentBuilder content, IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            TagBuilder tag = TagLink(paginated.IsLastPage ? "#" : generatePageUrl(paginated.PageCount), options.LastLabel, options.CssClassAnchor);
            content.AppendHtml(tag, paginated.IsLastPage ? options.CssClassLiDisabled : null);
            return content;
        }
        #endregion

        #region page_numbers_with_first_previous_next_last
        internal static IHtmlContent PaginationNumbersWithFirstPreviousNextLast<T>(IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            First(content, paginated, generatePageUrl, options);
            Previous(content, paginated, generatePageUrl, options);
            Numbers(content, paginated, generatePageUrl, options);
            Next(content, paginated, generatePageUrl, options);
            Last(content, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

        #region page_numbers_with_previous_next
        internal static IHtmlContent PaginationNumbersWithPreviousNext<T>(IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            Previous(content, paginated, generatePageUrl, options);
            Numbers(content, paginated, generatePageUrl, options);
            Next(content, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

        #region page_first_previous_next_last
        internal static IHtmlContent PaginationFirstPreviousNextLast<T>(IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            First(content, paginated, generatePageUrl, options);
            Previous(content, paginated, generatePageUrl, options);
            Next(content, paginated, generatePageUrl, options);
            Last(content, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

        #region page_previous_next
        internal static IHtmlContent PaginationPreviousNext<T>(IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            Previous(content, paginated, generatePageUrl, options);
            Next(content, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

        #region page_numbers
        internal static HtmlContentBuilder Numbers<T>(HtmlContentBuilder content, IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            if (options.MaximumPageNumbersToDisplay != 8)
                paginated.SetPages(options.MaximumPageNumbersToDisplay);
            paginated
                .Pages
                .ToList()
                .ForEach(x =>
                {
                    var tagLink = TagLink((paginated.PageNumber != x) ? generatePageUrl(x) : "#", x.ToString(), options.CssClassAnchor);
                    content.AppendHtml(tagLink, (paginated.PageNumber == x) 
                        ? options.CssClassLiActive + " " + options.CssClassLi
                        : options.CssClassLi);
                });           
            return content;
        }

        internal static IHtmlContent PaginationNumbers<T>(IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            Numbers(content, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

        #region Pagination_Web_Page
        public static IHtmlContent Pagination<T>(this IHtmlHelper htmlHelper, IPaginated<T> paginated, Func<int, string> generatePageUrl, PaginatedStyle paginatedStyle = PaginatedStyle.NumbersWithFirstPreviousNextLast, PaginatedOptions options = default(PaginatedOptions))
        {
            IHtmlContent content = null;
            switch (paginatedStyle) 
            {
                case PaginatedStyle.FirstPreviousNextLast: 
                    {
                        content = PaginationFirstPreviousNextLast(paginated, generatePageUrl, options);
                        break;
                    }
                case PaginatedStyle.Numbers:
                    {
                        content = PaginationNumbers(paginated, generatePageUrl, options);
                        break;
                    }
                case PaginatedStyle.NumbersWithFirstPreviousNextLast:
                    {
                        content = PaginationNumbersWithFirstPreviousNextLast(paginated, generatePageUrl, options);
                        break;
                    }
                case PaginatedStyle.NumbersWithPreviousNext:
                    {
                        content = PaginationNumbersWithPreviousNext(paginated, generatePageUrl, options);
                        break;
                    }
                case PaginatedStyle.PreviousNext:
                    {
                        content = PaginationPreviousNext(paginated, generatePageUrl, options);
                        break;
                    }
            }
            return content;
        }
        #endregion
    }
}
