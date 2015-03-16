/*
 *  $Id: FullNameTypeResolver.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

namespace Nereal.Serialization {
    /// <summary>
    /// Стандартный определитель типов, использующий FullName типа.
    /// При поиске проверяет все загруженные сборки, и кэширует найденные типы.
    /// </summary>
    public sealed class FullNameTypeResolver : ITypeResolver {
        /// <summary>
        /// Xml-имя атрибута, в котором хранится тип.
        /// </summary>
        public const string TypeAttributeName = "type";

        private Dictionary<string, Type> _cache;

        /// <summary>
        /// Создает новый экземпляр определителя типов по полным именам.
        /// </summary>
        public FullNameTypeResolver() {
            _cache = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Читает атрибут и ищет по его значению тип.
        /// </summary>
        public Type Deserialize(DeserializationContext context, Type baseType) {
            var name = context.ReadAttribute(TypeAttributeName);
            var objectType = !string.IsNullOrEmpty(name) ? GetType(name) : null;
            if (objectType == null || !baseType.IsAssignableFrom(objectType))
                objectType = baseType;
            return objectType;
        }

        /// <summary>
        /// Получает тип по имени из кэша.
        /// </summary>
        private Type GetType(string name) {
            Type type;
            if (!_cache.TryGetValue(name, out type)) {
                type = FindType(name);
                if (type != null)
                    _cache.Add(name, type);
            }
            return type;
        }

        /// <summary>
        /// Ищет тип по имени во всех загруженных сборках.
        /// Возвращает null, если тип не найден.
        /// </summary>
        private Type FindType(string name) {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) {
                var type = asm.GetType(name, false);
                if (type != null)
                    return type;
            }
            return null;
        }

        /// <summary>
        /// Тип сохраняется в атрибуте, потому имя элемента всегда null.
        /// </summary>
        public string GetTypeElementName(Type objectType) {
            return null;
        }

        /// <summary>
        /// Сохраняет имя типа в атрибуте, если он не равен базовому.
        /// </summary>
        public void Serialize(SerializationContext context, Type baseType, Type objectType) {
            if (objectType != baseType)
                context.WriteAttribute(TypeAttributeName, objectType.FullName);
        }
    }
}
