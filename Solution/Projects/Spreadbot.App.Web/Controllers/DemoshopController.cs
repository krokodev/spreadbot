using System.Web.Mvc;
using Spreadbot.Core.Channel.Ebay;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.App.Web
{
    // Now: >> | Controller | DemoshopController
    public class DemoshopController : Controller
    {
        public ActionResult Index()
        {
            return View(DemoshopModel.Instance);
        }

        [HttpPost]
        public ActionResult UpdateItem([Bind(Include = "Sku, Title, Price, Quantity")]DemoshopItemModel item)
        {
            DemoshopModel.Instance.SaveItem(item);
            return RedirectToAction("Index");
        }

        public ActionResult Publish()
        {
            DemoshopModel.Instance.PublishItemOnEbay();

            var ebayChannel = new EbayChannel();

            var task = DemoshopModel.AsStore.GetTaskForChannel(ebayChannel);
            PublisherModel.Response = Dispatcher.Run(task);

            return View(new PublisherModel());
        }
    }


}