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
        protected IEnumerable<IPublishedContent> GetNews()
        {
            var content = Value as IPublishedContent;
            if (content == null) return Enumerable.Empty<IPublishedContent>();

            var homepage = content.AncestorsOrSelf(1).First();
            var newsArchive = homepage.Children.FirstOrDefault(x => x.DocumentTypeAlias == "newsOverview");
            if (newsArchive == null) return Enumerable.Empty<IPublishedContent>();

            return newsArchive.Children
                .Where(x => x.DocumentTypeAlias == "newsItem")
                .OrderByDescending(x => x.Get<DateTime>("publishDate"))
                .ThenByDescending(x => x.CreateDate);
        }
    }
}