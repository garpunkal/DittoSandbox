using System;
using System.Collections.Generic;
using System.Web;
using DittoSandbox.Web.Logic.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models
{
    public class BaseModel
    {
        public int Id { get; set; } 
        
        public string DocumentTypeAlias { get; set; } 

        public string Url { get; set; }

        [Title]
        public string Title { get; set; }
        
        [PublishDate]
        public DateTime PublishDate { get; set; }

        [DittoCache(CacheDuration = 300)]
        [PrimaryNavigation("homepage", true)]
        public IEnumerable<TreeNode> PrimaryNavItems { get; set; }

        [DittoCache(CacheDuration = 300)]
        [SecondaryNavigation]
        public IEnumerable<TreeNode> SecondaryNavItems { get; set; }

        [DittoCache(CacheDuration = 300)]
        [BreadcrumbNavigation]
        public IEnumerable<TreeNode> BreadcrumbItems { get; set; }

        public bool IsHomepage => DocumentTypeAlias?.ToLower() == "homepage";
    }
}