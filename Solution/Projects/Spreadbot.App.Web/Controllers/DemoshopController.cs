using System.Web.Mvc;
using Spreadbot.Core.System;

namespace Spreadbot.App.Web
{
    // >> Controller | DemoshopController
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
            return View(new DemoshopModel());
        }

        // ===================================================================================== []
        // Update
        public ActionResult UpdateChannelTasks()
        {
            Dispatcher.UpdateChannelTasks(DemoshopModel.ChannelTasksInprocess);
            return RedirectToAction("Index");
        }
    }
}