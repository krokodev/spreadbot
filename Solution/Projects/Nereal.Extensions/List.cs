/*
 *  $Id: List.cs 172 2010-11-19 14:41:41Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System.Collections.Generic;

namespace Nereal.Extensions {
    /// <summary>
    /// Расширений для класса List (или интерфейса IList).
    /// </summary>
    public static class ListExtensions {
        /// <summary>
        /// Получает значение по индексу из списка. При выходе за границы списка, не выдает ошибку, а возвращает значение по умолчанию.
        /// </summary>
        public static T GetOrDefault<T>(this IList<T> list, int index) {
            return list.GetOrDefault(index, default(T));
        }

        /// <summary>
        /// Получает значение по индексу из списка. При выходе за границы списка, не выдает ошибку, а возвращает указанное значение по умолчанию.
        /// </summary>
        public static T GetOrDefault<T>(this IList<T> list, int index, T defaultValue) {
            return index >= 0 && index < list.Count ? list[index] : defaultValue;
        }
    }
}
