/*
 *  $Id: QualifiedTypeResolver.cs 134 2010-10-27 17:21:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Стандартный определитель типов, использующий AssemblyQualifiedName.
    /// </summary>
    public class QualifiedTypeResolver : ITypeResolver {
        /// <summary>
        /// Xml-имя атрибута, в котором хранится тип.
        /// </summary>
        public const string TypeAttributeName = "type";

        /// <summary>
        /// Читает атрибут и ищет по его значению тип.
        /// </summary>
        public Type Deserialize(DeserializationContext context, Type baseType) {
            var name = context.ReadAttribute(TypeAttributeName);
            var objectType = !string.IsNullOrEmpty(name) ? Type.GetType(name, false) : null;
             if (objectType == null || !baseType.IsAssignableFrom(objectType))
                objectType = baseType;
            return objectType;
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
                context.WriteAttribute(TypeAttributeName, objectType.AssemblyQualifiedName);
        }
    }
}
