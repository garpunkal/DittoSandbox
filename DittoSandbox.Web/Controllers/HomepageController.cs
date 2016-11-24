using System.Web.Mvc;
using DittoSandbox.Web.Models;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Controllers
{
    public class HomepageController : DittoController
    {
        public ActionResult Index(RenderModel model)
        {
            var home = model.As<Homepage>();
            home.ControllerAction = "Controller action hijack!";

            return CurrentView(home);
        }
    }
}