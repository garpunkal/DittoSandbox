using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models.Processors
{
    public class TitleAttribute : DittoMultiProcessorAttribute
    {
        public string TitleAttr { get; set; }

        public TitleAttribute() : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new[] {
                new UmbracoPropertyAttribute(TitleAttr),
                new AltUmbracoPropertyAttribute("Name")
            });
        }
    }
}