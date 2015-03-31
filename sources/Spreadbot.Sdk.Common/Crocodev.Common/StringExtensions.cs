// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// StringExtensions.cs
// romak_000, 2015-03-26 19:42

namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    public static class StringExtensions
    {
        public static string UnescapeSlashes( this string str )
        {
            return str
                .Replace( @"\r\n", "\r\n" )
                .Replace( @"\\", "\\" )
                .Replace( @"\""", "\"" )
                ;
        }
    }
}