/*
 *  $Id: ReferenceAttribute.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данное поле или свойство является ссылкой, и должно сохраняться не по значению, а по идентификатору.
    /// Кроме того, разрешаться некоторые ссылки должны после окончания десериализации, так как могут быть и на объекты внутри того же документа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReferenceAttribute : Attribute {
        /// <summary>
        /// Внешняя ли ссылка.
        /// </summary>
        public readonly bool External;
        /// <summary>
        /// Тип ключа ссылки.
        /// </summary>
        public readonly Type KeyType;
        /// <summary>
        /// Имя определителя/коллектора ссылок.
        /// </summary>
        public readonly string ResolverName;
        /// <summary>
        /// Использовать ли корневой коллектор.
        /// </summary>
        public readonly bool RootCollector;

        /// <summary>
        /// Создает новый экземпляр ReferenceAttribute.
        /// </summary>
        public ReferenceAttribute(bool external, Type keyType, string resolverName) : this(external, keyType, resolverName, false) {
        }
        /// <summary>
        /// Создает новый экземпляр ReferenceAttribute с указанием, использовать ли корневой коллектор.
        /// </summary>
        public ReferenceAttribute(bool external, Type keyType, string resolverName, bool rootCollector) {
            External = external;
            KeyType = keyType;
            ResolverName = resolverName;
            RootCollector = rootCollector;
        }
    }
}
