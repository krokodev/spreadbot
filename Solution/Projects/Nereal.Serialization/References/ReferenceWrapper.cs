/*
 *  $Id: ReferenceWrapper.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Обертка над ссылкой.
    /// </summary>
    public sealed class ReferenceWrapper<TKey, TValue> {
        private Action<TValue> _setter;
        private TKey _key;

        /// <summary>
        /// Создает новый экземпляр обертки над ссылкой.
        /// </summary>
        public ReferenceWrapper(Action<TValue> setter, TKey key) {
            _setter = setter;
            _key = key;
        }

        /// <summary>
        /// Определяет значение ссылки и устанавливает его.
        /// </summary>
        public void Resolve(IKeyResolver<TKey, TValue> resolver) {
            _setter(resolver.ResolveValue(_key));
        }
    }
}
