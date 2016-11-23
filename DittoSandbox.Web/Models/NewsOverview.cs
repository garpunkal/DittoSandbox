using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Models.Processors;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace DittoSandbox.Web.Models
{
    public class NewsOverview : BaseModel
    {
        [Title]
        public string Title { get; set; }

        [DittoCache(CacheDuration = 300, CacheKeyBuilderType = typeof(NewsKeyBuilder))]
        [News]
        public PagedCollection<NewsItem> NewsItems { get; set; }
    }
}