// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// HomeController.cs
// Roman, 2015-03-31 1:25 PM

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