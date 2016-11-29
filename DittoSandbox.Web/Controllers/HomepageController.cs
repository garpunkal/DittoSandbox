using System.Web.Mvc;
using DittoSandbox.Web.Logic.Config;
using DittoSandbox.Web.Logic.Models;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Controllers
{
    public class HomepageController : DittoController
    {
        private readonly GlobalConfig _sandboxConfig;

        public HomepageController(GlobalConfig sandboxConfig)
        {
            _sandboxConfig = sandboxConfig;
        }


        public ActionResult Index(RenderModel model)
        {
            var home = model.As<Homepage>();
            home.ControllerAction = _sandboxConfig.CacheEnabled.ToString();

            return CurrentView(home);
        }
    }
}