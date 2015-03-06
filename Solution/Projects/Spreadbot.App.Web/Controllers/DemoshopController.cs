using System.Web.Mvc;

namespace Spreadbot.App.Web
{
    // Now: >> | Controller | DemoshopController
    public class DemoshopController : Controller
    {
        public ActionResult Index()
        {
            return View(new DemoshopModel());
        }

        [HttpPost]
        public ActionResult UpdateItem([Bind(Include = "Sku, Title, Price, Quantity")]DemoshopItemModel item)
        {
            DemoshopModel.SaveItem(item);
            return RedirectToAction("Index");
        }

        public ActionResult Publish()
        {
            return View(new PublisherModel());
        }
    }
}