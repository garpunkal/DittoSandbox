using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using DittoSandbox.Web.Logic.Config;

namespace DittoSandbox.Web.Logic.Modules
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var config = (GlobalConfig)ConfigurationManager.GetSection(GlobalConfig.SectionName);
            builder.Register(x => config).SingleInstance();
        }
    }
}