﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.App.Web
// FilterConfig.cs

using System.Web.Mvc;

namespace Spreadbot.App.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters( GlobalFilterCollection filters )
        {
            filters.Add( new HandleErrorAttribute() );
        }
    }
}