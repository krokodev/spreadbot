/*
 *  $Id: InlineListUtility.cs 148 2010-10-29 12:44:07Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Вспомогательные методы по работе со строчными списками.
    /// </summary>
    internal static class InlineListUtility {
        /// <summary>
        /// Разделитель элементов.
        /// </summary>
        internal const char Separator = ' ';
        /// <summary>
        /// Разделитель элементов в виде строки.
        /// </summary>
        internal const string SeparatorString = " ";

        /// <summary>
        /// Пустой список частей.
        /// </summary>
        private static readonly string[] EmptyParts = new string[0];

        /// <summary>
        /// Формирует строку из указанного набора элементов.
        /// </summary>
        internal static string GetString<T>(IEnumerable<T> items, TypeInfo<T> info) {
            return new StringBuilder().Append(items, item => info.ConvertToString(item), SeparatorString).ToString();
        }

        /// <summary>
        /// Получает разделенные части указанной строки.
        /// </summary>
        internal static string[] GetParts(string data) {
            return !string.IsNullOrEmpty(data) ? data.Split(Separator) : EmptyParts;
        }

        /// <summary>
        /// Получает массив из указанной строки.
        /// </summary>
        internal static T[] GetArray<T>(string data, TypeInfo<T> info) {
            var parts = GetParts(data);
            var arr = new T[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                arr[i] = info.ConvertFromString(parts[i]);
            return arr;
        }

        /// <summary>
        /// Обновляет список из указанной строки.
        /// </summary>
        internal static void UpdateList<T>(string data, TypeInfo<T> info, List<T> list) {
            list.Clear();
            var parts = GetParts(data);
            if (list.Capacity < parts.Length)
                list.Capacity = parts.Length;
            for (int i = 0; i < parts.Length; i++) {
                list.Add(info.ConvertFromString(parts[i]));
            }
        }
    }
}

