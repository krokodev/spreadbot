// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// FileSystemExtensions.cs
// romak_000, 2015-03-19 13:44

using System;

namespace Spreadbot.Sdk.Common.Crocodev.Common.Etensions
{
    public static class FileSystemExtensions
    {
        public static string MapPathToDataDirectory(this string path)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory") + path;
        }
    }
}