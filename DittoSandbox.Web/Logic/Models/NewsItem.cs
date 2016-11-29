using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Logic.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models
{
    public class NewsItem : BaseModel
    {
        [DittoCache(CacheDuration = 300)]
        [UmbracoProperty]
        [UmbracoPicker]
        public IEnumerable<Image> Images { get; set; }

        [DittoCache(CacheDuration = 300)]
        [UmbracoProperty]
        [UmbracoPicker]
        public Image Image { get; set; } 

        public HtmlString BodyText { get; set; }
        
        [DelimitedString]
        public IEnumerable<string> Tags { get; set; }

        [DittoCache(CacheDuration = 300)]
        [UmbracoProperty]
        [UmbracoPicker]
        public Link PromotedPage { get; set; }

        [UmbracoProperty]
        public string Attachment { get; set; }

        public string Description { get; set; }
    }
}