/*
 *  $Id: SerializeExtensions.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Расширения для поддержки клонирования объектов.
    /// </summary>
    public static class SerializeExtensions {
        /// <summary>
        /// Выполняет глубокое клонирование объекта и возвращает его копию.
        /// </summary>
        public static T DeepClone<T>(this T source) {
            return DeepClone(source, SerializationConfig.InfoManager.GetInfo<T>());
        }

        /// <summary>
        /// Выполняет глубокое клонирование объекта и возвращает его копию.
        /// </summary>
        internal static T DeepClone<T>(this T source, TypeInfo<T> info) {
            if (info.IsSealed)
                return info.Clone(source);
            return info.IsNull(source) ? source : info.GetAdapter(source.GetType()).Clone(source);
        }

        /// <summary>
        /// Выполняет глубокое сравнение двух объектов.
        /// </summary>
        public static bool DeepEquals<T>(this T a, T b) {
            return DeepEquals(a, b, SerializationConfig.InfoManager.GetInfo<T>());
        }

        /// <summary>
        /// Выполняет глубокое сравнение двух объектов.
        /// </summary>
        internal static bool DeepEquals<T>(this T a, T b, TypeInfo<T> info) {
            if (info.IsSealed)
                return info.Equals(a, b);
            bool nullA = info.IsNull(a), nullB = info.IsNull(b);
            if (nullA || nullB)
                return nullA && nullB;
            var type = a.GetType();
            if (type != b.GetType())
                return false;
            return info.GetAdapter(type).Equals(a, b);
        }
    }
}
