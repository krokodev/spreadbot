// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// BugController.cs
// Roman, 2015-04-09 6:03 PM

using System.Web.Mvc;
using Spreadbot.Sdk.Common.Research;

namespace Spreadbot.App.Web.Controllers
{
    public class BugController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThrowSpreadbotException()
        {
            BugGenerator.ThrowSpreadbotException( "From Bug Controller" );
            return RedirectToAction( "Index" );
        }
    }
}