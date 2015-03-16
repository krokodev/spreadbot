/*
 *  $Id: StringBuilder.cs 105 2010-10-22 10:25:36Z thenn $
 *  This file is a part of Nereal Extensions library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Nereal.Extensions {
    /// <summary>
    /// Расширения для класса StringBuilder.
    /// </summary>
    public static class StringBuilderExtensions {
        /// <summary>
        /// Добавляет набор строк с указанным разделителем.
        /// </summary>
        public static StringBuilder Append(this StringBuilder builder, IEnumerable<string> strings, string separator) {
            return builder.Append(strings, (sb, s) => sb.Append(s), separator);
        }

        /// <summary>
        /// Добавляет набор объектов с указанным разделителем. Объекты превращаются в строки с помощью указанного делегата.
        /// </summary>
        public static StringBuilder Append<T>(this StringBuilder builder, IEnumerable<T> items, Func<T, string> converter, string separator) {
            return builder.Append(items, (sb, item) => sb.Append(converter(item)), separator);
        }

        /// <summary>
        /// Добавляет набор объектов с указанным разделителем. Объекты добавляются с помощью указанного делегата.
        /// </summary>
        public static StringBuilder Append<T>(this StringBuilder builder, IEnumerable<T> items, Action<StringBuilder, T> appender, string separator) {
            bool first = true;
            foreach (var item in items) {
                if (first)
                    first = false;
                else
                    builder.Append(separator);
                appender(builder, item);
            }
            return builder;
        }
    }
}

