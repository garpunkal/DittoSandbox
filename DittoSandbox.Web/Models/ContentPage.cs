using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DittoSandbox.Web.Models.Processors;
using Our.Umbraco.Ditto;

namespace DittoSandbox.Web.Models
{
    public class ContentPage : BaseModel
    {
       
        public HtmlString BodyText { get; set; }
    
        [DelimitedString]
        public IEnumerable<string> Departments { get; set; }

        [UmbracoProperty]
        [UmbracoPicker]
        public IEnumerable<Link> RelatedInternalPages { get; set; }
        
        [UmbracoProperty, UmbracoPicker]
        public Link PromotedPage { get; set; } 
    }
}