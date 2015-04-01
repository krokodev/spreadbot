// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Utils.cs
// Roman, 2015-04-01 9:09 PM

using Crocodev.Common.Extensions;
using Spreadbot.Core.Stores.Demoshop.Configuration.Sections;

namespace Spreadbot.Core.Stores.Demoshop.Manager
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