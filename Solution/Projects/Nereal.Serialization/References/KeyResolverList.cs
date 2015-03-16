/*
 *  $Id: KeyResolverList.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Список определителей ключей.
    /// </summary>
    public sealed class KeyResolverList {
        private Dictionary<string, object> _resolvers;

        /// <summary>
        /// Создает новый экземпляр списка определителей ключей.
        /// </summary>
        public KeyResolverList() {
            _resolvers = new Dictionary<string, object>();
        }

        /// <summary>
        /// Добавляет определитель в список по указанному имени.
        /// </summary>
        public void Add<TKey, TValue>(string name, IKeyResolver<TKey, TValue> resolver) {
            _resolvers[name] = resolver;
        }

        /// <summary>
        /// Удаляет определитель из списка по указанному имени.
        /// </summary>
        public void Remove(string name) {
            _resolvers.Remove(name);
        }

        /// <summary>
        /// Получает определитель по указанному имени.
        /// </summary>
        public IKeyResolver<TKey, TValue> Get<TKey, TValue>(string name) {
            return _resolvers.GetOrDefault(name, null) as IKeyResolver<TKey, TValue>;
        }
    }
}
