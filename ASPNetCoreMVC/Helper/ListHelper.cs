using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace MvcApp
{
    public static class ListHelper
    {
        public static HtmlString CreateList(this IHtmlHelper html, string[] items)
        {
            var result = new StringBuilder("<ul>");

            foreach (var item in items)
                result.Append("<li>").Append(item).Append("</li>");

            result.Append("</ul>");

            return new HtmlString(result.ToString());
        }
    }
}