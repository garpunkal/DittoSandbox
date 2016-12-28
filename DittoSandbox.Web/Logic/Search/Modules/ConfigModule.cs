using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using DittoSandbox.Web.Logic.Search.Config;

namespace DittoSandbox.Web.Logic.Search.Modules
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var config = (SearchConfig)ConfigurationManager.GetSection(SearchConfig.SectionName);
            builder.Register(x => config).SingleInstance();
        }
    }
}