using Canducci.Pagination.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
//https://github.com/netdragoon/helpWebForms/blob/master/Canducci.HtmlHelpers/MethodsHelpers.cs
namespace Canducci.Pagination.Mvc
{
    public static class HtmlHelpers
    {
        #region getValue
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

        #region stringContent
        public static string ToStringContent(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
        #endregion

        internal static IHtmlContentBuilder AppendHtmlTaLi(this IHtmlContentBuilder content, TagBuilder tag, string cssClass = null)
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
            if (!string.IsNullOrEmpty(cssClass))
            {
                tag.Attributes.Add("class", cssClass);
            }
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
        public static IHtmlContent PaginationNumbersWithPreviousNext<T>(this IHtmlHelper htmlHelper, Paginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);            
            Previous(content, htmlHelper, paginated, generatePageUrl, options);
            Numbers(content, htmlHelper, paginated, generatePageUrl, options);
            Next(content, htmlHelper, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }

        #region previous_next
        internal static HtmlContentBuilder Previous<T>(HtmlContentBuilder content, IHtmlHelper htmlHelper, Paginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            TagBuilder aPrevious = TagLink(paginated.IsFirstPage ? "#" : generatePageUrl(paginated.PageNumber - 1), 
                options.PreviousLabel, 
                options.CssClassLink);
            content.AppendHtmlTaLi(aPrevious, paginated.IsFirstPage ? options.CssClassLiDisabled : null);
            return content;            
        }
        internal static HtmlContentBuilder Next<T>(HtmlContentBuilder content, IHtmlHelper htmlHelper, Paginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {
            TagBuilder aNext = TagLink(paginated.IsLastPage ? "#": generatePageUrl(paginated.PageNumber + 1), 
                options.NextLabel, 
                options.CssClassLink);
            content.AppendHtmlTaLi(aNext, paginated.IsLastPage ? options.CssClassLiDisabled : null);
            return content;
        }
        public static IHtmlContent PaginationPreviousNext<T>(this IHtmlHelper htmlHelper, Paginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            Previous(content, htmlHelper, paginated, generatePageUrl, options);
            Next(content, htmlHelper, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

        #region numbers
        internal static HtmlContentBuilder Numbers<T>(HtmlContentBuilder content, IHtmlHelper htmlHelper, Paginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options)
        {            
            PaginatedMetaData metaData = paginated.ToPaginatedMetaData();
            int interaction = 3;
            List<int> pages = new List<int>();
            int pageNumber = paginated.PageNumber;
            while (interaction >= 0 && pageNumber > 0)
            {
                pages.Add(pageNumber--);
                interaction--;
            }
            interaction = 0;
            pageNumber = paginated.PageNumber;
            while (interaction < 3 && pageNumber < metaData.PageCount)
            {
                pages.Add(++pageNumber);
                interaction++;
            }
            pages.OrderBy(o => o).ToList()
                .ForEach(x =>
                {
                    var tagLink = TagLink((metaData.PageNumber != x) ? generatePageUrl(x) : "#", x.ToString(), options.CssClassLink);
                    content.AppendHtmlTaLi(tagLink, (metaData.PageNumber == x) 
                        ? options.CssClassLiActive + " " + options.CssClassLi
                        : options.CssClassLi);
                });           
            return content;
        }

        public static IHtmlContent PaginationNumbers<T>(this IHtmlHelper htmlHelper, Paginated<T> paginated, Func<int, string> generatePageUrl, PaginatedOptions options = null)
        {
            if (options == null) options = new PaginatedOptions();
            HtmlContentBuilder content = CreateHtmlContentBuilder(options);
            Numbers(content, htmlHelper, paginated, generatePageUrl, options);
            content.AppendHtml("</ul>");
            return content;
        }
        #endregion

    }
}
