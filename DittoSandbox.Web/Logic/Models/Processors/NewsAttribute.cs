using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using DittoSandbox.Web.Logic.Config;
using DittoSandbox.Web.Logic.Models.Processors.Contexts;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models.Processors
{
    [DittoProcessorMetaData(ContextType = typeof(PaginationContext))]
    public class NewsAttribute : BaseNewsAttribute
    {
        public string HomepageAlias { get; set; }
        public string NewsOverviewAlias { get; set; }
        public string NewsItemAlias { get; set; }
        public string PublishDateAlias { get; set; }
        public int PageSize { get; set; }

        public NewsAttribute(
            string homepageAlias = "homepage", 
            string newsOverviewAlias = "newsOverview",
            string newsItemAlias = "newsItem", 
            string publishDateAlias = "publishDate",
            int pageSize = 10)
        {
            HomepageAlias = homepageAlias;
            NewsOverviewAlias = newsOverviewAlias;
            NewsItemAlias = newsItemAlias;
            PublishDateAlias = publishDateAlias;
            PageSize = pageSize;
        }
        
        public override object ProcessValue()
        {
            var pageNumber = ((PaginationContext)Context).PageNumber;
            var items = GetNews(HomepageAlias, NewsOverviewAlias, NewsItemAlias, PublishDateAlias).ToList();
            var totalItems = items.Count;
            var totalPages = (long)Math.Ceiling(totalItems / (decimal)PageSize);

            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages));

            var pagedItems = items
                .Skip((int)(pageNumber - 1) * PageSize)
                .Take(PageSize)
                .As<NewsItem>();

            return new PagedCollection<NewsItem>
            {
                CurrentPage = pageNumber,
                PageSize = PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = pagedItems
            };
        }
    }

    public class NewsKeyBuilder : DittoDefaultCacheKeyBuilder
    {
        public override string BuildCacheKey(DittoCacheContext context)
        {
            string key = base.BuildCacheKey(context);
            key += "_" + HttpContext.Current.Request.QueryString["p"];
            return key;
        }
    }
}