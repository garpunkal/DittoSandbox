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
    }
}