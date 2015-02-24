using System.Web.Mvc;

namespace Spreadbot.App.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
    }
}