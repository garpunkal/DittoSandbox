using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;

namespace DittoSandbox.Web.Logic.Search.Extensions
{
    public static class SearchExtensions
    {
        public static string CleanseSearchTerm(this UmbracoHelper helper, string input)
        {
            return helper.StripHtml(input).ToString();
        }

        public static IHtmlString FormatHtml(this HtmlHelper html, string input, params object[] args)
        {
            return html.Raw(string.Format(input, args));
        }

        public static IList<string> SplitToList(this string input)
        {
            return input.Split(',')
                .Select(f => f.Trim())
                .Where(f => !string.IsNullOrEmpty(f))
                .ToList();
        }
    }
}