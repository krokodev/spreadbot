using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        public static IEnumerable<string> GetStringValues<T>()
        {
            return GetValues<T>().Select(e=>e.ToString());
        }
    }
}