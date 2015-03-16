/*
 *  $Id: Dictionary.cs 105 2010-10-22 10:25:36Z thenn $
 *  This file is a part of Nereal Extensions library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

namespace Nereal.Extensions {
    /// <summary>
    /// Расширений для класса Dictionary (или интерфейса IDictionary).
    /// </summary>
    public static class DictionaryExtensions {
        /// <summary>
        /// Дабавляет к словарю содержимое другого словаря.
        /// </summary>
        public static void AddRange<K, T>(this IDictionary<K, T> dict, IDictionary<K, T> values) {
            foreach (var v in values)
                dict.Add(v);
        }

        /// <summary>
        /// Добавляет к словарю указанные значения по ключам, полученным указанным делегатом.
        /// </summary>
        public static void AddRange<K, T>(this IDictionary<K, T> dict, IEnumerable<T> values, Converter<T, K> keyGetter) {
            foreach (var v in values)
                dict.Add(keyGetter(v), v);
        }

        /// <summary>
        /// Добавляет к словарю указанные значения по ключам, полученным по интерфейсу IKeyed.
        /// </summary>
        public static void AddRange<K, T>(this IDictionary<K, T> dict, IEnumerable<T> values) where T : IKeyed<K> {
            foreach (var v in values)
                dict.Add(v.Key, v);
        }

        /// <summary>
        /// Добавляет к словарю указанные пары ключ/значение.
        /// </summary>
        public static void AddRange<K, T>(this IDictionary<K, T> dict, IEnumerable<Tuple<K, T>> values) {
            foreach (var v in values)
                dict.Add(v.Item1, v.Item2);
        }

        /// <summary>
        /// Создает словарь из указанных значений по ключам, полученным указанным делегатом.
        /// </summary>
        public static Dictionary<K, T> ToDictionary<K, T>(this IEnumerable<T> values, Converter<T, K> keyGetter) {
            var dict = new Dictionary<K, T>();
            dict.AddRange(values, keyGetter);
            return dict;
        }

        /// <summary>
        /// Создает словарь из указанных значений по ключам, полученным по интерфейсу IKeyed.
        /// </summary>
        public static Dictionary<K, T> ToDictionary<K, T>(this IEnumerable<T> values) where T: IKeyed<K> {
            var dict = new Dictionary<K, T>();
            dict.AddRange(values);
            return dict;
        }

        /// <summary>
        /// Создает словарь из указанных пар ключ/значение.
        /// </summary>
        public static Dictionary<K, T> ToDictionary<K, T>(this IEnumerable<Tuple<K, T>> values) {
            var dict = new Dictionary<K, T>();
            dict.AddRange(values);
            return dict;
        }

        /// <summary>
        /// Получает значение по ключу из словаря. При отсутствии ключа, не выдает ошибку, а возвращает значение по умолчанию.
        /// </summary>
        public static T GetOrDefault<K, T>(this IDictionary<K, T> dict, K key) {
            return dict.GetOrDefault(key, default(T));
        }

        /// <summary>
        /// Получает значение по ключу из словаря. При отсутствии ключа, не выдает ошибку, а возвращает указанное значение по умолчанию.
        /// </summary>
        public static T GetOrDefault<K, T>(this IDictionary<K, T> dict, K key, T defaultValue) {
            T value;
            if (dict.TryGetValue(key, out value))
                return value;
            else
                return defaultValue;
        }
    }
}

