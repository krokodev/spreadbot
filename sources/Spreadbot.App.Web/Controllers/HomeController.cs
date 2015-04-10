// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// HomeController.cs

using System.Web.Mvc;

namespace Spreadbot.App.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}