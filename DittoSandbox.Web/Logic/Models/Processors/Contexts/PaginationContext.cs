using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models.Processors.Contexts
{
    public class PaginationContext : DittoProcessorContext
    {
        public long PageNumber { get; set; }
    }
}