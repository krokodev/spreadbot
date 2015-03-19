// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// StringExtensions.cs
// romak_000, 2015-03-19 13:44

namespace Spreadbot.Sdk.Common.Crocodev.Common.Etensions
{
    public static class StringExtensions
    {
        // ===================================================================================== []
        // TryFormat
        public static string TryFormat(this string template, params object[] args)
        {
            return string.Format(template, args);
        }
    }
}