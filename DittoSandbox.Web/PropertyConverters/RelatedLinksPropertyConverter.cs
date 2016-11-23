using System.Collections.Generic;
using DittoSandbox.Web.PropertyConverters.Models;
using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace DittoSandbox.Web.PropertyConverters
{
    [PropertyValueType(typeof(RelatedLinks))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
    public class RelatedLinksPropertyConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.RelatedLinksAlias);
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
                return null;

            var sourceString = source.ToString();
            var relatedLinksData = JsonConvert.DeserializeObject<IEnumerable<RelatedLinkData>>(sourceString);
            var relatedLinks = new List<RelatedLink>();

            foreach (RelatedLinkData linkData in relatedLinksData)
            {
                var relatedLink = new RelatedLink
                {
                    Caption = linkData.Caption,
                    NewWindow = linkData.NewWindow,
                    IsInternal = linkData.IsInternal,
                    Type = linkData.Type,
                    Id = linkData.Internal,
                    Link = linkData.Link
                };
                relatedLink = CreateLink(relatedLink);

                if (!relatedLink.IsDeleted)
                    relatedLinks.Add(relatedLink);
                else
                    LogHelper.Warn<RelatedLinks>($"Related Links value converter skipped a link as the node has been unpublished/deleted (Internal Link NodeId: {relatedLink.Link}, Link Caption: \"{relatedLink.Caption}\")");
            }
            return new RelatedLinks(relatedLinks);
        }

        private RelatedLink CreateLink(RelatedLink link)
        {
            if (!link.IsInternal || link.Id == null) return link;
            if (UmbracoContext.Current == null) return null;

            link.Link = UmbracoContext.Current.UrlProvider.GetUrl((int)link.Id);
            if (link.Link.Equals("#"))
            {
                link.IsDeleted = true;
                link.Link = link.Id.ToString();
            }
            else
                link.IsDeleted = false;

            return link;
        }
    }
}