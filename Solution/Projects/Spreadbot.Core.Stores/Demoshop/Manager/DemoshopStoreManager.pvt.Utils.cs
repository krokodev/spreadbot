// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Utils.cs
// romak_000, 2015-03-20 13:57

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