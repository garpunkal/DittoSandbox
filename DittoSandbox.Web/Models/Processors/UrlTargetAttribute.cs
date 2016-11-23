using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace DittoSandbox.Web.Models.Processors
{
    public class UrlTargetAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            return !content?.Url.StartsWith("/");
        }
    }
}