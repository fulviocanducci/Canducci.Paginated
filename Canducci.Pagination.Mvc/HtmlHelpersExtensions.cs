using Canducci.Pagination.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.IO;
using System.Linq.Expressions;
//https://github.com/netdragoon/helpWebForms/blob/master/Canducci.HtmlHelpers/MethodsHelpers.cs
namespace Canducci.Pagination.Mvc
{
    public static class HtmlHelpers
    {
        #region internal
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
        public static string ToStringContent(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
        
        public static IHtmlContent Page<T>(this IHtmlHelper htmlHelper, Paginated<T> paginated)            
        {            
            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.MergeAttribute("class", "pagination");
            if (paginated.IsFirstPage)
            {
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\" tabindex=\"-1\">«</a></li>");
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\" tabindex=\"-1\">« voltar</a></li>");
            }
            else
            {
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item\">{htmlHelper.ActionLink("«", "Index", new { Page = 1 }).ToStringContent()}</li>");
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item\">{htmlHelper.ActionLink("« voltar", "Index", new { Page = paginated.HasPreviousPage ? paginated.PageNumber - 1 : paginated.PageNumber }).ToStringContent()}</li>");
            }
            tagUl.InnerHtml.AppendHtml("<li class=\"page-item active\">");
            tagUl.InnerHtml.AppendHtml($"<a class=\"page -link\" href=\"#\">{paginated.PageNumber} / {paginated.PageCount} <span class=\"sr-only\">(current)</span></a>");
            tagUl.InnerHtml.AppendHtml("</li>");
            if (!paginated.IsLastPage)
            {
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item\">{htmlHelper.ActionLink("avançar »", "Index", new { Page = paginated.HasNextPage ? paginated.PageNumber + 1 : paginated.PageNumber }).ToStringContent()}</li>");
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item\">{htmlHelper.ActionLink("»", "Index", new { Page = paginated.PageCount }).ToStringContent()}</li>");
            }
            else
            {
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\" tabindex=\"-1\">avançar »</a></li>");
                tagUl.InnerHtml.AppendHtmlLine($"<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\" tabindex=\"-1\">»</a></li>");                
            }
            
            return new HtmlString(tagUl.ToStringContent());
        }
    }
}
