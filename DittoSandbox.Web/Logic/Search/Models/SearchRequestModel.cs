using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.Logic.Search.Models
{
    public class SearchRequestModel
    {
        public string Query { get; set; }
        public int Page { get; set; } = 1;
        public int RootContentNodeId { get; set; } = -1;
        public int RootMediaNodeId { get; set; } = -1;
        public List<string> SearchFields { get; set; }
        public int PageSize { get; set; }
        public string IndexType { get; set; }
        public string HideFromSearchField { get; set; } = StaticValues.Properties.UmbracoNaviHide;
        public string FormLocation { get; set; }
    }
}