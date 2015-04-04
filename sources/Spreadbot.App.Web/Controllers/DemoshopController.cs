// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopController.cs
// Roman, 2015-04-04 11:29 AM

using System.Web.Mvc;
using Spreadbot.App.Web.Models;
using Spreadbot.Core.Stores.Demoshop.Items;
using Spreadbot.Core.System.Dispatcher;

// !>> Controller | DemoshopController

namespace Spreadbot.App.Web.Controllers
{
    public class DemoshopController : Controller
    {
        // --------------------------------------------------------[]
        public ActionResult Index()
        {
            return View( new DemoshopModel() );
        }

        // --------------------------------------------------------[]
        [HttpPost]
        public ActionResult UpdateItem( [Bind( Include = "Sku, Title, Price, Quantity" )] DemoshopItem item )
        {
            using( var model = new DemoshopModel() ) {
                model.UpdateItem( item );
                model.Message = "Item updated";
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult AddTask()
        {
            using( var model = new DemoshopModel() ) {
                model.CreateTaskPublishItemOnEbay();
                model.Message = "Task added";
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult RunChannelTasks()
        {
            using( var model = new DemoshopModel() ) {
                Dispatcher.Instance.RunChannelTasks( model.ChannelTasksTodo );
                model.Message = "Tasks started";
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult ProceedChannelTasks()
        {
            using( var model = new DemoshopModel() ) {
                Dispatcher.Instance.ProceedChannelTasks( model.ChannelTasksInprocess );
                model.Message = "Tasks proceeded";
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult DeleteTasks()
        {
            using( var model = new DemoshopModel() ) {
                model.DeleteTasks();
                model.Message = "Tasks deleted";
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        // Code: Tode: Ref: Use TaskModel
        public ActionResult ShowTask( string taskId )
        {
            ViewBag.TaskId = taskId;
            return View();
        }
    }
}