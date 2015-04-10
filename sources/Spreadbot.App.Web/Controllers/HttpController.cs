// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// HttpController.cs

using System.Web.Mvc;

namespace Spreadbot.App.Web.Controllers
{
    public class HttpController : Controller
    {
        public ActionResult HttpStatus404( string aspxerrorpath )
        {
            ViewBag.ErrorPath = aspxerrorpath;
            return View();
        }

        public ActionResult DefaultRedirect()
        {
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}