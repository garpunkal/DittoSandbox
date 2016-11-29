using System.Collections.Generic;
using System.Web;
using DittoSandbox.Web.Logic.Models.Processors;
using DittoSandbox.Web.Logic.PropertyConverters.Models;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Logic.Models
{
    public class Homepage : BaseModel
    {
        public IHtmlString Description { get; set; }

        public RelatedLinks RelatedLinks { get; set; }

        [DelimitedString]
        public IEnumerable<string> Fruit { get; set; }

        public ImageCropDataSet PromotionalImage { get; set; }

        [DittoIgnore]
        public string ControllerAction { get; set; }

        [DittoCache(CacheDuration = 300)]
        [Heroes]
        public IEnumerable<Hero> Heroes { get; set; }

        public string PromotionalImageCrop(string cropAlias) => $"{PromotionalImage.Src}{PromotionalImage.GetCropUrl(cropAlias)}";
    }
}