/*
 *  $Id: CacheDictionary.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный кэширующий словарь, создающий отсутствующие элементы при первом запросе.
    /// </summary>
    public abstract class CacheDictionary<K, T> where T : class {
        private Dictionary<K, T> _dict;

        /// <summary>
        /// Инициализирует новый экземпляр кэширующего словаря.
        /// </summary>
        public CacheDictionary() {
            _dict = new Dictionary<K, T>();
        }

        /// <summary>
        /// Получает элемент словаря по ключу, с автоматическим созданием нового элемента в случае отсутствия такового.
        /// </summary>
        public T this[K key] {
            get { return GetItem(key); }
        }

        /// <summary>
        /// Получает все элементы словаря в виде пар ключ-значение.
        /// </summary>
        public IEnumerable<Tuple<K, T>> GetItems() {
            foreach (var item in _dict)
                yield return Tuple.Create(item.Key, item.Value);
        }

        /// <summary>
        /// Получает элемент словаря по ключу, с автоматическим созданием нового элемента в случае отсутствия такового.
        /// </summary>
        protected T GetItem(K key) {
            T value;
            if (!_dict.TryGetValue(key, out value)) {
                value = CreateItem(key);
                Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// Добавляет элемент в словарь и вызывает событие.
        /// </summary>
        protected void Add(K key, T value) {
            _dict[key] = value;
            OnAdded(value);
        }

        /// <summary>
        /// Создает новый элемент по отсутствующему ключу.
        /// </summary>
        protected abstract T CreateItem(K key);

        /// <summary>
        /// Выполняет дополнительные действия после добавления нового элемента.
        /// </summary>
        protected virtual void OnAdded(T value) {
        }
    }
}
