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
            return View(DemoshopModel.Instance);
        }

        // ===================================================================================== []
        // UpdateItem
        [HttpPost]
        public ActionResult UpdateItem([Bind(Include = "Sku, Title, Price, Quantity")]DemoshopItemModel item)
        {
            DemoshopModel.Instance.SaveItem(item);
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // Add Task
        public ActionResult AddTask()
        {
            DemoshopModel.Instance.PublishItemOnEbay();
            return RedirectToAction("Index");
        }

        // ===================================================================================== []
        // Publish
        public ActionResult Publish()
        {
            // Code: Controller : Publish

           Dispatcher.Run(DemoshopModel.Instance.ChannelTasks);

            return View(DemoshopModel.Instance);
        }

    }
}