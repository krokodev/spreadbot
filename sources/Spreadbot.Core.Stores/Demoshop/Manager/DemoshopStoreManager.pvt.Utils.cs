// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Utils.cs

using System;
using System.IO;
using Krokodev.Common.Extensions;
using Nereal.Serialization;
using Spreadbot.Core.Stores.Demoshop.Configuration.Sections;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        private static string GetDataFileName()
        {
            try {
                return DemoshopConfig.Instance.DemoshopPaths.DataFile.MapPathToDataDirectory();
            }
            catch( Exception e ) {
                throw new SpreadbotException( "Can't get [DataFile] value, check [app.config] for Demoshop Section: \n"
                    + e.Message );
            }
        }

        private void _LoadData()
        {
            var fileName = GetDataFileName();
            try {
                Serializer.Default.Deserialize( this, fileName );
            }
            catch {
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