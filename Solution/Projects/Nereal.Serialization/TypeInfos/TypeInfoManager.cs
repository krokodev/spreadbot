/*
 *  $Id: TypeInfoManager.cs 169 2010-11-17 15:45:13Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Менеджер информации о типах.
    /// Создает, хранит, и выдает информацию о типах.
    /// </summary>
    public sealed class TypeInfoManager : CacheDictionary<Type, AbstractTypeInfo> {
        private Dictionary<Type, Type> _directInfos;
        private List<ITypeInfoSelector> _selectors;

        /// <summary>
        /// Создает новый экземпляр менеджера информации о типах.
        /// </summary>
        public TypeInfoManager() {
            _directInfos = new Dictionary<Type, Type>();
            _selectors = new List<ITypeInfoSelector>();
            RegisterInfo(typeof(TypeInfoManager).Assembly);
        }

        /// <summary>
        /// Регистрирует классы информации о конкретных типах и селекторы классов информации из указанной сборки.
        /// </summary>
        public void RegisterInfo(Assembly asm) {
            foreach (var v in asm.GetChildTypes<TypeInfoAttribute>(typeof(AbstractTypeInfo)))
                _directInfos.Add(v.Item1.InfoType, v.Item2);
            var selectorInterface = typeof(ITypeInfoSelector);
            foreach (var v in asm.GetTypes()) {
                if (!v.IsAbstract && v.HasAttribute<TypeInfoSelectorAttribute>() && v.Implements(selectorInterface)) {
                    var selector = (ITypeInfoSelector) Activator.CreateInstance(v);
                    _selectors.Add(selector);
                }
            }
            _selectors.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }

        /// <summary>
        /// Получает селектор по указанному типу.
        /// </summary>
        private ITypeInfoSelector GetSelector(Type objectType) {
            foreach (var selector in _selectors)
                if (selector.Accept(objectType))
                    return selector;
            throw new SerializationException(string.Format("Cannot find a TypeInfo for '{0}' type.", objectType));
        }

        /// <summary>
        /// Получает типизированную информацию о типе.
        /// </summary>
        public TypeInfo<T> GetInfo<T>() {
            return (TypeInfo<T>) this[typeof(T)];
        }

        /// <summary>
        /// Получает адаптированную информацию об указанном типе.
        /// </summary>
        public TypeInfo<T> GetAdaptedInfo<T>(Type objectType) {
            var info = GetInfo<T>();
            return info.GetAdapter(objectType);
        }

        /// <summary>
        /// Создает новый объект информации по указанному типу.
        /// </summary>
        protected override AbstractTypeInfo CreateItem(Type key) {
            Type infoType;
            if (!_directInfos.TryGetValue(key, out infoType)) {
                var selector = GetSelector(key);
                infoType = selector.GetTypeInfo(key);
            }
            return (AbstractTypeInfo) Activator.CreateInstance(infoType);
        }
    }
}
