using System.Collections.Generic;
using System.Linq;
using System.Web;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Models.Processors
{
    public class SecondaryNavigationAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            IPublishedContent currentPage = Context.Content;
            if (currentPage == null) return null;

            var items = new List<TreeNode>();
            
            foreach (var item in currentPage.Children.Where(x => x.IsVisible()))
            {
                items.Add(new TreeNode
                {
                    Name = item.Name,
                    Url = item.Url
                });
            }
            return items.Any() ? items : null;
        }
    }
}