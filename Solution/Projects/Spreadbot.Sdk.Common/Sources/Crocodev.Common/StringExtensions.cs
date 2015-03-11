namespace Crocodev.Common
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