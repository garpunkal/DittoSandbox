using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Models.Processors;
using DittoSandbox.Web.PropertyConverters.Models;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace DittoSandbox.Web.Models
{
    public class Homepage : BaseModel
    {
        [Title]
        public string Title { get; set; }

        public IHtmlString Description { get; set; }

        [UmbracoProperty]
        [UmbracoPicker]
        public Image Hero { get; set; }

        public RelatedLinks RelatedLinks { get; set; }

        [DelimitedString]
        public IEnumerable<string> Fruit { get; set; }  
    }
}