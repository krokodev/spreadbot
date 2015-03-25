// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopController.cs
// romak_000, 2015-03-25 15:24

using System.Web.Mvc;
using Spreadbot.App.Web.Models;
using Spreadbot.Core.Stores.Demoshop.Items;
using Spreadbot.Core.System.Dispatcher;

namespace Spreadbot.App.Web.Controllers
{
    // !>> Controller | DemoshopController
    public class DemoshopController : Controller
    {
        // ===================================================================================== []
        // Index
        public ActionResult Index()
        {
            DemoshopModel.Restore();
            return View();
        }

        // ===================================================================================== []
        // UpdateItem
        [HttpPost]
        public ActionResult UpdateItem( [Bind( Include = "Sku, Title, Price, Quantity" )] DemoshopItem item )
        {
            DemoshopModel.SaveItem( item );
            return RedirectToAction( "Index" );
        }

        // ===================================================================================== []
        // Add Task
        public ActionResult AddTask()
        {
            DemoshopModel.CreateTaskPublishItemOnEbay();
            DemoshopModel.Save();
            return RedirectToAction( "Index" );
        }

        // ===================================================================================== []
        // RunChannelTasks
        public ActionResult RunChannelTasks()
        {
            Dispatcher.Instance.RunChannelTasks( DemoshopModel.ChannelTasksTodo );
            DemoshopModel.Save();
            return RedirectToAction( "Index" );
        }

        // ===================================================================================== []
        // ProceedChannelTasks
        public ActionResult ProceedChannelTasks()
        {
            Dispatcher.Instance.ProceedChannelTasks( DemoshopModel.ChannelTasksInprocess );
            DemoshopModel.Save();
            return RedirectToAction( "Index" );
        }

        // ===================================================================================== []
        // Save/Restore Tasks
        public ActionResult SaveTasks()
        {
            DemoshopModel.Save();
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult RestoreTasks()
        {
            DemoshopModel.Restore();
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult DeleteTasks()
        {
            DemoshopModel.DeleteTasks();
            DemoshopModel.Save();
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult ShowTask( string taskId )
        {
            ViewBag.TaskId = taskId;
            return View();
        }
    }
}