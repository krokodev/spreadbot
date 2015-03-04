using System.Web.Mvc;

namespace Spreadbot.App.Web
{
    // >> | Controller | MipDemoController
    public class MipDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
        public ActionResult Start()
        {
            return View(new HomeModel());
        }
    }
}