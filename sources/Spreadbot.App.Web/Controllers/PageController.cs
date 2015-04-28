// Spreadbot (c) 2015 Krokodev
// Spreadbot.App.Web
// PageController.cs

using System.Web.Mvc;

// Here: Controller | Page

namespace Spreadbot.App.Web.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }
    }
}