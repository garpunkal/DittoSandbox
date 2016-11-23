using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DittoSandbox.Web.Models.Processors.Contexts;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Controllers
{
    public class NewsOverviewController : DittoController
    {
        public ActionResult Index(RenderModel model, long p = 1)
        {
            RegisterProcessorContext(new PaginationContext
            {
                PageNumber = p
            });

            return CurrentView(model);
        }
    }
}