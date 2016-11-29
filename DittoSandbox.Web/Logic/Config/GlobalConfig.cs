using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.Logic.Config
{
    public class GlobalConfig : ConfigurationSection
    {
        public const string SectionName = "ditto.sandbox";

        [ConfigurationProperty("cacheEnabled", DefaultValue = true)]
        public bool CacheEnabled 
        {
            get { return (bool) this["cacheEnabled"]; }
            set { this["cacheEnabled"] = value; }
        }
    }
}