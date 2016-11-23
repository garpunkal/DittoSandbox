using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models
{
    public class NewsItem : BaseModel
    {

        [DittoCache(CacheDuration = 300)]
        [UmbracoProperty]
        [UmbracoPicker]
        public IEnumerable<Image> Images { get; set; }

        [DittoIgnore]
        public Image FirstImage => Images.FirstOrDefault();

        public HtmlString BodyText { get; set; }
        

        [DelimitedString]
        public IEnumerable<string> Tags { get; set; }

        [DittoCache(CacheDuration = 300)]
        [UmbracoProperty]
        [UmbracoPicker]
        public Link PromotedPage { get; set; }

        [UmbracoProperty]
        public string Attachment { get; set; } 
    }
}