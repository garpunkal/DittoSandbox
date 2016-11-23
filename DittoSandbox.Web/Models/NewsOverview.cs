﻿using DittoSandbox.Web.Models.Processors;
using Our.Umbraco.Ditto;

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