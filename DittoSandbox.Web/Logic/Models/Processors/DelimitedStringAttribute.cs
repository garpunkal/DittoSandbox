using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Logic.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Logic.Models.Processors
{
    public class DelimitedStringAttribute : DittoProcessorAttribute
    {
        public string Delimiter { get; set; }

        public DelimitedStringAttribute(string delimiter = ",")
        {
            Delimiter = delimiter;
        }

        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            if (content == null) return false;

            var value = content.Get<string>(Context.PropertyDescriptor?.Name ?? string.Empty);

            if (string.IsNullOrWhiteSpace(value))
                return null;

             return value.Contains(Delimiter) ? value.ToDelimitedList(Delimiter) : new List<string> { value };
        }
    }
}