using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models.Processors
{
    public class PublishDateAttribute : DittoMultiProcessorAttribute
    {
        public string PublishDateAttr { get; set; }

        public PublishDateAttribute() : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            base.Attributes.AddRange(new[] {
                new UmbracoPropertyAttribute(PublishDateAttr),
                new AltUmbracoPropertyAttribute("CreateDate")
            });
        }
    }
}