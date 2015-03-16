/*
 *  $Id: KeyedResolver.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный класс разрешителя ключей/значений, использующий реализацию у типа значения интерфейса IKeyed.
    /// </summary>
    public abstract class KeyedResolver<TKey, TValue> : IKeyResolver<TKey, TValue> where TValue : IKeyed<TKey> {
        /// <summary>
        /// Получает ключ через интерфейс IKeyed.
        /// </summary>
        public TKey ResolveKey(TValue value) {
            return value.Key;
        }

        /// <summary>
        /// Получает значение по ключу. Не реализовано.
        /// </summary>
        public abstract TValue ResolveValue(TKey key);
    }
}
