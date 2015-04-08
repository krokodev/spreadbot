// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Utils.cs
// Roman, 2015-04-07 2:57 PM

using System;
using System.IO;
using Crocodev.Common.Extensions;
using Nereal.Serialization;
using Spreadbot.Core.Stores.Demoshop.Configuration.Sections;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        private static string GetDataFileName()
        {
            return DemoshopConfig.Instance.DemoshopPaths.XmlDataFileName.MapPathToDataDirectory();
        }

        private void _LoadData()
        {
            var fileName = GetDataFileName();
            try {
                Serializer.Default.Deserialize( this, fileName );
            }
            catch( Exception ) {
                Message = "Error: Can't load data".SafeFormat( fileName );
            }
        }

        private void _SaveData()
        {
            var fileName = GetDataFileName();
            var tmpFileName = fileName + ".bak";
            try {
                Serializer.Default.Serialize( this, tmpFileName );
                File.Delete( fileName );
                File.Copy( tmpFileName, fileName );
            }
            catch {
                Message = "Error: Can't save data".SafeFormat( fileName );
            }
        }
    }
}