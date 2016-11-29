using System.Collections.Generic;
using System.Web;
using DittoSandbox.Web.Logic.Models.Processors;
using DittoSandbox.Web.Logic.PropertyConverters.Models;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Logic.Models
{
    public class Hero : BaseModel
    {
        [UmbracoProperty]
        [UmbracoPicker]
        public Image Image { get; set; }

        [UmbracoProperty]
        [UmbracoPicker]
        public Link Link { get; set; } 
    }
}