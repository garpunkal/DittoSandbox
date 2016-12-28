using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Examine;
using Umbraco.Core;
using Umbraco.Forms.Web.Trees;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace DittoSandbox.Web.Logic
{
    public class EventHandlers : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Register the application dependencies and receive a built container.
            IContainer container = RegisterDependencies(applicationContext);

            // Register the Autofac container with the WebApi resolver.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register the Autofac container with the MVC resolver.
            //DependencyResolver.SetResolver(new DependencyResolverDecorator(new AutofacDependencyResolver(container)));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
     
        private IContainer RegisterDependencies(ApplicationContext applicationContext)
        {
            var builder = new ContainerBuilder();

            Assembly applicationWebAssembly = GetType().Assembly;

            // Register the Umbraco built-in controllers.
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

            // Register specific Controllers included as part of the Umbraco forms package.
            builder.RegisterApiControllers(typeof(FormTreeController).Assembly);

            // Register our API controllers.
            builder.RegisterApiControllers(applicationWebAssembly)
                .PropertiesAutowired();

            // Register our controllers.
            builder.RegisterControllers(applicationWebAssembly);

            // Register Autofac helpers, allows you to resolve ASP.net items like HttpContext as dependencies.
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register all application modules in the startup assembly.
            builder.RegisterAssemblyModules(applicationWebAssembly);

            builder.Register(c => applicationContext)
                .As<ApplicationContext>()
                .InstancePerRequest();

            builder.Register(c => new UmbracoHelper(UmbracoContext.Current))
                .As<UmbracoHelper>()
                .InstancePerRequest();

            return builder.Build();
        }
    }
}