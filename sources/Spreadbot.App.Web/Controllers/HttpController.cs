// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// HttpController.cs
// Roman, 2015-04-09 6:34 PM

using System.Web.Mvc;

namespace Spreadbot.App.Web.Controllers
{
    // Here: Controller | HttpController
    public class HttpController : Controller
    {
        public ActionResult HttpStatus404(string aspxerrorpath)
        {
            ViewBag.Title = "Http Status 404";
            ViewBag.ErrorPath = aspxerrorpath;
            return View();
        }        
        
        public ActionResult DefaultRedirect()
        {
            ViewBag.Title = "DefaultRedirect";
            return View();
        }
        public ActionResult ErrorPage()
        {
            ViewBag.Title = "ErrorPage";
            return View();
        }
    }
}