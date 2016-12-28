using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Logic.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence.Repositories;

namespace DittoSandbox.Web.Logic.Search.Models
{
    public class SearchResultViewModel
    {
        public string IndexType { get; set; }
        public Link Content { get; set; }
        public Image Media { get; set; }

        public IEnumerable<string> SearchTerms { get; set; }

        [UmbracoDictionary(StaticValues.Dictionary.Search.ExtensionLabel)]
        public string ExtensionLabel { get; set; } = "Extension";
    }
}
