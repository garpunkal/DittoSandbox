using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models
{
    public class Link
    {
        [UmbracoProperty("Name", Order = 0)]
        public string Title { get; set; }

        public string Url { get; set; }
    }
}