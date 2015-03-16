/*
 *  $Id: ReferenceCollectorList.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Список коллекторов ссылок.
    /// </summary>
    public sealed class ReferenceCollectorList {
        private Dictionary<string, List<ReferenceCollector>> _collectors;

        /// <summary>
        /// Создает новый экземпляр списка коллекторов ссылок.
        /// </summary>
        public ReferenceCollectorList() {
            _collectors = new Dictionary<string, List<ReferenceCollector>>();
        }

        /// <summary>
        /// Получает список коллекторов по имени, с возможным созданием нового списка.
        /// </summary>
        private List<ReferenceCollector> GetStack(string name, bool create) {
            if (Contains(name))
                return _collectors[name];
            if (!create)
                return null;
            var stack = new List<ReferenceCollector>();
            _collectors.Add(name, stack);
            return stack;
        }

        /// <summary>
        /// Добавляет коллектор в список по указанному имени.
        /// </summary>
        public void Add<TKey, TValue>(string name, ReferenceCollector<TKey, TValue> collector) {
            var stack = GetStack(name, true);
            stack.Add(collector);
        }

        /// <summary>
        /// Удаляет из списка последний коллектор по указанному имени.
        /// </summary>
        public void Remove(string name) {
            var stack = GetStack(name, false);
            if (stack != null) {
                if (stack.Count > 0)
                    stack.RemoveAt(stack.Count - 1);
            }
        }

        /// <summary>
        /// Проверяет, есть ли в списке коллекторы с указанным именем.
        /// </summary>
        public bool Contains(string name) {
            return _collectors.ContainsKey(name);
        }

        /// <summary>
        /// Получает последний (или первый, если root == true) коллектор по указанному имени.
        /// </summary>
        private ReferenceCollector Get(string name, bool root) {
            var stack = GetStack(name, false);
            return stack != null && stack.Count > 0 ? stack[root ? 0 : stack.Count - 1] : null;
        }

        /// <summary>
        /// Получает последний (или первый, если root == true) коллектор по указанному имени.
        /// </summary>
        public ReferenceCollector<TKey, TValue> Get<TKey, TValue>(string name, bool root) {
            return Get(name, root) as ReferenceCollector<TKey, TValue>;
        }

        /// <summary>
        /// Вызывает определение ссылок в коллекторе с указанным именем.
        /// </summary>
        public void Resolve(string name) {
            var collector = Get(name, false);
            if (collector != null)
                collector.Resolve();
        }

        /// <summary>
        /// Вызывает определение ссылок в коллекторе с указанным именем, и сразу удаляет этот коллектор из списка.
        /// </summary>
        public void ResolveAndRemove(string name) {
            Resolve(name);
            Remove(name);
        }
    }
}
