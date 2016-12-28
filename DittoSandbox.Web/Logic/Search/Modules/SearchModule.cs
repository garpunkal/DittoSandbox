using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using DittoSandbox.Web.Logic.Search.ModelBuilders;
using DittoSandbox.Web.Logic.Search.Services;

namespace DittoSandbox.Web.Logic.Search.Modules
{
    public class SearchModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchFilterBuilder>().AsSelf().InstancePerRequest();
            builder.RegisterType<SearchResultModelBuilder>().AsSelf().InstancePerRequest();
            builder.RegisterType<SearchService>().AsSelf().InstancePerRequest();
        }
    }
}