// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.pvt.Utils.cs
// romak_000, 2015-03-19 17:19

using Crocodev.Common.Extensions;
using Spreadbot.App.Web.Sources.Configuration.Sections;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    public partial class DemoshopStoreManager
    {
        // --------------------------------------------------------[]
        private static string DataFileName()
        {
            return DemoshopConfig.Instance.DemoshopPaths.XmlDataFileName.MapPathToDataDirectory();
        }
    }
}