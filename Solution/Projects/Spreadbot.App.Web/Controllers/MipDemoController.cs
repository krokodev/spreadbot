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
        public ActionResult Start(int id)
        {
            MipDemoModel.Identifier identifier = (MipDemoModel.Identifier)id;
            return View(HomeModel.FindMipDemo(identifier));
        }
    }
}