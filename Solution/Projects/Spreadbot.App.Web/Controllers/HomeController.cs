using System.IO;
using System.Web.Mvc;

namespace Crimenuts.App.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
    }
}