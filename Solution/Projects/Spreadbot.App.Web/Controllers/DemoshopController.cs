// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopController.cs
// romak_000, 2015-03-19 14:07

using System.Web.Mvc;
using Spreadbot.App.Web.Models;
using Spreadbot.App.Web.Sources.Demoshop;
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
            return View(new DemoshopModel());
        }

        // ===================================================================================== []
        // UpdateItem
        [HttpPost]
        public ActionResult UpdateItem([Bind(Include = "Sku, Title, Price, Quantity")] DemoshopItem item)
        {
            DemoshopModel.SaveItem(item);
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // Add Task
        public ActionResult AddTask()
        {
            DemoshopModel.PublishItemOnEbay();
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // RunChannelTasks
        public ActionResult RunChannelTasks()
        {
            Dispatcher.RunChannelTasks(DemoshopModel.ChannelTasksTodo);
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // ProceedChannelTasks
        public ActionResult ProceedChannelTasks()
        {
            Dispatcher.ProceedChannelTasks(DemoshopModel.ChannelTasksInprocess);
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // Save/Restore Tasks
        public ActionResult SaveTasks()
        {
            DemoshopModel.Save();
            return RedirectToAction("Index");
        }

        // --------------------------------------------------------[]
        public ActionResult RestoreTasks()
        {
            DemoshopModel.Restore();
            return RedirectToAction("Index");
        }
    }
}