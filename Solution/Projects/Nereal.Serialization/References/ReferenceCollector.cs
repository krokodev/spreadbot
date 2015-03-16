/*
 *  $Id: ReferenceCollector.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный класс коллектора ссылок. Создан только для того, чтобы не хранить список коллекторов как object.
    /// </summary>
    public abstract class ReferenceCollector {
        /// <summary>
        /// Вызывает определение для всех собранных ссылок.
        /// </summary>
        public abstract void Resolve();
    }

    /// <summary>
    /// Стандартный коллектор ссылок: собирает ссылки и вызывает определение для всех собранных ссылок.
    /// </summary>
    public class ReferenceCollector<TKey, TValue> : ReferenceCollector, IKeyResolver<TKey, TValue> {
        private List<ReferenceWrapper<TKey, TValue>> _refs;
        private IKeyResolver<TKey, TValue> _resolver;

        /// <summary>
        /// Создает новый экземпляр коллектора ссылок.
        /// </summary>
        public ReferenceCollector(IKeyResolver<TKey, TValue> resolver) {
            _refs = new List<ReferenceWrapper<TKey, TValue>>();
            _resolver = resolver;
        }

        /// <summary>
        /// Добавляет ссылку в коллектор.
        /// </summary>
        public void Add(ReferenceWrapper<TKey, TValue> reference) {
            _refs.Add(reference);
        }

        /// <summary>
        /// Вызывает определение для всех собранных ссылок.
        /// </summary>
        public override void Resolve() {
            foreach (var r in _refs)
                r.Resolve(_resolver);
        }

        TKey IKeyResolver<TKey, TValue>.ResolveKey(TValue value) {
            return _resolver.ResolveKey(value);
        }

        TValue IKeyResolver<TKey, TValue>.ResolveValue(TKey key) {
            return _resolver.ResolveValue(key);
        }
    }
}
