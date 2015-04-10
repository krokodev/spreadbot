// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// TaskController.cs
// Roman, 2015-04-10 1:27 PM

using System.Web.Mvc;
using Spreadbot.App.Web.Models;

// Here: Controller | Task

namespace Spreadbot.App.Web.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult Show( string taskId )
        {
            using( var model = new DemoshopModel() ) {
                ViewBag.TaskId = taskId;
                return View( model.FindTask( taskId ) );
            }
        }
    }
}