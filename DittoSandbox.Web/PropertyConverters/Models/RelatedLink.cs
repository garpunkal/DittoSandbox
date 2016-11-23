using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DittoSandbox.Web.PropertyConverters.Models
{
    public class RelatedLink : RelatedLinkBase
    {
        public int? Id { get; internal set; }
        internal bool IsDeleted { get; set; }
    }
}