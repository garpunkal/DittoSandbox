using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models.Processors.Contexts
{
    public class CurrentPageContext : DittoProcessorContext
    {
        public long CurrentPage { get; set; }
    }
}