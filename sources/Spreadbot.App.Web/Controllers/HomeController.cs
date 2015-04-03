// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// HomeController.cs
// Roman, 2015-04-03 8:15 PM

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