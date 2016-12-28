using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Logic.Search.Helpers
{
    public class SearchHelpers
    {
        // Splits a string on space, except where enclosed in quotes
        public static IEnumerable<string> Tokenize(string input)
        {
            return Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value.Trim('\"'))
                .ToList();
        }

        // Highlights all occurances of the search terms in a body of text
        public static IHtmlString Highlight(IHtmlString input, IEnumerable<string> searchTerms)
        {
            return Highlight(input.ToString(), searchTerms);
        }

        // Highlights all occurances of the search terms in a body of text
        public static IHtmlString Highlight(string input, IEnumerable<string> searchTerms)
        {
            input = HttpUtility.HtmlDecode(input);

            foreach (var searchTerm in searchTerms)
                input = Regex.Replace(input, Regex.Escape(searchTerm), @"<strong>$0</strong>", RegexOptions.IgnoreCase);

            return new HtmlString(input);
        }

        public static Enums.SearchFormLocation GetFormLocation(string value)
        {
            Enums.SearchFormLocation outValue;
            return Enum.TryParse(value, true, out outValue) ? outValue : Enums.SearchFormLocation.Bottom;
        }
    }
}