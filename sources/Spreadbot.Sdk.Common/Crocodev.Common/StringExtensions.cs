// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// StringExtensions.cs
// Roman, 2015-04-03 1:45 PM

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