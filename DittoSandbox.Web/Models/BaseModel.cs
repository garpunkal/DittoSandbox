using System;
using System.Collections.Generic;
using System.Web;
using DittoSandbox.Web.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models
{
    public class BaseModel
    {
        [Title]
        public string Title { get; set; }

        [PublishDate]
        public DateTime PublishDate { get; set; }
        
        public string Url { get; set; }


        [PrimaryNavigation("homepage", true)]
        public IEnumerable<TreeNode> NavItems { get; set; } 
    }
}