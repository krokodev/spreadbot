﻿using System.Web.Mvc;

namespace Spreadbot.App.Web
{
    // Now: >> | Controller | DemoshopController
    public class DemoshopController : Controller
    {
        public ActionResult Index()
        {
            return View(new DemoshopModel());
        }
    }
}