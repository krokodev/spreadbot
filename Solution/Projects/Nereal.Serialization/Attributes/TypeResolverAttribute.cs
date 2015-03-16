/*
 *  $Id: TypeResolverAttribute.cs 193 2010-12-03 13:05:23Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает тип определителя типов для данного класса и него потомков.
    /// Указанный тип должен реализовывать интерфейс <see cref="ITypeResolver" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class TypeResolverAttribute : Attribute {
        /// <summary>
        /// Тип определителя типов.
        /// </summary>
        public readonly Type ResolverType;
        /// <summary>
        /// Xml-имя атрибута, в котором сохраняется тип.
        /// </summary>
        public readonly string XmlAttributeName;

        /// <summary>
        /// Создает новый экземпляр TypeResolverAttribute без указания имени атрибута.
        /// </summary>
        public TypeResolverAttribute(Type resolverType) : this(resolverType, "type") {
        }
        /// <summary>
        /// Создает новый экземпляр TypeResolverAttribute с указанием имени атрибута.
        /// </summary>
        public TypeResolverAttribute(Type resolverType, string xmlAttributeName) {
            ResolverType = resolverType;
            XmlAttributeName = xmlAttributeName;
        }
    }
}

