namespace Crocodev.Common
{
    public static class StringExtensions
    {
        public static string TryFormat(this string template, params object[] args)
        {
            try
            {
                return string.Format(template, args);
            }
            catch
            {
                // ignored
            }
            return template;
        }
    }
}
