using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DittoSandbox.Web.Logic.PropertyConverters.Models
{
    public class RelatedLinkData : RelatedLinkBase
    {
        [JsonProperty("internal")]
        public int? Internal { get; set; }
        [JsonProperty("edit")]
        public bool Edit { get; set; }
        [JsonProperty("internalName")]
        public string InternalName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}