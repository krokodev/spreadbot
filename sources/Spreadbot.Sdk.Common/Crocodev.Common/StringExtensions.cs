// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// StringExtensions.cs
// Roman, 2015-04-01 4:59 PM

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