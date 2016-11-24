using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using DittoSandbox.Web.Extensions;
using DittoSandbox.Web.Models.Processors.Contexts;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DittoSandbox.Web.Models.Processors
{
    public class HeroesAttribute : DittoProcessorAttribute
    {
        public string DataAlias { get; set; }
        public string HeroesRepositoryAlias { get; set; }
        public string HeroAlias { get; set; }

        public HeroesAttribute(string dataAlias = "dataRepository", string heroesRepositoryAlias = "heroesRepository", string heroAlias = "hero")
        {
            DataAlias = dataAlias;
            HeroesRepositoryAlias = heroesRepositoryAlias;
            HeroAlias = heroAlias; 
        }

        public override object ProcessValue()
        {
            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current);

            var dataRepo = helper.TypedContentAtRoot()
                .FirstOrDefault(x => x.DocumentTypeAlias == DataAlias && x.IsVisible());

            var heroesRepo = dataRepo?.Children
                .FirstOrDefault(x => x.DocumentTypeAlias == HeroesRepositoryAlias && x.IsVisible());

            return heroesRepo?.Children
                .Where(x => x.DocumentTypeAlias == HeroAlias && x.IsVisible());
        }
    }
}