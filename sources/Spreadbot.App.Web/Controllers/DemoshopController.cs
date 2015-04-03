// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopController.cs
// Roman, 2015-04-03 12:48 PM

using System.Web.Mvc;
using Spreadbot.App.Web.Models;
using Spreadbot.Core.Stores.Demoshop.Items;
using Spreadbot.Core.System.Dispatcher;

// !>> Controller | DemoshopController

namespace Spreadbot.App.Web.Controllers
{
    public class DemoshopController : Controller
    {
        private static readonly object Locker = new object();

        // --------------------------------------------------------[]
        public ActionResult Index()
        {
            lock( Locker ) {
                DemoshopModel.Restore();
            }
            return View();
        }

        // --------------------------------------------------------[]
        [HttpPost]
        public ActionResult UpdateItem( [Bind( Include = "Sku, Title, Price, Quantity" )] DemoshopItem item )
        {
            lock( Locker ) {
                DemoshopModel.SaveItem( item );
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult AddTask()
        {
            lock( Locker ) {
                DemoshopModel.CreateTaskPublishItemOnEbay();
                DemoshopModel.Save();
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult RunChannelTasks()
        {
            lock( Locker ) {
                Dispatcher.Instance.RunChannelTasks( DemoshopModel.ChannelTasksTodo );
                DemoshopModel.Save();
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult ProceedChannelTasks()
        {
            lock( Locker ) {
                Dispatcher.Instance.ProceedChannelTasks( DemoshopModel.ChannelTasksInprocess );
                DemoshopModel.Save();
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult SaveTasks()
        {
            lock( Locker ) {
                DemoshopModel.Save();
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult RestoreTasks()
        {
            lock( Locker ) {
                DemoshopModel.Restore();
            }
            return RedirectToAction( "Index" );
        }

        // --------------------------------------------------------[]
        public ActionResult DeleteTasks()
        {
            lock( Locker ) {
                DemoshopModel.DeleteTasks();
                DemoshopModel.Save();
            }
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