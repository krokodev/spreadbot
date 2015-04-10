// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// FilterConfig.cs
// Roman, 2015-04-10 1:27 PM

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