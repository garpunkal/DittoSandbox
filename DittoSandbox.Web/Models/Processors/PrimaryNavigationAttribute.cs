﻿using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Models.Processors
{
    public class PrimaryNavigationAttribute : DittoProcessorAttribute
    {
        public string HomepageAlias { get; set; }
        public bool IncludeHomepage { get; set; }

        public PrimaryNavigationAttribute(string homepageAlias = "homepage", bool includeHomepage = false)
        {
            HomepageAlias = homepageAlias;
            IncludeHomepage = includeHomepage;
        }

        public override object ProcessValue()
        {
            IPublishedContent home = Context.Content.AncestorOrSelf(HomepageAlias);
            if (home == null) return null;

            var items = new List<TreeNode>();

            if (IncludeHomepage)
                items.Add(new TreeNode
                {
                    Name = home.Name,
                    Url = home.Url
                });

            foreach (var item in home.Children.Where(x => x.IsVisible()))
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