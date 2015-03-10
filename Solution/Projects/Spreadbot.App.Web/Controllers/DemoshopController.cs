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
        // Publish
        public ActionResult Publish()
        {
            // Code: Controller.Publish
            DemoshopModel.Instance.PublishItemOnEbay();

            Dispatcher.Run(DemoshopModel.Instance.Tasks);

            return View(DemoshopModel.Instance);
        }
    }
}