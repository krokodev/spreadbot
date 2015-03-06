using System;
using System.Collections.Generic;
using System.Linq;

namespace Crocodev.Common
{
    public static class StringExtensions
    {
        // ===================================================================================== []
        // SafeFormat
        public static string SafeFormat(this string template, params object[] args)
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

        // ===================================================================================== []
        // FoldToStringBy
        public static string FoldToStringBy<T>(
            this IEnumerable<T> enumerable, 
            Func<T, string> selector,
            string delimiter = ", ", 
            string emptyResult = "")
        {
            var list = enumerable as IList<T> ?? enumerable.ToList();
            if (!list.Any()) return emptyResult;
            return list.Select(selector).Aggregate((a, s) => string.Format("{0}{1}{2}", a, delimiter, s));
        }
    }
}