/*
 *  $Id: ReferenceResolverAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данный класс является определителем ссылок.
    /// Класс при этом должен реализовывать интерфейс <see cref="T:IReferenceResolver`2" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ReferenceResolverAttribute : Attribute {
        /// <summary>
        /// Имя коллектора ссылок.
        /// </summary>
        public readonly string CollectorName;
        /// <summary>
        /// Тип ключа ссылки.
        /// </summary>
        public readonly Type KeyType;
        /// <summary>
        /// Тип значения ссылки.
        /// </summary>
        public readonly Type ValueType;

        /// <summary>
        /// Создает новый экземпляр ReferenceResolverAttribute.
        /// </summary>
        public ReferenceResolverAttribute(string collectorName, Type keyType, Type valueType) {
            CollectorName = collectorName;
            KeyType = keyType;
            ValueType = valueType;
        }
    }
}
