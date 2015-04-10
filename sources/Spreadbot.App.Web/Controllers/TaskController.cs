// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// TaskController.cs

using System.Web.Mvc;
using Spreadbot.App.Web.Models;

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