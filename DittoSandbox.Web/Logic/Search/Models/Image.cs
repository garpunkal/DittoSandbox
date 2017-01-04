using System;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Search.Models
{
    public class Image : Link
    {
        [UmbracoProperty(StaticValues.Properties.UmbracoExtension)]
        public string Extension { get; set; }

        public string DocumentTypeAlias { get; set; }

        public bool IsImage => string.Equals(DocumentTypeAlias, StaticValues.DocumentTypes.Image, StringComparison.OrdinalIgnoreCase);
    }
}