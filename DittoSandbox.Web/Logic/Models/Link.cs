using DittoSandbox.Web.Logic.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Logic.Models
{
    public class Link
    {
        [UmbracoProperty("Name", Order = 0)]
        public string Title { get; set; }

        public string Url { get; set; }

        [UrlTarget]
        public bool NewWindow { get; set; }

        public string NewWindowAttribute => NewWindow ? " target='_blank'" : string.Empty;
    }
}