using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Logic.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Logic.Processors
{
    public class ExtractAttribute : DittoProcessorAttribute
    {
        private int TruncateLimit { get; set; }

        public ExtractAttribute(int truncateLimit = 100)
        {
            TruncateLimit = truncateLimit;
        }
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            if (content == null) return null;

            if (content.HasValue("extract")) return content.Get<string>("extract");
            if (content.HasValue("bodyText")) return content.Get<string>("bodyText").StripHtml().Truncate(TruncateLimit);
            return null;
        }
    }
}