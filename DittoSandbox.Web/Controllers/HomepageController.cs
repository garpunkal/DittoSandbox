using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DittoSandbox.Web.Models;
using DittoSandbox.Web.Models.Processors.Contexts;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Controllers
{
    public class HomepageController : DittoController
    {
        public ActionResult Index(RenderModel model)
        {
            var home = model.As<Homepage>();
            home.ControllerAction = "This could be data from Entity Framework?";

            return CurrentView(home);
        }
    }
}