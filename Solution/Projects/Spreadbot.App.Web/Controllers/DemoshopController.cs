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
        /*[ValidateAntiForgeryToken]*/
        public ActionResult UpdateItem([Bind(Include = "Sku, Title, Price, Quantity")]DemoshopItem item)
        {
            DemoshopModel.SaveItem(item);
            return RedirectToAction("Index");
        }
    }
}