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

        [ConfigurationProperty("rootContentNodeId", DefaultValue = -1)]
        public int RootContentNodeId
        {
            get { return (int)this["rootContentNodeId"]; }
            set { this["rootContentNodeId"] = value; }
        }

        [ConfigurationProperty("rootMediaNodeId", DefaultValue = -1)]
        public int RootMediaNodeId
        {
            get { return (int)this["rootMediaNodeId"]; }
            set { this["rootMediaNodeId"] = value; }
        }
        
        [ConfigurationProperty("indexType", DefaultValue = "Both")]
        public string IndexType
        {
            get { return (string)this["indexType"]; }
            set { this["indexType"] = value; }
        }

        [ConfigurationProperty("formLocation", DefaultValue = "Bottom")]
        public string FormLocation
        {
            get { return (string)this["formLocation"]; }
            set { this["formLocation"] = value; }
        }
        
        [ConfigurationProperty("hideFromSearchField", DefaultValue = "umbracoNaviHide")]
        public string HideFromSearchField
        {
            get { return (string)this["hideFromSearchField"]; }
            set { this["hideFromSearchField"] = value; }
        }

        [ConfigurationProperty("fuzzySearch", DefaultValue = "0")]
        public string FuzzySearch
        {
            get { return (string)this["fuzzySearch"]; }
            set { this["fuzzySearch"] = value; }
        }
    }
}