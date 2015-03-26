namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    static public class StringExtensions
    {
        public static string UnescapeSlashes(this string str)
        {
            return str
                .Replace( @"\r\n", "\r\n" )
                .Replace( @"\\", "\\" )
                .Replace( @"\""", "\"" )
                ;
        }
    }
}