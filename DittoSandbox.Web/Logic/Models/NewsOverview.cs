using DittoSandbox.Web.Logic.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models
{
    public class NewsOverview : BaseModel
    {
        [DittoCache(CacheDuration = 300, CacheKeyBuilderType = typeof(NewsKeyBuilder))]
        [News("homepage", "newsOverview", "newsItem", "publishDate", 2)]
        public PagedCollection<NewsItem> NewsItems { get; set; }
    }
}