using System;
using System.Collections.Generic;
using System.Linq;
using DittoSandbox.Web.Extensions;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Models.Processors
{
    public abstract class BaseNewsAttribute : DittoProcessorAttribute
    {
        protected IEnumerable<IPublishedContent> GetNews(string homepageAlias, string newsOverviewAlias, string newsItemAlias, string publishDateAlias)
        {
            var content = Value as IPublishedContent;
            if (content == null) return Enumerable.Empty<IPublishedContent>();

            var homepage = content.AncestorsOrSelf(homepageAlias).First();

            var newsArchive = homepage.Children
                .FirstOrDefault(x => x.DocumentTypeAlias == newsOverviewAlias && x.IsVisible());

            if (newsArchive == null) return Enumerable.Empty<IPublishedContent>();

            return newsArchive.Children
                .Where(x => x.DocumentTypeAlias == newsItemAlias && x.IsVisible())
                .OrderByDescending(x => x.Get<DateTime>(publishDateAlias))
                .ThenByDescending(x => x.CreateDate);
        }
    }
}