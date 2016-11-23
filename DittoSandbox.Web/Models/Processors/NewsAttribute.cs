using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using DittoSandbox.Web.Models.Processors.Contexts;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models.Processors
{
    [DittoProcessorMetaData(ContextType = typeof(CurrentPageContext))]
    public class NewsAttribute : BaseNewsAttribute
    {
        public override object ProcessValue()
        {
            var currentPage = ((CurrentPageContext)Context).CurrentPage;
            var pageSize = 2;

            var items = GetNews().ToList();
            var totalItems = items.Count;
            var totalPages = (long)Math.Ceiling(totalItems / (decimal)pageSize);

            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            var pagedItems = items
                .Skip((int)(currentPage - 1) * pageSize)
                .Take(pageSize)
                .As<NewsItem>();

            return new PagedCollection<NewsItem>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
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