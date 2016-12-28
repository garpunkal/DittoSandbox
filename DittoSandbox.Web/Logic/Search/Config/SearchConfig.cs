using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.Logic.Search.Config
{
    public class SearchConfig : ConfigurationSection
    {
        public const string SectionName = "jay.search";

        [ConfigurationProperty("searchSearcher", DefaultValue = "ExternalSearcher")]
        public string SearchSearcher
        {
            get { return (string)this["searchSearcher"]; }
            set { this["searchSearcher"] = value; }
        }

        [ConfigurationProperty("searchFields", DefaultValue = "nodeName,metaTitle,metaDescription,metaKeywords,bodyText")]
        public string SearchFields
        {
            get { return (string)this["searchFields"]; }
            set { this["searchFields"] = value; }
        }

        [ConfigurationProperty("pageSize", DefaultValue = 10)]
        public int PageSize
        {
            get { return (int)this["pageSize"]; }
            set { this["pageSize"] = value; }
        }
    }
}