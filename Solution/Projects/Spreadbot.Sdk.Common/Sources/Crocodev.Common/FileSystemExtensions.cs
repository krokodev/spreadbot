using System;

namespace Crocodev.Common
{
    public static class FileSystemExtensions
    {
        public static string MapPathToDataDirectory(this string path)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory") + path;
        }
    }

}