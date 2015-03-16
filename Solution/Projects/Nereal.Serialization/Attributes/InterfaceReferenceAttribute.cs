/*
 *  $Id: InterfaceReferenceAttribute.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данное поле или свойство является интерфейсной ссылкой. В отличие от обычной ссылки, интерфейсная действует только в случае, если в свойстве объект указанного типа.
    /// Может быть несколько таких атрибутов у одного поля/свойства, на разные базовые типы.
    /// Если ни один вариант базового типа не подходит, объект сохранится как вложенный, по значению.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InterfaceReferenceAttribute : ReferenceAttribute {
        /// <summary>
        /// Базовый тип для объектов, обрабатываемых данной интерфейсной ссылкой.
        /// </summary>
        public readonly Type BaseType;
        /// <summary>
        /// Имя варианта интерфейсной ссылки, по которой он определяется при десериализации.
        /// </summary>
        public readonly string ImplementationName;

        /// <summary>
        /// Создает новый экземпляр InterfaceReferenceAttribute.
        /// </summary>
        public InterfaceReferenceAttribute(Type baseType, string implName, bool external, Type keyType, string resolverName) : this(baseType, implName, external, keyType, resolverName, false) {
        }
        /// <summary>
        /// Создает новый экземпляр InterfaceReferenceAttribute с указанием, использовать ли корневой коллектор.
        /// </summary>
        public InterfaceReferenceAttribute(Type baseType, string implName, bool external, Type keyType, string resolverName, bool rootCollector) : base(external, keyType, resolverName, rootCollector) {
            BaseType = baseType;
            ImplementationName = implName;
        }
    }
}
