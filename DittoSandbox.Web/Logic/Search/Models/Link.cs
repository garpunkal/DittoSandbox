using System.Data.SqlTypes;
using Our.Umbraco.Ditto;
using DittoSandbox.Web.Logic.Search.Processors;

namespace DittoSandbox.Web.Logic.Search.Models
{
    public class Link
    {
        [UmbracoProperty(StaticValues.Properties.Name, Order = 0)]
        public string Title { get; set; }

        public string Url { get; set; }

        [Extract(300)]
        public string Extract { get; set; }

        [UrlTarget]
        public bool NewWindow { get; set; }
    }
}