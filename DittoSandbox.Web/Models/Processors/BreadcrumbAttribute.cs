using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Models.Processors
{
    public class BreadcrumbNavigationAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            IPublishedContent currentPage = Context.Content;
            if (currentPage == null) return null;

            return currentPage
                .AncestorsOrSelf()
                .Select(ancestor => new TreeNode
                {
                    Id = ancestor.Id,
                    Name = ancestor.Name,
                    Url = ancestor.TemplateId > 0 ? ancestor.Url : string.Empty,
                    Current = currentPage.Id == ancestor.Id
                })
                .Reverse();
        }
    }
}