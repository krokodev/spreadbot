/*
 *  $Id: DictTypeResolver.cs 184 2010-11-28 12:51:22Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Словарный определитель типов: регистрирует соответствия строковых ключей и типов, и по ним определяет.
    /// Может сохранять ключ как в атрибут по указанному имени, так и в имя элемента (если имя атрибута отсутствует).
    /// </summary>
    public class DictTypeResolver : ITypeResolver, ITypeIdDictionary {
        private Dictionary<string, Type> _types;
        private Dictionary<Type, string> _ids;
        private string _attrName;

        /// <summary>
        /// Создает новый экземпляр словарного определителя типов.
        /// </summary>
        public DictTypeResolver(string attrName) {
            _types = new Dictionary<string, Type>();
            _ids = new Dictionary<Type, string>();
            _attrName = attrName;
            RegisterTypes();
        }

        /// <summary>
        /// Регистрирует соответствие ключ/тип.
        /// </summary>
        public void Register(string id, Type type) {
            _types.Add(id, type);
            _ids.Add(type, id);
        }

        /// <summary>
        /// Регистрирует все необходимые типы. Должно быть реализовано в потомке.
        /// </summary>
        protected virtual void RegisterTypes() {
        }

        #region ITypeResolver implementation
        /// <summary>
        /// Читает ключ и ищет по нему тип.
        /// </summary>
        public Type Deserialize(DeserializationContext context, Type baseType) {
            var id = ReadTypeId(context);
            if (string.IsNullOrEmpty(id))
                return baseType;
            return _types.GetOrDefault(id, baseType);
        }

        /// <summary>
        /// Читает ключ типа из атрибута или имени элемента.
        /// </summary>
        private string ReadTypeId(DeserializationContext context) {
            if (string.IsNullOrEmpty(_attrName))
                return context.GetName();
            else
                return context.ReadAttribute(_attrName);
        }

        /// <summary>
        /// Получает имя элемента, если отсутствует имя атрибута.
        /// </summary>
        public string GetTypeElementName(Type objectType) {
            return string.IsNullOrEmpty(_attrName) ? _ids.GetOrDefault(objectType) : null;
        }

        /// <summary>
        /// Сохраняет ключ типа в атрибут, если есть имя атрибута.
        /// </summary>
        public void Serialize(SerializationContext context, Type baseType, Type objectType) {
            if (!string.IsNullOrEmpty(_attrName) && objectType != baseType && _ids.ContainsKey(objectType))
                context.WriteAttribute(_attrName, _ids[objectType]);
        }
        #endregion

        #region ITypeIdDictionary implementation
        bool ITypeIdDictionary.ContainsType(Type type) {
            return _ids.ContainsKey(type);
        }

        bool ITypeIdDictionary.ContainsId(string id) {
            return _types.ContainsKey(id);
        }

        string ITypeIdDictionary.GetId(Type type) {
            return _ids.GetOrDefault(type, null);
        }

        Type ITypeIdDictionary.GetType(string id) {
            return _types.GetOrDefault(id, null);
        }
        #endregion
    }
}
